using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderPlatePresetsToPlate : ModuleBuilder<DataPlatePresets, DataPlate> {

    protected override void Awake() => ModuleCore.PlatePresetsToPlate = this;

    public override DataPlate To(DataPlatePresets origin) {
        DataPlate dataPlate = new DataPlate();
        dataPlate.points = ToDataPoint(dataPlate, origin.designPoints);
        return dataPlate;
    }

    private List<DataPoint> ToDataPoint(DataPlate dataPlate, List<Vector3> list) {
        List<DataPoint> points = new List<DataPoint>();

        int maxIndex = list.Count - 1;

        DataPoint start = new DataPoint(dataPlate);
        start.frontBezier = list[0] + DataPointTool.DefaultBezier(list[0], list[maxIndex]);
        start.position = list[0];
        start.afterBezier = list[0] + DataPointTool.DefaultBezier(list[0], list[1]);
        points.Add(start);

        for (int i = 1; i < maxIndex; i++) {
            DataPoint dataPoint = new DataPoint(dataPlate);
            dataPoint.frontBezier = list[i] + DataPointTool.DefaultBezier(list[i], list[i - 1]);
            dataPoint.position = list[i];
            dataPoint.afterBezier = list[i] + DataPointTool.DefaultBezier(list[i], list[i + 1]);
            points.Add(dataPoint);
        }

        DataPoint end = new DataPoint(dataPlate);
        end.frontBezier = list[maxIndex] + DataPointTool.DefaultBezier(list[maxIndex], list[maxIndex - 1]);
        end.position = list[maxIndex];
        end.afterBezier = list[maxIndex] + DataPointTool.DefaultBezier(list[maxIndex], list[0]);
        points.Add(end);

        return points;
    }
}
