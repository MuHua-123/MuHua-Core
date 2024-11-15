using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPolygon {
    private readonly DataPlate dataPlate;
    public DataPolygon(DataPlate dataPlate) => this.dataPlate = dataPlate;

    /// <summary> 边缘平滑度 </summary>
    public float edgeSmooth => dataPlate.edgeSmooth;
    /// <summary> 设计点 </summary>
    public List<DataPoint> points => dataPlate.points;
    /// <summary> 边缘点 </summary>
    public List<Vector3> edgePoints {
        get => dataPlate.edgePoints;
        set => dataPlate.edgePoints = value;
    }
    /// <summary> 平面网格 </summary>
    public Mesh polygon {
        get => dataPlate.polygon;
        set => dataPlate.polygon = value;
    }

    #region 参数

    #endregion

    #region 缓存
    /// <summary> 三角形 </summary>
    public List<DataTriangle> triangles = new List<DataTriangle>();
    #endregion

}
