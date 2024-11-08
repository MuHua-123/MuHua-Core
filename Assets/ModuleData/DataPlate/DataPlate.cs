using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlate {
    public Action OnChange;
    public Action<int> OnChangeDesignPoint;
    public Action<int> OnChangeEdgePoint;

    /// <summary> 边缘平滑度 </summary>
    public float edgeSmooth = 0.01f;
    /// <summary> 设计点 </summary>
    public List<DataDesignPoint> designPoints = new List<DataDesignPoint>();

    /// <summary> 模型中心点偏移 </summary>
    public Vector3 centerOffset;
    /// <summary> 边缘点 </summary>
    public List<Vector2> edgePoints = new List<Vector2>();

    //平面网格数据
    /// <summary> 顶点 </summary>
    public List<Vector3> vertices = new List<Vector3>();
    /// <summary> UV </summary>
    public List<Vector2> uv = new List<Vector2>();
    /// <summary> 三角形 </summary>
    public List<int> triangles = new List<int>();
}