using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 板片预设数据(DataPlatePresets) 转换 板片数据(DataPlate)
/// </summary>
public class BuilderPlatePresetsToPlate : ModuleBuilder<DataPlatePresets, DataPlate> {

    protected override void Awake() => ModuleCore.PlatePresetsToPlate = this;

    public override DataPlate To(DataPlatePresets origin) {
        DataPlate dataPlate = new DataPlate();
        dataPlate.platePoints = ToDataPoint(dataPlate, origin.designPoints);
        dataPlate.plateSides = ToDataSide(dataPlate, dataPlate.platePoints);
        return dataPlate;
    }

    private List<DataPlatePoint> ToDataPoint(DataPlate dataPlate, List<Vector3> list) {
        List<DataPlatePoint> points = new List<DataPlatePoint>();
        for (int i = 0; i < list.Count; i++) {
            DataPlatePoint point = new DataPlatePoint(dataPlate);
            point.position = list[i];
            points.Add(point);
        }
        return points;
    }
    private List<DataPlateSide> ToDataSide(DataPlate dataPlate, List<DataPlatePoint> list) {
        List<DataPlateSide> sides = new List<DataPlateSide>();
        for (int i = 0; i < list.Count; i++) {
            DataPlateSide side = new DataPlateSide(dataPlate);
            side.aPoint = list.LoopIndex(i + 0);
            side.bPoint = list.LoopIndex(i + 1);
            side.OneRankBezier();
            sides.Add(side);
        }
        return sides;
    }
}
