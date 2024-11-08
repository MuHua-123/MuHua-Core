using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIUCameraRotate : ModuleViewInputUnit {
    private float mouseRotate;
    private float originalRotate;
    private readonly ModuleViewCamera viewCamera;
    public VIUCameraRotate(ModuleViewCamera viewCamera) {
        this.viewCamera = viewCamera;
    }
    public override void DownMouse(DataMouseInput data) {
        mouseRotate = viewCamera.ScreenToViewPosition(data.ScreenPosition).x;
        originalRotate = viewCamera.EulerAngles.y;
    }
    public override void DragMouse(DataMouseInput data) {
        float current = viewCamera.ScreenToViewPosition(data.ScreenPosition).x;
        float offset = (current - mouseRotate) * 360;
        viewCamera.EulerAngles = new Vector3(0, originalRotate - offset, 0);
    }
}
