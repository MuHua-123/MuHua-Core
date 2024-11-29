using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPoint : ModulePrefab<DataPlatePoint> {
    private DataPlatePoint point;

    public override DataPlatePoint Value => point;

    public override void UpdateVisual(DataPlatePoint point) {
        this.point = point;
        transform.localPosition = point.position;
    }
}
