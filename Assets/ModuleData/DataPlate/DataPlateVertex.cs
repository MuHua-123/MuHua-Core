using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlateVertex {
    /// <summary> 是否是有效顶点 </summary>
    public bool isValid;
    /// <summary> 设计视图中位置 </summary>
    public Vector3 position;

    /// <summary> 上 </summary>
    public DataPlateVertex above;
    /// <summary> 下 </summary>
    public DataPlateVertex below;
    /// <summary> 左 </summary>
    public DataPlateVertex left;
    /// <summary> 右 </summary>
    public DataPlateVertex right;

    /// <summary> 左上 </summary>
    public DataPlateVertex leftAbove;
    /// <summary> 左下 </summary>
    public DataPlateVertex leftBelow;
    /// <summary> 右上 </summary>
    public DataPlateVertex rightAbove;
    /// <summary> 右下 </summary>
    public DataPlateVertex rightBelow;
}
