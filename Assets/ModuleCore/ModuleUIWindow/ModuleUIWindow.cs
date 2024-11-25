using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI窗口
/// </summary>
/// <typeparam name="Data">窗口需要的数据类型</typeparam>
public abstract class ModuleUIWindow<Data> : MonoBehaviour {
    /// <summary> 绑定的页面 </summary>
    public ModuleUIPage ModuleUIPage;
    /// <summary> 必须初始化 </summary>
    public abstract void Awake();
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;
    /// <summary> 打开模块，并且传进参数 </summary>
    public abstract void Open(Data data);
    /// <summary> 关闭模块 </summary>
    public abstract void Close();
}
