using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendingPlate : ModuleSending<DataPlate> {
    private DataPlate dataPlate;

    public override DataPlate Current => dataPlate;
    public override event Action<DataPlate> OnChange;
    public override void Change(DataPlate dataPlate) {
        this.dataPlate = dataPlate;
        OnChange?.Invoke(dataPlate);
    }

    protected override void Awake() => ModuleCore.SendingPlate = this;
}
