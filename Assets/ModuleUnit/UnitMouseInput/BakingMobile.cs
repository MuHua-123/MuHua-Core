using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakingMobile : UnitMouseInput {
    /// <summary> 设计视图相机模块 </summary>
    public ModuleViewCamera ViewCamera => ModuleCore.ViewCameraBaking;

    private Vector3 mousePosition;
    private Vector3 originalPosition;

    public override void MouseDown(DataMouseInput data) {
        mousePosition = data.ScreenPosition;
        originalPosition = ViewCamera.Position;
    }
    public override void MouseDrag(DataMouseInput data) {
        Vector3 original = ViewCamera.ScreenToWorldPosition(mousePosition);
        Vector3 current = data.WorldPosition;
        Vector3 offset = current - original;
        Vector3 up = ViewCamera.Up * offset.y;
        Vector3 right = ViewCamera.Right * offset.x;
        ViewCamera.Position = originalPosition + up + right;
    }
}
