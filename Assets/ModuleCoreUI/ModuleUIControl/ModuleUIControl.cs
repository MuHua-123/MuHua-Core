using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// UI控件
/// </summary>
public class ModuleUIControl {
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;
    /// <summary> 绑定的元素 </summary>
    public readonly VisualElement element;
    /// <summary> UI控件 </summary>
    public ModuleUIControl(VisualElement element) => this.element = element;
}
