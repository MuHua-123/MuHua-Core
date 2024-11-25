using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 连接可视化模块
/// </summary>
public class VisualConnector : ModuleVisual<DataConnector> {
    public Transform viewSpace;
    public Transform connectorPrefab;//板片

    protected override void Awake() => ModuleCore.VisualConnector = this;

    public override void UpdateVisual(DataConnector data) {
        //更新板片
        Create(ref data.visual, connectorPrefab, viewSpace);
        data.visual.UpdateVisual(data);
    }
    public override void ReleaseVisual(DataConnector data) {
        if (data.visual != null) {
            Destroy(data.visual.gameObject);
        }
    }
}
