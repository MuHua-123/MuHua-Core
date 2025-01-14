using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// UI窗口
/// </summary>
public abstract class ModuleUIWindow<T> : ModuleSingle<T> {
    /// <summary> 绑定的页面 </summary>
    public ModuleUIPage UIPage;
    /// <summary> 绑定的根元素 </summary>
    public abstract VisualElement Element { get; }
}
