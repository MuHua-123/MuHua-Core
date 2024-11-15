using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderInsertPointToPoint : ModuleBuilder<DataInsertPoint, DataPoint> {

    protected override void Awake() => ModuleCore.InsertPointToPoint = this;

    public override DataPoint To(DataInsertPoint insertPoint) {
        Vector3 position = insertPoint.position - insertPoint.plate.position;

        DataPoint point = new DataPoint(insertPoint.plate);
        point.frontBezier = DataPointTool.DefaultBezier(position, insertPoint.aPoint.position);
        point.position = position;
        point.afterBezier = DataPointTool.DefaultBezier(position, insertPoint.bPoint.position);

        int index = insertPoint.plate.points.IndexOf(insertPoint.aPoint);
        insertPoint.plate.points.Insert(index + 1, point);
        insertPoint.plate.UpdateVisual();

        return point;
    }
}
