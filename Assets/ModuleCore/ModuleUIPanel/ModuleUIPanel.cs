using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// UI面板
/// </summary>
public abstract class ModuleUIPanel : MonoBehaviour {
    /// <summary> 绑定的页面 </summary>
    public ModuleUIPage ModuleUIPage;
    /// <summary> 可选初始化 </summary>
    protected virtual void Awake() { }
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;

    public abstract VisualElement Element { get; }
}
