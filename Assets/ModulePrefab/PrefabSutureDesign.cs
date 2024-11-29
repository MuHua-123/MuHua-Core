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

        DataSutureSideDesign aDesign = suture.a.dataDesign;
        DataSutureSideDesign bDesign = suture.b.dataDesign;
        aLineRenderer.SetPosition(0, aDesign.PointA);
        aLineRenderer.SetPosition(1, bDesign.PointA);

        bLineRenderer.SetPosition(0, aDesign.PointB);
        bLineRenderer.SetPosition(1, bDesign.PointB);
    }
}
