using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 缝合烘焙可视化模块
/// </summary>
public class VisualSutureBaking : ModuleVisual<DataSuture> {
    public Transform viewSpace;
    public Transform suturePrefab;//缝合
    public Transform sutureSidePrefab;//缝合边

    protected override void Awake() => ModuleCore.VisualSutureBaking = this;

    public override void UpdateVisual(DataSuture suture) {
        Create(ref suture.baking, suturePrefab, viewSpace);
        suture.baking.UpdateVisual(suture);

        UpdateVisual(suture.a, suture.baking.transform);
        UpdateVisual(suture.b, suture.baking.transform);
    }
    public override void ReleaseVisual(DataSuture data) {
        throw new System.NotImplementedException();
    }

    /// <summary> 更新缝合边 </summary>
    private void UpdateVisual(DataSutureSide sutureSide, Transform parent) {
        Create(ref sutureSide.baking, sutureSidePrefab, parent);
        sutureSide.baking.UpdateVisual(sutureSide);
    }
}
