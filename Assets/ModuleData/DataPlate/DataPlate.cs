using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlate {
    /// <summary> 核心模块 </summary>
    private ModuleCore ModuleCore => ModuleCore.I;
    /// <summary> 设计可视化模块 </summary>
    private ModuleVisual<DataPlate> VisualDesign => ModuleCore.VisualDesign;
    /// <summary> 烘焙可视化模块 </summary>
    private ModuleVisual<DataPlate> VisualBaking => ModuleCore.VisualBaking;
    /// <summary> 简单多边形算法模块 </summary>
    private ModuleAlgorithm<DataPlate> AlgorithmSimplePolygon => ModuleCore.AlgorithmSimplePolygon;
    /// <summary> 细分多边形算法模块 </summary>
    private ModuleAlgorithm<DataPlate> AlgorithmSubdivisionPolygon => ModuleCore.AlgorithmSubdivisionPolygon;

    public void UpdateVisual(bool recalculate = true) {
        if (recalculate) {
            //简单多边形计算
            //AlgorithmSimplePolygon.Compute(this);
            //细分多边形计算
            AlgorithmSubdivisionPolygon.Compute(this);
        }
        //生成可视化内容
        VisualDesign.UpdateVisual(this);
        //生成烘焙内容
        VisualBaking.UpdateVisual(this);
    }

    #region 核心数据
    /// <summary> 边缘平滑度 </summary>
    public float smooth = 0.01f;
    /// <summary> 板片设计视图的位置(本地坐标系) </summary>
    public Vector3 designPosition;
    /// <summary> 板片烘焙视图的位置(本地坐标系) </summary>
    public Vector3 bakingPosition;
    /// <summary> 板片烘焙视图的旋转(本地坐标系) </summary>
    public Vector3 bakingEulerAngles;
    /// <summary> 点 </summary>
    public List<DataPoint> points = new List<DataPoint>();
    /// <summary> 边 </summary>
    public List<DataSide> sides = new List<DataSide>();
    #endregion

    #region 次要数据
    /// <summary> 边界数据 </summary>
    public DataBorder border;
    /// <summary> 边缘点 </summary>
    public List<Vector3> edgePoints;
    /// <summary> 设计网格 </summary>
    public Mesh designMesh;
    
    /// <summary> 三角形数据 </summary>
    public List<DataTriangle> triangles;

    /// <summary> 顶点网格 </summary>
    public GridTool<DataVertex> vertexGrid;
    #endregion

    #region 可视化数据
    /// <summary> 可视化对象 </summary>
    public ModulePrefab<DataPlate> design;
    /// <summary> 可视化对象 </summary>
    public ModulePrefab<DataPlate> baking;
    /// <summary> 安排点 </summary>
    public FixedArrange arrange;
    #endregion

}