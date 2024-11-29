using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 版片可视化模块
/// </summary>
//public class VisualDesign : ModuleVisual<DataPlate> {
//    public Transform viewSpace;
//    public Transform platePrefab;//板片
//    public Transform pointPrefab;//点
//    public Transform sidePrefab;//边
//    public Transform suturePrefab;//缝合
//    public Transform sutureSidePrefab;//缝合边

//    protected override void Awake() => ModuleCore.VisualDesign = this;

//    public override void UpdateVisual(DataPlate plate) {
//        //更新板片
//        Create(ref plate.design, platePrefab, viewSpace);
//        plate.design.UpdateVisual(plate);
//        //子数据父对象
//        Transform parent = plate.design.transform;
//        //更新点
//        plate.points.ForEach(obj => UpdateVisual(obj, parent));
//        //更新线段
//        plate.sides.ForEach(obj => UpdateVisual(obj, parent));
//    }
//    public override void ReleaseVisual(DataPlate data) {
//        throw new System.NotImplementedException();
//    }

//    private void UpdateVisual(DataPoint point, Transform parent) {
//        Create(ref point.visual, pointPrefab, parent);
//        point.visual.UpdateVisual(point);
//    }
//    private void UpdateVisual(DataSide side, Transform parent) {
//        Create(ref side.design, sidePrefab, parent);
//        side.design.UpdateVisual(side);
//        //更新缝合线
//        if (side.suture == null) { return; }
//        side.suture.Update();
//        UpdateVisual(side.suture, viewSpace);
//    }
//    /// <summary> 更新缝合数据 </summary>
//    private void UpdateVisual(DataSuture suture, Transform parent) {
//        Create(ref suture.design, suturePrefab, parent);
//        UpdateVisual(suture.a, suture.design.transform);
//        UpdateVisual(suture.b, suture.design.transform);
//        suture.design.UpdateVisual(suture);
//    }
//    /// <summary> 更新缝合边 </summary>
//    private void UpdateVisual(DataSutureSide sutureSide, Transform parent) {
//        Create(ref sutureSide.design, sutureSidePrefab, parent);
//        sutureSide.design.UpdateVisual(sutureSide);
//    }
//}
