using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// UI项
/// </summary>
public abstract class ModuleUIItem<Data> : ModuleUIControl {
    /// <summary> 选择事件 </summary>
    public static event Action<Data> OnSelect;
    /// <summary> 触发事件 </summary>
    public static void Select(Data data) => OnSelect?.Invoke(data);
    /// <summary> 绑定的数据 </summary>
    public readonly Data value;
    /// <summary> UI项 </summary>
    public ModuleUIItem(Data value, VisualElement element) : base(element) {
        this.value = value;
        OnSelect += UnitUIPanelItem_OnSelect;
    }
    /// <summary> 触发选择事件 </summary>
    public virtual void Select() => OnSelect?.Invoke(value);
    /// <summary> 侦听选择事件 </summary>
    public virtual void UnitUIPanelItem_OnSelect(Data obj) {
        if (value.Equals(obj)) { SelectState(); }
        else { DefaultState(); }
    }
    /// <summary> 默认状态 </summary>
    public virtual void DefaultState() { }
    /// <summary> 选中状态 </summary>
    public virtual void SelectState() { }
    /// <summary> 释放 </summary>
    public virtual void Release() => OnSelect -= UnitUIPanelItem_OnSelect;
}
