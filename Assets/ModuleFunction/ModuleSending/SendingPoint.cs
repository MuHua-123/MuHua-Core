using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendingPoint : ModuleSending<DataPoint> {
    private DataPoint dataPoint;

    public override DataPoint Current => dataPoint;
    public override event Action<DataPoint> OnChange;
    public override void Change(DataPoint dataPoint) {
        this.dataPoint = dataPoint;
        OnChange?.Invoke(dataPoint);
    }

    protected override void Awake() => ModuleCore.SendingPoint = this;
}
