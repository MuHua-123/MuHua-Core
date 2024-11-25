using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 板片管理模块
/// </summary>
public class AssetsPlate : ModuleAssets<DataPlate> {
    private List<DataPlate> dataPlates = new List<DataPlate>();

    /// <summary> 视图相机模块 </summary>
    private ModuleViewCamera ViewCameraDesign => ModuleCore.ViewCameraDesign;

    public override int Count => dataPlates.Count;
    public override List<DataPlate> Datas => dataPlates;

    protected override void Awake() => ModuleCore.AssetsPlate = this;

    public override void Add(DataPlate data) {
        if (dataPlates.Contains(data)) { return; }
        dataPlates.Add(data);
        //初始化参数
        data.designPosition = ViewCameraDesign.CameraPosition;
        data.bakingPosition = ViewCameraDesign.CameraPosition;
        //生成可视化内容
        data.UpdateVisual();
    }
    public override void Remove(DataPlate data) {
        if (!dataPlates.Contains(data)) { return; }
        dataPlates.Remove(data);
    }
    public override DataPlate Find(int index) {
        return dataPlates.LoopIndex(index);
    }
    public override void ForEach(Action<DataPlate> action) {
        dataPlates.ForEach(action);
    }
}
