using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsPresetsPlate : ModuleAssets<DataPresetsPlate> {
    protected override void Awake() {
        ModuleCore.PresetsPlateAssets = this;
    }
    public override void ForEach(Action<DataPresetsPlate> action) {
        assets.ForEach(action);
    }
}
