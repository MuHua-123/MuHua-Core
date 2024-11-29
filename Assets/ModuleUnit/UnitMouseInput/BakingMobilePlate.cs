using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakingMobilePlate : UnitMouseInput {
    /// <summary> 安排点图层遮罩 </summary>
    private readonly LayerMask Arrange = LayerMaskTool.Arrange;
    /// <summary> 烘焙视图相机模块 </summary>
    public ModuleViewCamera ViewCamera => ModuleCore.ViewCameraBaking;

    private Vector3 mousePosition;
    private Vector3 originalPosition;
    private FixedArrange arrange;
    private ModulePrefab<DataPlate> platePrefab;

    public override void MouseDown(DataMouseInput data) {
        if (!ViewCamera.ScreenToWorldObject(data.ScreenPosition, out platePrefab)) { return; }
        mousePosition = data.ScreenPosition;
        originalPosition = platePrefab.transform.localPosition;
        platePrefab.Value.dataBaking.position = originalPosition;
        ModuleCore.BakingMobilePlate(platePrefab.Value);
    }
    public override void MouseDrag(DataMouseInput data) {
        if (platePrefab == null) { return; }
        if (ViewCamera.ScreenToWorldObjectParent(data.ScreenPosition, out arrange, Arrange)) {
            platePrefab.Value.arrange = arrange;
            platePrefab.Value.dataBaking.position = arrange.transform.localPosition;
            platePrefab.Value.dataBaking.eulerAngles = arrange.transform.localEulerAngles;
        }
        else { Mobile(data.WorldPosition); }
        platePrefab.Value.UpdateVisual(false);
    }
    public override void MouseRelease(DataMouseInput data) {
        ModuleCore.BakingMobilePlate(null);
    }

    private void Mobile(Vector3 worldPosition) {
        platePrefab.Value.arrange = null;
        Vector3 original = ViewCamera.ScreenToWorldPosition(mousePosition);
        Vector3 current = worldPosition;
        Vector3 offset = current - original;
        Vector3 up = ViewCamera.Up * offset.y;
        Vector3 right = ViewCamera.Right * offset.x;
        platePrefab.Value.dataBaking.position = originalPosition - up - right;
    }
}
