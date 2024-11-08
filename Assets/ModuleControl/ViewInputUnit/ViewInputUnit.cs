using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModuleViewInputUnit {
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;
    /// <summary> 按下鼠标左键 </summary>
    public virtual void DownMouse(DataMouseInput data) { }
    /// <summary> 拖拽鼠标左键 </summary>
    public virtual void DragMouse(DataMouseInput data) { }
    /// <summary> 释放鼠标左键 </summary>
    public virtual void ReleaseMouse(DataMouseInput data) { }
    /// <summary> 移动鼠标 </summary>
    public virtual void MoveMouse(DataMouseInput data) { }
    /// <summary> 鼠标滚轮 </summary>
    public virtual void ScrollWheel(DataMouseInput data) { }
}
