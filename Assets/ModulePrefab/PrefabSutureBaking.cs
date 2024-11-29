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

        DataSutureSideBaking aBaking = suture.a.dataBaking;
        DataSutureSideBaking bBaking = suture.b.dataBaking;
        aLineRenderer.SetPosition(0, aBaking.PointA);
        aLineRenderer.SetPosition(1, bBaking.PointA);

        bLineRenderer.SetPosition(0, aBaking.PointB);
        bLineRenderer.SetPosition(1, bBaking.PointB);
    }
}
