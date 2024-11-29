using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public enum Bezier {
    一阶 = 0, 二阶 = 1, 三阶 = 2
}
/// <summary> 边 </summary>
public class DataPlateSide {
    /// <summary> 绑定的板片 </summary>
    public readonly DataPlate plate;
    /// <summary> 初始化 </summary>
    public DataPlateSide(DataPlate plate) => this.plate = plate;

    #region 核心数据
    /// <summary> 贝塞尔曲线阶数 </summary>
    public Bezier bezier;
    /// <summary> 起点 </summary>
    public DataPlatePoint aPoint;
    /// <summary> 终点 </summary>
    public DataPlatePoint bPoint;
    /// <summary> 贝塞尔曲线前(-) </summary>
    public Vector3 aBezier;
    /// <summary> 贝塞尔曲线后(+) </summary>
    public Vector3 bBezier;
    #endregion

    #region 次要数据
    /// <summary> 缝合数据 </summary>
    public DataSuture suture;
    /// <summary> 设计缓存数据 </summary>
    public DataPlateSideDesign dataDesign = new DataPlateSideDesign();
    /// <summary> 烘焙缓存数据 </summary>
    public DataPlateSideBaking dataBaking = new DataPlateSideBaking();
    #endregion

    #region 可视化内容
    /// <summary> 可视化边缘线 </summary>
    public ModulePrefab<DataPlateSide> designPrefab;
    #endregion
}
/// <summary> 设计缓存数据 </summary>
public class DataPlateSideDesign {
    /// <summary> 总长度 </summary>
    public float length;
    /// <summary> 点 </summary>
    public Vector3[] positions = new Vector3[0];
    /// <summary> 线 </summary>
    public DataPlateLine[] lines = new DataPlateLine[0];
}
/// <summary> 烘焙缓存数据 </summary>
public class DataPlateSideBaking {
    /// <summary> 总长度 </summary>
    public float length;
    /// <summary> 点 </summary>
    public Vector3[] positions = new Vector3[0];
    /// <summary> 线 </summary>
    public DataPlateLine[] lines = new DataPlateLine[0];
    /// <summary> 关联的网格顶点 </summary>
    public DataPlateVertex[] vertexs = new DataPlateVertex[0];
}