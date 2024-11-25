using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLine {
    public Vector3 a;
    public Vector3 b;

    /// <summary> 曲线起点的距离 </summary>
    public float origin;

    /// <summary> 线段距离 </summary>
    public float Distance => Vector3.Distance(a, b);
    /// <summary> 线段方向 </summary>
    public Vector3 Direction => b - a;
}
