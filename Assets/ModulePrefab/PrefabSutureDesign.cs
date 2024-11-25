using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSutureDesign : ModulePrefab<DataSuture> {

    public LineRenderer aLineRenderer;
    public LineRenderer bLineRenderer;

    private DataSuture suture;

    public override DataSuture Value => suture;

    public override void UpdateVisual(DataSuture suture) {
        this.suture = suture;

        aLineRenderer.SetPosition(0, suture.a.DesignPosintA);
        aLineRenderer.SetPosition(1, suture.b.DesignPosintA);

        bLineRenderer.SetPosition(0, suture.a.DesignPosintB);
        bLineRenderer.SetPosition(1, suture.b.DesignPosintB);
    }
}
