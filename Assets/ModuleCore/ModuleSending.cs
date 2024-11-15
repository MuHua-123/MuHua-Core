using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 发送模块 广播数据
/// </summary>
public abstract class ModuleSending<T> : MonoBehaviour {
    /// <summary> 必须要初始化 </summary>
    protected abstract void Awake(); 
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;

    /// <summary> 当前输入单元 </summary>
    public abstract T Current { get; }
    /// <summary> 改变输入单元时触发 </summary>
    public abstract event Action<T> OnChange;
    /// <summary> 改变输入单元 </summary>
    public abstract void Change(T obj);
}
