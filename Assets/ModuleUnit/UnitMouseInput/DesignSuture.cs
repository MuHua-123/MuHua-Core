using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignSuture : UnitMouseInput {
    /// <summary> 设计视图相机模块 </summary>
    public ModuleViewCamera ViewCamera => ModuleCore.ViewCameraDesign;
    /// <summary> 查询点算法模块 </summary>
    public ModuleFind<DataSide> FindSide => ModuleCore.FindSide;
    /// <summary> 计算位置到边上最近的点 </summary>
    public ModuleAlgorithm<DataIntersect> AlgorithmSidePoint => ModuleCore.AlgorithmSidePoint;
    /// <summary> 连接可视化内容生成模块 </summary>
    public ModuleVisual<DataConnector> VisualConnector => ModuleCore.VisualConnector;

    private DataSide aSide;
    private DataSide bSide;
    private DataConnector connector;

    public override void MouseDown(DataMouseInput data) {
        if (!FindSide.Find(data.WorldPosition, out aSide)) { return; }
        if (!Intersect(aSide, data.WorldPosition, out Vector3 intersectPoint)) { return; }
        connector = new DataConnector();
        connector.aPoint = intersectPoint;
        connector.bPoint = intersectPoint;
    }
    public override void MouseDrag(DataMouseInput data) {
        if (aSide == null) { return; }
        connector.bPoint = GetPosition(data);
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

    private Vector3 GetPosition(DataMouseInput data) {
        if (!FindSide.Find(data.WorldPosition, out bSide)) { return data.WorldPosition; }
        if (aSide == bSide) { return data.WorldPosition; }
        if (Intersect(bSide, data.WorldPosition, out Vector3 intersectPoint)) { return intersectPoint; }
        else { return data.WorldPosition; }
    }
    private bool Intersect(DataSide side, Vector3 position, out Vector3 intersectPoint) {
        DataIntersect intersect = new DataIntersect(side, position);
        AlgorithmSidePoint.Compute(intersect);
        intersectPoint = intersect.intersectPoint;
        return intersect.isIntersect;
    }
}
