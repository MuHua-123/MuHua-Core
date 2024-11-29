using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 缝合设计可视化模块
/// </summary>
public class VisualSutureDesign : ModuleVisual<DataSuture> {
    public Transform viewSpace;
    public Transform suturePrefab;//缝合
    public Transform sutureSidePrefab;//缝合边

    protected override void Awake() => ModuleCore.VisualSutureDesign = this;

    public override void UpdateVisual(DataSuture suture) {
        Create(ref suture.design, suturePrefab, viewSpace);
        suture.design.UpdateVisual(suture);

        UpdateVisual(suture.a, suture.design.transform);
        UpdateVisual(suture.b, suture.design.transform);
    }
    public override void ReleaseVisual(DataSuture data) {
        throw new System.NotImplementedException();
    }

    /// <summary> 更新缝合边 </summary>
    private void UpdateVisual(DataSutureSide sutureSide, Transform parent) {
        Create(ref sutureSide.design, sutureSidePrefab, parent);
        sutureSide.design.UpdateVisual(sutureSide);
    }
}
