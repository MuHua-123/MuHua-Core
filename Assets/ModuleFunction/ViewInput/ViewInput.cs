using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewInput : ModuleViewInput {
    protected ModuleViewInputUnit leftViewInputUnit;
    protected ModuleViewInputUnit rightViewInputUnit;
    protected ModuleViewInputUnit middleViewInputUnit;
    protected ModuleViewInputUnit scrollViewInputUnit;

    public override event Action<Type> OnInputType;

    protected override void Awake() { }

    protected virtual void InputType(Type type) {
        OnInputType?.Invoke(type);
    }
    public override void SetPrimaryKeyInput<T>(T inputUnit) {
        leftViewInputUnit = inputUnit;
        InputType(inputUnit.GetType());
    }

    public override void DownLeftMouse(DataMouseInput data) {
        leftViewInputUnit?.DownMouse(data);
    }
    public override void DragLeftMouse(DataMouseInput data) {
        leftViewInputUnit?.DragMouse(data);
    }
    public override void MoveLeftMouse(DataMouseInput data) {
        leftViewInputUnit?.MoveMouse(data);
    }
    public override void ReleaseLeftMouse(DataMouseInput data) {
        leftViewInputUnit?.ReleaseMouse(data);
    }

    public override void DownRightMouse(DataMouseInput data) {
        rightViewInputUnit?.DownMouse(data);
    }
    public override void DragRightMouse(DataMouseInput data) {
        rightViewInputUnit?.DragMouse(data);
    }
    public override void MoveRightMouse(DataMouseInput data) {
        rightViewInputUnit?.MoveMouse(data);
    }
    public override void ReleaseRightMouse(DataMouseInput data) {
        rightViewInputUnit?.ReleaseMouse(data);
    }

    public override void DownMiddleMouse(DataMouseInput data) {
        middleViewInputUnit?.DownMouse(data);
    }
    public override void DragMiddleMouse(DataMouseInput data) {
        middleViewInputUnit?.DragMouse(data);
    }
    public override void MoveMiddleMouse(DataMouseInput data) {
        middleViewInputUnit?.MoveMouse(data);
    }
    public override void ReleaseMiddleMouse(DataMouseInput data) {
        middleViewInputUnit?.ReleaseMouse(data);
    }

    public override void ScrollWheel(DataMouseInput data) {
        scrollViewInputUnit?.ScrollWheel(data);
    }
}
