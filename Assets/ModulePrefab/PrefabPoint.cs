using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPoint : ModulePrefab<DataPoint> {
    private DataPoint point;

    public override DataPoint Value => point;

    public override void UpdateVisual(DataPoint point) {
        this.point = point;
        transform.localPosition = point.position;
    }
}
