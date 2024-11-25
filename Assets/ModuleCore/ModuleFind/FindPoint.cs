using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 查找点
/// </summary>
public class FindPoint : ModuleFind<DataPoint> {
    public readonly float FindRange = 0.01f;
    /// <summary> 板片资产 </summary>
    public ModuleAssets<DataPlate> AssetsPlate => ModuleCore.AssetsPlate;

    protected override void Awake() => ModuleCore.FindPoint = this;

    public override bool Find(Vector3 position, out DataPoint point) {
        List<DataPlate> plates = AssetsPlate.Datas;
        for (int i = 0; i < plates.Count; i++) {
            Vector3 localPosition = position - plates[i].designPosition;
            point = Find(plates[i], localPosition);
            if (point != null) { return true; }
        }
        point = null; return false;
    }

    /// <summary> 查询匹配的点 </summary>
    private DataPoint Find(DataPlate plate, Vector3 localPosition) {
        List<DataPoint> points = plate.points;
        for (int i = 0; i < points.Count; i++) {
            float distance = Vector3.Distance(points[i].position, localPosition);
            if (distance > FindRange) { continue; }
            return points[i];
        }
        return null;
    }
}
