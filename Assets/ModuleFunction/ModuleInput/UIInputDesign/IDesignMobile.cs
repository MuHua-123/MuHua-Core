using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDesignMobile : UIInputDesignUnit {
    /// <summary> 板片资产 </summary>
    public ModuleAssets<DataPlate> AssetsPlate => ModuleCore.AssetsPlate;
    /// <summary> 查询点算法模块 </summary>
    public ModuleAlgorithm<DataFindPoint> AlgorithmFindPoint => ModuleCore.AlgorithmFindPoint;
    /// <summary> 广播查询数据模块 </summary>
    public ModuleSending<DataFindPoint> SendingFindPoint => ModuleCore.SendingFindPoint;

    private Vector3 mousePosition;
    private Vector3 originalPosition;
    private DataFindPoint findPoint;
    private void FindPoint(Vector3 localPosition) {
        findPoint = new DataFindPoint();
        findPoint.position = localPosition;
        findPoint.datas = AssetsPlate.Datas;
        AlgorithmFindPoint.Compute(findPoint);
    }

    public override void MouseDown(DataUIMouseInput data) {
        FindPoint(data.WorldPosition);
        SendingFindPoint.Change(findPoint);
        if (findPoint.IsValidPoint) { RecordPoint(data.ScreenPosition); return; }
        if (findPoint.IsValidPlate) { RecordPlate(data.ScreenPosition); return; }
        RecordCamera(data.ScreenPosition);
    }
    public override void MouseDrag(DataUIMouseInput data) {
        Vector3 original = ViewCamera.ScreenToWorldPosition(mousePosition);
        Vector3 current = data.WorldPosition;
        Vector3 offset = current - original;

        if (findPoint.IsValidPoint) { MobilePoint(offset); return; }
        if (findPoint.IsValidPlate) { MobilePlate(offset); return; }
        MobileCamera(offset);
    }

    private void RecordPoint(Vector3 screenPosition) {
        mousePosition = screenPosition;
        originalPosition = findPoint.point.position;
    }
    private void MobilePoint(Vector3 offset) {
        findPoint.point.position = originalPosition + offset;
        findPoint.plate.UpdateVisual();
    }

    private void RecordPlate(Vector3 screenPosition) {
        mousePosition = screenPosition;
        originalPosition = findPoint.plate.position;
    }
    private void MobilePlate(Vector3 offset) {
        findPoint.plate.position = originalPosition + offset;
        findPoint.plate.UpdateVisual();
    }

    private void RecordCamera(Vector3 screenPosition) {
        mousePosition = screenPosition;
        originalPosition = ViewCamera.position;
    }
    private void MobileCamera(Vector3 offset) {
        ViewCamera.position = originalPosition - offset;
    }
}
