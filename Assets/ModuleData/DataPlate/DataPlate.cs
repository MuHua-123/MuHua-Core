using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlate {

    #region 核心数据
    /// <summary> 边缘平滑度 </summary>
    public float edgeSmooth = 0.01f;
    /// <summary> 板片位置(本地坐标系) </summary>
    public Vector3 position;
    /// <summary> 设计点 </summary>
    public List<DataPoint> points = new List<DataPoint>();
    #endregion

    #region 次要数据
    /// <summary> 平面网格 </summary>
    public Mesh polygon;
    /// <summary> 边缘点 </summary>
    public List<Vector3> edgePoints = new List<Vector3>();
    #endregion

    #region 可视化数据
    /// <summary> 可视化对象 </summary>
    public Transform transform;
    /// <summary> 可视化多边形网格 </summary>
    public MeshFilter polygonMeshFilter;
    /// <summary> 可视化边缘线 </summary>
    public LineRenderer edgeLineRenderer;
    #endregion

}