using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakingRotate : UnitMouseInput {
    /// <summary> 设计视图相机模块 </summary>
    public ModuleViewCamera ViewCamera => ModuleCore.ViewCameraBaking;

    private Vector3 mousePosition;
    private Vector3 originalEulerAngles;

    public override void MouseDown(DataMouseInput data) {
        mousePosition = data.ViewPosition;
        originalEulerAngles = ViewCamera.EulerAngles;
    }
    public override void MouseDrag(DataMouseInput data) {
        float offsetX = data.ViewPosition.x - mousePosition.x;
        float offsetY = data.ViewPosition.y - mousePosition.y;
        ViewCamera.EulerAngles = originalEulerAngles + new Vector3(-offsetY, offsetX , 0) * 360;
    }
}
