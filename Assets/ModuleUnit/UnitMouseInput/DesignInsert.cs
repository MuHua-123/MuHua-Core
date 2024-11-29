using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignInsert : UnitMouseInput {
    /// <summary> 查询边交点 </summary>
    public UnitFind<SideIntersectPoint> find = new FindSideIntersectPoint();

    public override void MouseDown(DataMouseInput data) {
        if (!find.Find(data.WorldPosition, out SideIntersectPoint sip)) { return; }
        Insert(sip.side, sip.side.plate, sip.side.aPoint, sip.side.bPoint, sip.intersectPoint);
    }

    private void Insert(DataPlateSide side, DataPlate plate, DataPlatePoint aPoint, DataPlatePoint bPoint, Vector3 position) {
        //创建新的点
        DataPlatePoint newPoint = new DataPlatePoint(plate);
        newPoint.position = position - plate.dataDesign.position;
        //改变关联的边B点，重置贝塞尔曲线
        side.bPoint = newPoint;
        side.OneRankBezier();
        //创建新的边
        DataPlateSide newSide = new DataPlateSide(plate);
        newSide.aPoint = newPoint;
        newSide.bPoint = bPoint;
        newSide.OneRankBezier();
        //插入边
        int sideIndex = plate.plateSides.IndexOf(side);
        plate.plateSides.Insert(sideIndex + 1, newSide);
        //插入点
        int pointIndex = plate.platePoints.IndexOf(aPoint);
        plate.platePoints.Insert(pointIndex + 1, newPoint);
        //更新数据
        plate.UpdateVisual();
    }
}
