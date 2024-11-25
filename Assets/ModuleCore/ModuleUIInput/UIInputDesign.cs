using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 设计输入模块
/// </summary>
public class UIInputDesign : ModuleUIInput<UnitMouseInput> {
    private bool isDownMouseLeft;
    private bool isDownMouseRight;
    private UnitMouseInput leftInputUnit;
    private UnitMouseInput rightInputUnit;

    /// <summary> 设计视图相机模块 </summary>
    protected ModuleViewCamera ViewCamera => ModuleCore.ViewCameraDesign;

    public override UnitMouseInput Current => leftInputUnit;
    public override event Action<UnitMouseInput> OnChangeInput;
    public override void ChangeInput(UnitMouseInput input) {
        leftInputUnit = input;
        OnChangeInput?.Invoke(input);
    }

    protected override void Awake() {
        ModuleCore.UIInputDesign = this;
        rightInputUnit = new DesignMobile();
    }

    public override void Binding(VisualElement element) {
        element.RegisterCallback<MouseDownEvent>(MouseDown);
        element.RegisterCallback<MouseMoveEvent>(MouseMove);
        element.RegisterCallback<MouseUpEvent>(MouseRelease);
        element.RegisterCallback<MouseOutEvent>(MouseRelease);
        element.RegisterCallback<WheelEvent>(ScrollWheel);
    }

    private void MouseDown(MouseDownEvent evt) {
        DataMouseInput data = CreateData(evt.localMousePosition, 0);
        if (evt.button == 0) { leftInputUnit.MouseDown(data); isDownMouseLeft = true; }
        if (evt.button == 1) { rightInputUnit.MouseDown(data); isDownMouseRight = true; }
    }
    private void MouseMove(MouseMoveEvent evt) {
        DataMouseInput data = CreateData(evt.localMousePosition, 0);
        if (isDownMouseLeft) { leftInputUnit.MouseDrag(data); }
        if (isDownMouseRight) { rightInputUnit.MouseDrag(data); }
        leftInputUnit.MouseMove(data);
        rightInputUnit.MouseMove(data);
    }
    private void MouseRelease(MouseUpEvent evt) {
        DataMouseInput data = CreateData(evt.localMousePosition, 0);
        leftInputUnit.MouseRelease(data); isDownMouseLeft = false;
        rightInputUnit.MouseRelease(data); isDownMouseRight = false;
    }
    private void MouseRelease(MouseOutEvent evt) {
        DataMouseInput data = CreateData(evt.localMousePosition, 0);
        leftInputUnit.MouseRelease(data); isDownMouseLeft = false;
        rightInputUnit.MouseRelease(data); isDownMouseRight = false;
    }
    private void ScrollWheel(WheelEvent evt) {
        DataMouseInput data = CreateData(evt.localMousePosition, evt.delta.y);
        float size = ViewCamera.Scale + data.ScrollWheel;
        size = Mathf.Clamp(size, 0.1f, 4);
        ViewCamera.Scale = Mathf.Lerp(ViewCamera.Scale, size, Time.deltaTime * 20);
    }

    private DataMouseInput CreateData(Vector2 localMousePosition, float scrollWheel) {
        DataMouseInput data = new DataMouseInput();
        data.ScrollWheel = scrollWheel;
        data.ViewPosition = ViewCamera.ScreenToViewPosition(localMousePosition);
        data.WorldPosition = ViewCamera.ScreenToWorldPosition(localMousePosition);
        data.ScreenPosition = localMousePosition;
        return data;
    }
}