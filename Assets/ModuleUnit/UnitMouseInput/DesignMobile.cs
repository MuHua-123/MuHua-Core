using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignMobile : UnitMouseInput {
    /// <summary> 设计视图相机模块 </summary>
    public ModuleViewCamera ViewCamera => ModuleCore.ViewCameraDesign;
    /// <summary> 查询贝塞尔点算法模块 </summary>
    public ModuleFind<DataBezier> FindBezier => ModuleCore.FindBezier;

    private DataBezier bezier;
    private Vector3 mousePosition;
    private Vector3 originalPosition;
    private ModulePrefab<DataPlatePoint> prefabPoint;
    private ModulePrefab<DataPlate> prefabPlate;

    public override void MouseDown(DataMouseInput data) {
        //判断是否选中贝塞尔点
        if (FindBezier.Find(data.WorldPosition, out bezier)) {
            RecordBezier(data.ScreenPosition); return;
        }
        //判断是否选中点
        if (ViewCamera.ScreenToWorldObjectParent(data.ScreenPosition, out prefabPoint)) {
            RecordPoint(data.ScreenPosition); return;
        }
        //判断是否选中板片
        if (ViewCamera.ScreenToWorldObjectParent(data.ScreenPosition,out prefabPlate)) {
            RecordPlate(data.ScreenPosition); return;
        }
        RecordCamera(data.ScreenPosition);
    }
    public override void MouseDrag(DataMouseInput data) {
        Vector3 original = ViewCamera.ScreenToWorldPosition(mousePosition);
        Vector3 current = data.WorldPosition;
        Vector3 offset = current - original;

        if (bezier != null) { MobileBezier(offset); return; }
        if (prefabPoint != null) { MobilePoint(offset); return; }
        if (prefabPlate != null) { MobilePlate(offset); return; }
        MobileCamera(offset);
    }

    //贝塞尔
    private void RecordBezier(Vector3 screenPosition) {
        mousePosition = screenPosition;
        originalPosition = bezier.position;
    }
    private void MobileBezier(Vector3 offset) {
        Vector3 position = originalPosition + offset;
        bezier.SetBezierPosition(position);
    }
    //点
    private void RecordPoint(Vector3 screenPosition) {
        mousePosition = screenPosition;
        originalPosition = prefabPoint.Value.position;
    }
    private void MobilePoint(Vector3 offset) {
        prefabPoint.Value.position = originalPosition + offset;
        prefabPoint.Value.plate.UpdateVisual();
    }
    //板片
    private void RecordPlate(Vector3 screenPosition) {
        mousePosition = screenPosition;
        originalPosition = prefabPlate.Value.dataDesign.position;
    }
    private void MobilePlate(Vector3 offset) {
        prefabPlate.Value.dataDesign.position = originalPosition + offset;
        prefabPlate.Value.UpdateVisual(false);
    }
    //相机
    private void RecordCamera(Vector3 screenPosition) {
        mousePosition = screenPosition;
        originalPosition = ViewCamera.Position;
    }
    private void MobileCamera(Vector3 offset) {
        ViewCamera.Position = originalPosition - offset;
    }
}
