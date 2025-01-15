using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 数据处理器模块
/// </summary>
public class ModuleHandle<Data> {
    /// <summary> 数据 </summary>
    protected Data value;

    /// <summary> 当前数据 </summary>
    public virtual Data Current => value;
    /// <summary> 当前数据是否有效 </summary>
    public virtual bool IsValid => Current != null;

    /// <summary> 改变当前数据 Event </summary>
    public virtual event Action<Data> OnChange;
    /// <summary> 改变当前数据 </summary>
    public virtual void Change() => OnChange?.Invoke(value);
    /// <summary> 改变当前数据 </summary>
    public virtual void Change(Data value) { this.value = value; OnChange?.Invoke(value); }
}
