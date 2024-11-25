using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSutureBaking : ModulePrefab<DataSuture> {

    public LineRenderer aLineRenderer;
    public LineRenderer bLineRenderer;

    private DataSuture suture;

    public override DataSuture Value => suture;

    public override void UpdateVisual(DataSuture suture) {
        this.suture = suture;

        aLineRenderer.SetPosition(0, suture.a.BakingPosintA);
        aLineRenderer.SetPosition(1, suture.b.BakingPosintA);

        bLineRenderer.SetPosition(0, suture.a.BakingPosintB);
        bLineRenderer.SetPosition(1, suture.b.BakingPosintB);
    }
}
