using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// UI项
/// </summary>
public abstract class ModuleUIItem<Data> {
    /// <summary> 选择事件 </summary>
    public static event Action<Data> OnSelect;
    /// <summary> 触发事件 </summary>
    public static void Select(Data data) => OnSelect?.Invoke(data);
    /// <summary> 绑定的数据 </summary>
    public readonly Data value;
    /// <summary> 绑定的元素 </summary>
    public readonly VisualElement element;
    /// <summary> 基础实例 </summary>
    public ModuleUIItem(Data value, VisualElement element) {
        this.value = value;
        this.element = element;
        OnSelect += UIItem_OnSelect;
    }
    /// <summary> 触发选择事件 </summary>
    public virtual void Select() => OnSelect?.Invoke(value);
    /// <summary> 侦听选择事件 </summary>
    public virtual void UIItem_OnSelect(Data obj) {
        if (value.Equals(obj)) { SelectState(); }
        else { DefaultState(); }
    }
    /// <summary> 默认状态 </summary>
    public virtual void DefaultState() { }
    /// <summary> 选中状态 </summary>
    public virtual void SelectState() { }
    /// <summary> 释放 </summary>
    public virtual void Release() => OnSelect -= UIItem_OnSelect;
}
