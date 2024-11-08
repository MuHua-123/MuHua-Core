using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIUEdgePointAdd : ModuleViewInputUnit {
    private readonly ModuleViewCamera viewCamera;
    private ModulePlateDesign PlateDesign => ModuleCore.I.PlateDesign;
    public VIUEdgePointAdd(ModuleViewCamera viewCamera) {
        this.viewCamera = viewCamera;
    }
    public override void DownMouse(DataMouseInput data) {
        PlateDesign.InsertEdgePoint(data.ScreenPosition);
    }
    public override void MoveMouse(DataMouseInput data) {
        //Vector3 position = viewCamera.ScreenToWorldPosition(data.ScreenPosition);
        //Vector3 worldPosition = position + viewCamera.CameraWorldPosition;
        //worldPosition.z = 0;
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPosition, range, DefaultLayerMask);
        //if (colliders.Length == 0) { return; }
        //edgePoint = colliders[0].GetComponentInParent<PrefabEdgePoint>();
    }
}
