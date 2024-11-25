using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 2D边界数据 </summary>
public class DataBorder {
    public readonly float minX = 0;
    public readonly float maxX = 0;
    public readonly float minY = 0;
    public readonly float maxY = 0;
    /// <summary> 2D边界数据 </summary>
    public DataBorder(float minX, float maxX, float minY, float maxY) {
        this.minX = minX; this.maxX = maxX;
        this.minY = minY; this.maxY = maxY;
    }
    /// <summary> 边界宽 </summary>
    public float Wide => maxX - minX;
    /// <summary> 边界高 </summary>
    public float High => maxY - minY;
    /// <summary> 最小点 </summary>
    public Vector3 MinPoint => new Vector3(minX, minY, 0);
    /// <summary> 最大点 </summary>
    public Vector3 MaxPoint => new Vector3(maxX, maxY, 0);
}
