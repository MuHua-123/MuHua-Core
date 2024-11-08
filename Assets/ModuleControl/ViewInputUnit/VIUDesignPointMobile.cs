using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIUDesignPointMobile : ModuleViewInputUnit {
    private Vector3 mousePosition;
    private Vector3 originalPosition;
    private readonly ModuleViewCamera viewCamera;
    private ModulePlateDesign PlateDesign => ModuleCore.I.PlateDesign;
    public VIUDesignPointMobile(ModuleViewCamera viewCamera) {
        this.viewCamera = viewCamera; 
    }
    public override void DownMouse(DataMouseInput data) {
        PlateDesign.SelectDesignPoint(data.ScreenPosition);
        if (!PlateDesign.IsValidDesignPoint) { return; }
        mousePosition = viewCamera.ScreenToWorldPosition(data.ScreenPosition);
        originalPosition = PlateDesign.DesignPointPosition;
    }
    public override void DragMouse(DataMouseInput data) {
        if (!PlateDesign.IsValidDesignPoint) { return; }
        Vector3 current = viewCamera.ScreenToWorldPosition(data.ScreenPosition);
        Vector3 offset = current - mousePosition;
        PlateDesign.ChangeDesignPoint(originalPosition + offset);
    }
    public override void ReleaseMouse(DataMouseInput data) {
        PlateDesign.ReleaseDesignPoint();
    }
    public override void ScrollWheel(DataMouseInput data) {
        PlateDesign.ReleaseDesignPoint();
    }
}
