using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 线段 </summary>
public class DataPlateLine {
    /// <summary> 线段起点a </summary>
    public Vector3 a;
    /// <summary> 线段终点b </summary>
    public Vector3 b;
    /// <summary> 原始距离 </summary>
    public float origin;
    /// <summary> 线段距离 </summary>
    public float Distance => Vector3.Distance(a, b);
    /// <summary> 线段方向 </summary>
    public Vector3 Direction => b - a;
}
