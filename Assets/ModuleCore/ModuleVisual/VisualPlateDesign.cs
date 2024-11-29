using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 版片可视化模块
/// </summary>
public class VisualPlateDesign : ModuleVisual<DataPlate> {
    public Transform viewSpace;
    public Transform platePrefab;//板片
    public Transform pointPrefab;//点
    public Transform sidePrefab;//边

    protected override void Awake() => ModuleCore.VisualPlateDesign = this;

    public override void UpdateVisual(DataPlate plate) {
        //更新板片
        Create(ref plate.designPrefab, platePrefab, viewSpace);
        plate.designPrefab.UpdateVisual(plate);
        //子数据父对象
        Transform parent = plate.designPrefab.transform;
        //更新点
        plate.platePoints.ForEach(obj => UpdateVisual(obj, parent));
        //更新线段
        plate.plateSides.ForEach(obj => UpdateVisual(obj, parent));
    }
    public override void ReleaseVisual(DataPlate data) {
        throw new System.NotImplementedException();
    }

    private void UpdateVisual(DataPlatePoint point, Transform parent) {
        Create(ref point.visual, pointPrefab, parent);
        point.visual.UpdateVisual(point);
    }
    private void UpdateVisual(DataPlateSide side, Transform parent) {
        Create(ref side.designPrefab, sidePrefab, parent);
        side.designPrefab.UpdateVisual(side);
        //更新缝合线
        if (side.suture != null) { side.suture.UpdateVisual(); }
    }
}
