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
        dataPlate.points = ToDataPoint(dataPlate, origin.designPoints);
        dataPlate.sides = ToDataSide(dataPlate, dataPlate.points);
        return dataPlate;
    }

    private List<DataPoint> ToDataPoint(DataPlate dataPlate, List<Vector3> list) {
        List<DataPoint> points = new List<DataPoint>();
        for (int i = 0; i < list.Count; i++) {
            DataPoint point = new DataPoint(dataPlate);
            point.position = list[i];
            points.Add(point);
        }
        return points;
    }
    private List<DataSide> ToDataSide(DataPlate dataPlate, List<DataPoint> list) {
        List<DataSide> sides = new List<DataSide>();
        for (int i = 0; i < list.Count; i++) {
            DataSide side = new DataSide(dataPlate);
            side.aPoint = list.LoopIndex(i + 0);
            side.bPoint = list.LoopIndex(i + 1);
            side.OneRankBezier();
            sides.Add(side);
        }
        return sides;
    }
}
