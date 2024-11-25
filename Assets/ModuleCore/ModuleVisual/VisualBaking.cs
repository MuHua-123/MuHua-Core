using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualBaking : ModuleVisual<DataPlate> {
    public Transform viewSpace;
    public Transform platePrefab;//板片
    public Transform suturePrefab;//缝合
    public Transform sutureSidePrefab;//缝合边

    protected override void Awake() => ModuleCore.VisualBaking = this;

    public override void UpdateVisual(DataPlate plate) {
        //更新板片
        Create(ref plate.baking, platePrefab, viewSpace);
        plate.baking.UpdateVisual(plate);
        //子数据父对象
        Transform parent = plate.design.transform;
        //更新线段
        plate.sides.ForEach(obj => UpdateVisual(obj, parent));
    }
    public override void ReleaseVisual(DataPlate data) {
        throw new System.NotImplementedException();
    }

    private void UpdateVisual(DataSide side, Transform parent) {
        //Create(ref side.design, sidePrefab, parent);
        //side.design.UpdateVisual(side);
        //更新缝合线
        if (side.suture == null) { return; }
        //side.suture.Update();
        UpdateVisual(side.suture, viewSpace);
    }
    /// <summary> 更新缝合数据 </summary>
    private void UpdateVisual(DataSuture suture, Transform parent) {
        Create(ref suture.baking, suturePrefab, parent);
        UpdateVisual(suture.a, suture.baking.transform);
        UpdateVisual(suture.b, suture.baking.transform);
        suture.baking.UpdateVisual(suture);
    }
    /// <summary> 更新缝合边 </summary>
    private void UpdateVisual(DataSutureSide sutureSide, Transform parent) {
        Create(ref sutureSide.baking, sutureSidePrefab, parent);
        sutureSide.baking.UpdateVisual(sutureSide);
    }
}
