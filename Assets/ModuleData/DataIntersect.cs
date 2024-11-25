using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 交点信息 </summary>
public class DataIntersect {
    public readonly DataSide side;
    public readonly Vector3 position;
    /// <summary> 交点信息 </summary>
    public DataIntersect(DataSide side, Vector3 position) {
        this.side = side;
        this.position = position;
    }
    /// <summary> 是否相交 </summary>
    public bool isIntersect;
    /// <summary> 交点 </summary>
    public Vector3 intersectPoint;
}
