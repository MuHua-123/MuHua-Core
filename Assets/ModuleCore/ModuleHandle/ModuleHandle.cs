using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 数据处理器模块
/// </summary>
public class ModuleHandle<T> {
    /// <summary> 数据 </summary>
    protected T value;
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;

    /// <summary> 当前数据 </summary>
    public virtual T Current => value;
    /// <summary> 当前数据是否有效 </summary>
    public virtual bool IsValid => Current != null;

    /// <summary> 改变当前数据 Event </summary>
    public virtual event Action<T> OnChange;
    /// <summary> 改变当前数据 </summary>
    public virtual void Change() => OnChange?.Invoke(value);
    /// <summary> 改变当前数据 </summary>
    public virtual void Change(T value) {this.value = value; OnChange?.Invoke(value); }
}