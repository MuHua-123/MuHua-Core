using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// UI面板单元
/// </summary>
public abstract class UnitUIPanel : MonoBehaviour {
    /// <summary> 绑定的页面 </summary>
    public ModuleUIPage ModuleUIPage;
    /// <summary> 必须初始化 </summary>
    protected abstract void Awake();
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;

    public abstract VisualElement Element { get; }
}
