using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 插入点(DataInsertPoint) 转换 点(DataPoint)
/// </summary>
//public class BuilderInsertPointToPoint : ModuleBuilder<DataInsertPoint, DataPoint> {

//    protected override void Awake() => ModuleCore.InsertPointToPoint = this;

//    public override DataPoint To(DataInsertPoint insertPoint) {
//        DataPlate plate = insertPoint.plate;
//        Vector3 position = insertPoint.position;
//        //创建新的点
//        DataPoint point = new DataPoint(insertPoint.plate);
//        point.position = position;
//        //改变关联的边B点，重置贝塞尔曲线
//        insertPoint.side.bPoint = point;
//        insertPoint.side.OneRankBezier();
//        //创建新的边
//        DataSide side = CreateDataSide(plate, point, insertPoint.bPoint);
//        //插入边
//        int sideIndex = plate.sides.IndexOf(insertPoint.side);
//        plate.sides.Insert(sideIndex + 1, side);
//        //插入点
//        int pointIndex = plate.points.IndexOf(insertPoint.aPoint);
//        plate.points.Insert(pointIndex + 1, point);
//        //更新数据
//        plate.UpdateVisual();
//        return point;
//    }

//    private DataSide CreateDataSide(DataPlate plate, DataPoint a, DataPoint b) {
//        DataSide side = new DataSide(plate);
//        side.aPoint = a;
//        side.bPoint = b;
//        side.OneRankBezier();
//        return side;
//    }
//}
