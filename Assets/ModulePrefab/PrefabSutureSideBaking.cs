using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSutureSideBaking : ModulePrefab<DataSutureSide> {

    private DataSutureSide sutureSide;

    public override DataSutureSide Value => sutureSide;
    public LineRenderer lineRenderer => GetComponent<LineRenderer>();

    public override void UpdateVisual(DataSutureSide sutureSide) {
        this.sutureSide = sutureSide;

        DataSutureSideBaking baking = sutureSide.dataBaking;
        lineRenderer.positionCount = baking.positions.Length;
        lineRenderer.SetPositions(baking.positions);
    }
}