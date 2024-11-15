using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 设计输入模块
/// </summary>
public class UIInputDesign : ModuleUIInput<UIInputDesignUnit> {
    private bool isDownMouseLeft;
    private bool isDownMouseRight;
    private bool isDownMouseMiddle;
    private UIInputDesignUnit leftInputUnit;
    private UIInputDesignUnit rightInputUnit;
    private UIInputDesignUnit middleInputUnit;
    private UIInputDesignUnit scrollInputUnit;

    /// <summary> 设计视图相机模块 </summary>
    protected ModuleViewCamera ViewCamera => ModuleCore.ViewCameraDesign;

    public override UIInputDesignUnit Current => leftInputUnit;
    public override event Action<UIInputDesignUnit> OnChangeInput;
    public override void ChangeInput(UIInputDesignUnit input) {
        leftInputUnit = input;
        OnChangeInput?.Invoke(input);
    }

    protected override void Awake() {
        ModuleCore.UIInputDesign = this;
        rightInputUnit = new IDesignMobile();
        middleInputUnit = new IDesignScaleCamera(); 
        scrollInputUnit = new IDesignScaleCamera(); 
    }

    public override void Binding(VisualElement element) {
        element.RegisterCallback<MouseDownEvent>(MouseDown);
        element.RegisterCallback<MouseMoveEvent>(MouseMove);
        element.RegisterCallback<MouseUpEvent>(MouseRelease);
        element.RegisterCallback<MouseOutEvent>(MouseRelease);
        element.RegisterCallback<WheelEvent>(ScrollWheel);
    }

    private void MouseDown(MouseDownEvent evt) {
        DataUIMouseInput data = CreateData(evt.localMousePosition, 0);
        if (evt.button == 0) { leftInputUnit.MouseDown(data); isDownMouseLeft = true; }
        if (evt.button == 1) { rightInputUnit.MouseDown(data); isDownMouseRight = true; }
        if (evt.button == 2) { middleInputUnit.MouseDown(data); isDownMouseMiddle = true; }
    }
    private void MouseMove(MouseMoveEvent evt) {
        DataUIMouseInput data = CreateData(evt.localMousePosition, 0);
        if (isDownMouseLeft) { leftInputUnit.MouseDrag(data); }
        if (isDownMouseRight) { rightInputUnit.MouseDrag(data); }
        if (isDownMouseMiddle) { middleInputUnit.MouseDrag(data); }
        if (evt.button == 0) { leftInputUnit.MouseMove(data); }
        if (evt.button == 1) { rightInputUnit.MouseMove(data); }
        if (evt.button == 2) { middleInputUnit.MouseMove(data); }
    }
    private void MouseRelease(MouseUpEvent evt) {
        DataUIMouseInput data = CreateData(evt.localMousePosition, 0);
        leftInputUnit.MouseRelease(data); isDownMouseLeft = false;
        rightInputUnit.MouseRelease(data); isDownMouseRight = false;
        middleInputUnit.MouseRelease(data); isDownMouseMiddle = false;
    }
    private void MouseRelease(MouseOutEvent evt) {
        DataUIMouseInput data = CreateData(evt.localMousePosition, 0);
        leftInputUnit.MouseRelease(data); isDownMouseLeft = false;
        rightInputUnit.MouseRelease(data); isDownMouseRight = false;
        middleInputUnit.MouseRelease(data); isDownMouseMiddle = false;
    }
    private void ScrollWheel(WheelEvent evt) {
        DataUIMouseInput data = CreateData(evt.localMousePosition, evt.delta.y);
        scrollInputUnit.ScrollWheel(data);
    }

    private DataUIMouseInput CreateData(Vector2 localMousePosition, float scrollWheel) {
        DataUIMouseInput data = new DataUIMouseInput();
        data.ScrollWheel = scrollWheel;
        data.ViewPosition = ViewCamera.ScreenToViewPosition(localMousePosition);
        data.WorldPosition = ViewCamera.ScreenToWorldPosition(localMousePosition);
        data.ScreenPosition = localMousePosition;
        return data;
    }
}
public abstract class UIInputDesignUnit {
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;
    /// <summary> 设计视图相机模块 </summary>
    protected ModuleViewCamera ViewCamera => ModuleCore.ViewCameraDesign;

    /// <summary> 按下鼠标 </summary>
    public virtual void MouseDown(DataUIMouseInput data) { }
    /// <summary> 拖拽鼠标 </summary>
    public virtual void MouseDrag(DataUIMouseInput data) { }
    /// <summary> 移动鼠标 </summary>
    public virtual void MouseMove(DataUIMouseInput data) { }
    /// <summary> 释放鼠标 </summary>
    public virtual void MouseRelease(DataUIMouseInput data) { }
    /// <summary> 鼠标滚轮 </summary>
    public virtual void ScrollWheel(DataUIMouseInput data) { }
}