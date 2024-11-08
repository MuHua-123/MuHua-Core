using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIUBezierMobile : ModuleViewInputUnit {
    private Vector3 mousePosition;
    private Vector3 originalPosition;
    private readonly ModuleViewCamera viewCamera;
    private ModulePlateDesign PlateDesign => ModuleCore.I.PlateDesign;
    public VIUBezierMobile(ModuleViewCamera viewCamera) {
        this.viewCamera = viewCamera;
    }
    public override void DownMouse(DataMouseInput data) {
        PlateDesign.SelectBezierPoint(data.ScreenPosition);
        if (!PlateDesign.IsValidBezierPoint) { return; }
        mousePosition = viewCamera.ScreenToWorldPosition(data.ScreenPosition);
        originalPosition = PlateDesign.BezierPointPosition;
    }
    public override void DragMouse(DataMouseInput data) {
        if (!PlateDesign.IsValidBezierPoint) { return; }
        Vector3 current = viewCamera.ScreenToWorldPosition(data.ScreenPosition);
        Vector3 offset = current - mousePosition;
        PlateDesign.ChangeBezierPoint(originalPosition + offset);
    }
    public override void ReleaseMouse(DataMouseInput data) {
        PlateDesign.ReleaseBezierPoint();
    }
    public override void ScrollWheel(DataMouseInput data) {
        PlateDesign.ReleaseBezierPoint();
    }
}
