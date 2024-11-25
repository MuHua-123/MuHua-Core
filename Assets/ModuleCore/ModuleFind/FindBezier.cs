using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 查询贝塞尔点
/// </summary>
public class FindBezier : ModuleFind<DataBezier> {
    public readonly float FindRange = 0.02f;
    /// <summary> 板片资产 </summary>
    public ModuleAssets<DataPlate> AssetsPlate => ModuleCore.AssetsPlate;

    protected override void Awake() => ModuleCore.FindBezier = this;

    public override bool Find(Vector3 position, out DataBezier bezier) {
        List<DataPlate> plates = AssetsPlate.Datas;
        for (int i = 0; i < plates.Count; i++) {
            Vector3 localPosition = position - plates[i].designPosition;
            bezier = Find(plates[i], localPosition);
            if (bezier != null) { return true; }
        }
        bezier = null; return false;
    }

    /// <summary> 查询匹配的边 </summary>
    private DataBezier Find(DataPlate plate, Vector3 localPosition) {
        for (int i = 0; i < plate.sides.Count; i++) {
            DataBezier bezier = Find(plate.sides[i], localPosition);
            if (bezier != null) { return bezier; }
        }
        return null;
    }
    /// <summary> 查询匹配的边 </summary>
    private DataBezier Find(DataSide side, Vector3 localPosition) {
        if (side.bezier == Bezier.一阶) { return null; }
        float aDis = Vector3.Distance(side.aBezier, localPosition);
        if (aDis < FindRange) { return new DataBezier() { isA = true, side = side }; }
        float bDis = Vector3.Distance(side.bBezier, localPosition);
        if (bDis < FindRange) { return new DataBezier() { isA = false, side = side }; }
        return null;
    }
}
