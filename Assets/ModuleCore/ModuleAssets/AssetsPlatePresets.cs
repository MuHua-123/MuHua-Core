using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsPlatePresets : ModuleAssets<DataPlatePresets> {
    [SerializeField] private List<DataPlatePresets> assets;

    public override int Count => assets.Count;
    public override List<DataPlatePresets> Datas => assets;

    protected override void Awake() => ModuleCore.AssetsPlatePresets = this;

    public override void Add(DataPlatePresets data) => assets.Add(data);
    public override void Remove(DataPlatePresets data) => assets.Remove(data);
    public override DataPlatePresets Find(int index) => assets.LoopIndex(index);
    public override void ForEach(Action<DataPlatePresets> action) => assets.ForEach(action);
}
