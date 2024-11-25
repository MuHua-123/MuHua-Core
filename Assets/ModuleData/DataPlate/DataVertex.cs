using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataVertex {
    /// <summary> 是否是有效顶点 </summary>
    public bool isValid;
    /// <summary> 设计视图中位置 </summary>
    public Vector3 design;
    /// <summary> 烘焙视图中位置 </summary>
    public Vector3 baking;

    /// <summary> 上 </summary>
    public DataVertex above;
    /// <summary> 下 </summary>
    public DataVertex below;
    /// <summary> 左 </summary>
    public DataVertex left;
    /// <summary> 右 </summary>
    public DataVertex right;

    /// <summary> 左上 </summary>
    public DataVertex leftAbove;
    /// <summary> 左下 </summary>
    public DataVertex leftBelow;
    /// <summary> 右上 </summary>
    public DataVertex rightAbove;
    /// <summary> 右下 </summary>
    public DataVertex rightBelow;
}
