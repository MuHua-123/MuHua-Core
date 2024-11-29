using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlate {
    /// <summary> 核心模块 </summary>
    private ModuleCore ModuleCore => ModuleCore.I;
    /// <summary> 设计可视化模块 </summary>
    private ModuleVisual<DataPlate> VisualDesign => ModuleCore.VisualPlateDesign;
    /// <summary> 烘焙可视化模块 </summary>
    private ModuleVisual<DataPlate> VisualBaking => ModuleCore.VisualPlateBaking;
    /// <summary> 简单多边形算法模块 </summary>
    private ModuleAlgorithm<DataPlate> AlgorithmSimplePolygon => ModuleCore.AlgorithmSimplePolygon;
    /// <summary> 细分多边形算法模块 </summary>
    private ModuleAlgorithm<DataPlate> AlgorithmSubdivisionPolygon => ModuleCore.AlgorithmSubdivisionPolygon;

    public DataPlate() {
        dataDesign = new DataPlateDesign(this);
    }

    public void UpdateVisual(bool recalculate = true) {
        if (recalculate) {
            //简单多边形计算
            AlgorithmSimplePolygon.Compute(this);
            //细分多边形计算
            AlgorithmSubdivisionPolygon.Compute(this);
        }
        //生成可视化内容
        VisualDesign.UpdateVisual(this);
        //生成烘焙内容
        VisualBaking.UpdateVisual(this);
    }

    #region 核心数据
    /// <summary> 点 </summary>
    public List<DataPlatePoint> platePoints = new List<DataPlatePoint>();
    /// <summary> 边 </summary>
    public List<DataPlateSide> plateSides = new List<DataPlateSide>();
    #endregion

    #region 次要数据
    /// <summary> 设计缓存数据 </summary>
    public DataPlateDesign dataDesign;
    /// <summary> 烘焙缓存数据 </summary>
    public DataPlateBaking dataBaking = new DataPlateBaking();
    #endregion

    #region 可视化数据
    /// <summary> 可视化对象 </summary>
    public ModulePrefab<DataPlate> designPrefab;
    /// <summary> 可视化对象 </summary>
    public ModulePrefab<DataPlate> bakingPrefab;
    /// <summary> 安排点 </summary>
    public FixedArrange arrange;
    #endregion

}
/// <summary> 设计缓存数据 </summary>
public class DataPlateDesign {
    /// <summary> 板片 </summary>
    public readonly DataPlate plate;
    /// <summary> 设计缓存数据 </summary>
    public DataPlateDesign(DataPlate plate) => this.plate = plate;

    /// <summary> 板片的位置 </summary>
    public Vector3 position;
    /// <summary> 网格 </summary>
    public Mesh mesh;
    /// <summary> 边缘点 </summary>
    public Vector3[] points;
    /// <summary> 三角形数据 </summary>
    public List<DataTriangle> triangles;
}
/// <summary> 烘焙缓存数据 </summary>
public class DataPlateBaking {
    /// <summary> 网格 </summary>
    public Mesh mesh;
    /// <summary> 板片的位置 </summary>
    public Vector3 position;
    /// <summary> 板片的旋转 </summary>
    public Vector3 eulerAngles;
    /// <summary> 边界数据 </summary>
    public DataBorder border;
    /// <summary> 全部顶点 </summary>
    public DataPlateVertex[] vertexs;
}