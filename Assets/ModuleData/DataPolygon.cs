using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPolygon {

    #region 次要数据
    /// <summary> 平面网格 </summary>
    public Mesh mesh;
    /// <summary> 三角形数据 </summary>
    public List<DataTriangle> triangles;
    #endregion

    #region 可视化数据
    /// <summary> 可视化对象 </summary>
    public MeshFilter meshFilter;
    #endregion

}
