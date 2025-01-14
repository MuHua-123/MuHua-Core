using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// UI窗口
/// </summary>
public abstract class ModuleUIWindow<Data> : MonoBehaviour {
    /// <summary> 绑定的页面 </summary>
    public ModuleUIPage ModuleUIPage;
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;
    /// <summary> 绑定的根元素 </summary>
    public abstract VisualElement Element { get; }
    /// <summary> 打开窗口，并且传进参数 </summary>
    public abstract void Open(Data data);
    /// <summary> 关闭窗口 </summary>
    public abstract void Close();
}
