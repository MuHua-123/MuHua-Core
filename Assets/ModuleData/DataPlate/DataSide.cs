using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Bezier {
    一阶 = 0, 二阶 = 1, 三阶 = 2
}
public class DataSide {
    /// <summary> 绑定的板片 </summary>
    public readonly DataPlate plate;
    /// <summary> 初始化 </summary>
    public DataSide(DataPlate plate) => this.plate = plate;

    #region 核心数据
    /// <summary> 贝塞尔曲线阶数 </summary>
    public Bezier bezier;
    /// <summary> 起点 </summary>
    public DataPoint aPoint;
    /// <summary> 终点 </summary>
    public DataPoint bPoint;
    /// <summary> 贝塞尔曲线前(-) </summary>
    public Vector3 aBezier;
    /// <summary> 贝塞尔曲线后(+) </summary>
    public Vector3 bBezier;
    #endregion

    #region 次要数据
    /// <summary> 边长度 </summary>
    public float length;
    /// <summary> 缝合数据 </summary>
    public DataSuture suture;
    /// <summary> 边缘点 </summary>
    public Vector3[] positions = new Vector3[0];
    /// <summary> 边缘线 </summary>
    public DataLine[] lines = new DataLine[0];
    /// <summary> 边缘顶点数据 </summary>
    public DataVertex[] vertices = new DataVertex[0];
    #endregion

    #region 可视化内容
    /// <summary> 可视化边缘线 </summary>
    public ModulePrefab<DataSide> design;
    #endregion
}
