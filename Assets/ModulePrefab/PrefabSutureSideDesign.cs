using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSutureSideDesign : ModulePrefab<DataSutureSide> {

    private DataSutureSide sutureSide;

    public override DataSutureSide Value => sutureSide;
    public LineRenderer lineRenderer => GetComponent<LineRenderer>();

    public override void UpdateVisual(DataSutureSide sutureSide) {
        this.sutureSide = sutureSide;

        lineRenderer.positionCount = sutureSide.designPositions.Length;
        lineRenderer.SetPositions(sutureSide.designPositions);
    }
}
