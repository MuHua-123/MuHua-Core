using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIUCameraMobile : ModuleViewInputUnit {
    private Vector3 mousePosition;
    private Vector3 originalPosition;
    private readonly ModuleViewCamera viewCamera;
    public VIUCameraMobile(ModuleViewCamera viewCamera) {
        this.viewCamera = viewCamera; 
    }
    public override void DownMouse(DataMouseInput data) {
        mousePosition = viewCamera.ScreenToWorldPosition(data.ScreenPosition);
        originalPosition = viewCamera.Position;
    }
    public override void DragMouse(DataMouseInput data) {
        Vector3 current = viewCamera.ScreenToWorldPosition(data.ScreenPosition);
        Vector3 offset = current - mousePosition;
        viewCamera.Position = originalPosition + offset;
    }
    public override void ReleaseMouse(DataMouseInput data) {

    }
    public override void ScrollWheel(DataMouseInput data) {
        
    }
}
