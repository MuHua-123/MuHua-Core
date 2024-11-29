using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 边界数据 </summary>
public class DataBorder {
    /// <summary> 网格细分 </summary>
    public readonly float smooth = 0.01f;
    /// <summary> minX </summary>
    public readonly float minX = 0;
    /// <summary> maxX </summary>
    public readonly float maxX = 0;
    /// <summary> minY </summary>
    public readonly float minY = 0;
    /// <summary> maxY </summary>
    public readonly float maxY = 0;
    /// <summary> 多边形边缘点 </summary>
    public readonly Vector3[] points;
    /// <summary> 边界数据 </summary>
    public DataBorder(float minX, float maxX, float minY, float maxY, Vector3[] points) {
        this.minX = minX; this.maxX = maxX;
        this.minY = minY; this.maxY = maxY;
        this.points = points;
    }
    /// <summary> 边界宽 </summary>
    public float Wide => maxX - minX;
    /// <summary> 边界高 </summary>
    public float High => maxY - minY;
    /// <summary> 网格宽 </summary>
    public int GridWide => Mathf.FloorToInt(Wide / smooth) + 1;
    /// <summary> 网格高 </summary>
    public int GridHigh => Mathf.FloorToInt(High / smooth) + 1;
    /// <summary> 最小点 </summary>
    public Vector3 MinPoint => new Vector3(minX, minY, 0);
    /// <summary> 最大点 </summary>
    public Vector3 MaxPoint => new Vector3(maxX, maxY, 0);
}
