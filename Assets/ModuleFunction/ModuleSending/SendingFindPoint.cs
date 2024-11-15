using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendingFindPoint : ModuleSending<DataFindPoint> {
    private DataFindPoint findPoint;

    public override DataFindPoint Current => findPoint;
    public override event Action<DataFindPoint> OnChange;
    public override void Change(DataFindPoint findPoint) {
        this.findPoint = findPoint;
        OnChange?.Invoke(findPoint);
    }

    protected override void Awake() => ModuleCore.SendingFindPoint = this;
}
