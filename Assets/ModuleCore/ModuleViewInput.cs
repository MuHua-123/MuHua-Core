using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModuleViewInput : MonoBehaviour {
    /// <summary> 主键输入模块类型 </summary>
    public abstract event Action<Type> OnInputType;
    /// <summary> 必须要初始化 </summary>
    protected abstract void Awake();
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;

    public abstract void SetPrimaryKeyInput<T>(T inputUnit) where T : ModuleViewInputUnit;

    /// <summary> 按下鼠标左键 </summary>
    public abstract void DownLeftMouse(DataMouseInput data);
    /// <summary> 拖拽鼠标左键 </summary>
    public abstract void DragLeftMouse(DataMouseInput data);
    /// <summary> 移动鼠标左键 </summary>
    public abstract void MoveLeftMouse(DataMouseInput data);
    /// <summary> 释放鼠标左键 </summary>
    public abstract void ReleaseLeftMouse(DataMouseInput data);

    /// <summary> 按下鼠标右键 </summary>
    public abstract void DownRightMouse(DataMouseInput data);
    /// <summary> 拖拽鼠标右键 </summary>
    public abstract void DragRightMouse(DataMouseInput data);
    /// <summary> 移动鼠标右键 </summary>
    public abstract void MoveRightMouse(DataMouseInput data);
    /// <summary> 释放鼠标右键 </summary>
    public abstract void ReleaseRightMouse(DataMouseInput data);

    /// <summary> 按下鼠标中键 </summary>
    public abstract void DownMiddleMouse(DataMouseInput data);
    /// <summary> 拖拽鼠标中键 </summary>
    public abstract void DragMiddleMouse(DataMouseInput data);
    /// <summary> 移动鼠标中键 </summary>
    public abstract void MoveMiddleMouse(DataMouseInput data);
    /// <summary> 释放鼠标中键 </summary>
    public abstract void ReleaseMiddleMouse(DataMouseInput data);

    /// <summary> 鼠标滚轮 </summary>
    public abstract void ScrollWheel(DataMouseInput data);
}