using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlateVertex {
    /// <summary> 设计视图中位置 </summary>
    public Vector3 position;

    /// <summary> 上 </summary>
    public int above;
    /// <summary> 下 </summary>
    public int below;
    /// <summary> 左 </summary>
    public int lefts;
    /// <summary> 右 </summary>
    public int right;

    /// <summary> 左上 </summary>
    public int leftsAbove;
    /// <summary> 左下 </summary>
    public int leftsBelow;
    /// <summary> 右上 </summary>
    public int rightAbove;
    /// <summary> 右下 </summary>
    public int rightBelow;
}
