using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 图层遮罩工具
/// </summary>
public static class LayerMaskTool {
    /// <summary> 安排点图层遮罩 </summary>
    public static readonly LayerMask Arrange = 1 << LayerMask.NameToLayer("Arrange");
}
