using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIUEdgePointMobile : ModuleViewInputUnit {
    private Vector3 mousePosition;
    private Vector3 originalPosition;
    private readonly ModuleViewCamera viewCamera;
    private ModulePlateDesign PlateDesign => ModuleCore.I.PlateDesign;
    public VIUEdgePointMobile(ModuleViewCamera viewCamera) {
        this.viewCamera = viewCamera;
    }
    public override void DownMouse(DataMouseInput data) {
        PlateDesign.SelectEdgePoint(data.ScreenPosition);
        if (!PlateDesign.IsValidEdgePoint) { return; }
        mousePosition = viewCamera.ScreenToWorldPosition(data.ScreenPosition);
        originalPosition = PlateDesign.EdgePointPosition;
    }
    public override void DragMouse(DataMouseInput data) {
        if (!PlateDesign.IsValidEdgePoint) { return; }
        Vector3 current = viewCamera.ScreenToWorldPosition(data.ScreenPosition);
        Vector3 offset = current - mousePosition;
        PlateDesign.ChangeEdgePoint(originalPosition + offset);
    }
    public override void ReleaseMouse(DataMouseInput data) {
        PlateDesign.ReleaseEdgePoint();
    }
    public override void ScrollWheel(DataMouseInput data) {
        PlateDesign.ReleaseEdgePoint();
    }
}
