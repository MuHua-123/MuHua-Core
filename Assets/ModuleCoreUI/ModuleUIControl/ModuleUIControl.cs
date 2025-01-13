using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// UI控件
/// </summary>
public abstract class ModuleUIControl {
    /// <summary> 绑定的元素 </summary>
    public readonly VisualElement element;
    /// <summary> 基础实例 </summary>
    public ModuleUIControl(VisualElement element) => this.element = element;
}
