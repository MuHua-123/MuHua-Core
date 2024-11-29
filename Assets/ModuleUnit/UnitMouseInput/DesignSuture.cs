using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignSuture : UnitMouseInput {
    /// <summary> 查询边交点 </summary>
    public UnitFind<SideIntersectPoint> find = new FindSideIntersectPoint();
    /// <summary> 连接可视化内容生成模块 </summary>
    public ModuleVisual<DataConnector> VisualConnector => ModuleCore.VisualConnector;

    private DataPlateSide aSide;
    private DataPlateSide bSide;
    private DataConnector connector;

    public override void MouseDown(DataMouseInput data) {
        connector = new DataConnector();
        connector.aPoint = data.WorldPosition;
        if (!find.Find(data.WorldPosition, out SideIntersectPoint sip)) { return; }
        connector.aPoint = sip.intersectPoint;
        aSide = sip.side;
    }
    public override void MouseDrag(DataMouseInput data) {
        if (connector == null) { return; }
        connector.bPoint = data.WorldPosition;
        if (find.Find(data.WorldPosition, out SideIntersectPoint sip)) {
            connector.bPoint = sip.intersectPoint;
            bSide = sip.side;
        }
        else { connector.bPoint = data.WorldPosition; }
        VisualConnector.UpdateVisual(connector);
    }
    public override void MouseRelease(DataMouseInput data) {
        if (connector == null) { return; }
        VisualConnector.ReleaseVisual(connector);
        connector = null;

        if (aSide == null || bSide == null || aSide == bSide) { return; }
        if (aSide.suture != null || bSide.suture != null) { return; }
        DataSuture suture = new DataSuture(aSide, bSide);
        aSide.suture = suture;
        bSide.suture = suture;
        aSide.plate.UpdateVisual();
    }

    //private Vector3 GetPosition(DataMouseInput data) {
    //    if (!FindSide.Find(data.WorldPosition, out bSide)) { return data.WorldPosition; }
    //    if (aSide == bSide) { return data.WorldPosition; }
    //    if (Intersect(bSide, data.WorldPosition, out Vector3 intersectPoint)) { return intersectPoint; }
    //    else { return data.WorldPosition; }
    //}
    //private bool Intersect(DataSide side, Vector3 position, out Vector3 intersectPoint) {
    //DataIntersect intersect = new DataIntersect(side, position);
    //AlgorithmSidePoint.Compute(intersect);
    //intersectPoint = intersect.intersectPoint;
    //return intersect.isIntersect;
    //}
}
