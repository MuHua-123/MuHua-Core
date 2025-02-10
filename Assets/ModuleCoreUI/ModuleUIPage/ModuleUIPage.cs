using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// UI页面
/// </summary>
public abstract class ModuleUIPage : MonoBehaviour {
    /// <summary> 绑定的页面 </summary>
    public ModuleDocument document;
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;
    /// <summary> 绑定的根元素 </summary>
    public abstract VisualElement Element { get; }
}