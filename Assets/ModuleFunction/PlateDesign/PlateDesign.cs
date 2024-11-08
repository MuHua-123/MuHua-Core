using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

public class PlateDesign : ModulePlateDesign {
    public Transform PlateParent;
    public Transform PlateTemplate;

    private DataPlate dataPlate;

    public override void AddData(DataPlate data) {
        PlateParent.Instantiate(PlateTemplate, data, true);
    }

    #region 边缘点操作
    private PrefabPlateEdge edgePoint;
    public override bool IsValidEdgePoint => edgePoint != null;
    public override Vector3 EdgePointPosition => edgePoint.CurrentPosition;
    public override void SelectEdgePoint(Vector3 screenPosition) {
        edgePoint = RayFind<PrefabPlateEdge>(screenPosition, DefaultLayerMask);
        if (!IsValidEdgePoint) { return; }
        dataPlate = edgePoint.value;
    }
    public override void ChangeEdgePoint(Vector3 localPosition) {
        if (!IsValidEdgePoint) { return; }
        dataPlate.ChangeEdgePoint(edgePoint.index, localPosition);
    }
    public override void InsertEdgePoint(Vector3 screenPosition) {
        Vector3 position = ViewCamera.ScreenToWorldPosition(screenPosition);
        Vector3 worldPosition = position + ViewCamera.CameraWorldPosition;
        worldPosition.z = 0;
        edgePoint = Physics2DOverlapCircleAll<PrefabPlateEdge>(worldPosition, DefaultLayerMask);
        if (!IsValidEdgePoint) { return; }
        dataPlate = edgePoint.value;
        dataPlate?.InsertEdgePoint(edgePoint.index, position);
    }
    public override void ReleaseEdgePoint() {
        if (!IsValidEdgePoint) { return; }
        dataPlate.Compute();
        edgePoint = null;
    }
    #endregion

    #region 设计点操作
    private PrefabDesignPoint designPoint;
    public override bool IsValidDesignPoint => designPoint != null;
    public override Vector3 DesignPointPosition => designPoint.Position;
    public override void SelectDesignPoint(Vector3 screenPosition) {
        designPoint = RayFind<PrefabDesignPoint>(screenPosition, DefaultLayerMask);
        if (!IsValidDesignPoint) { return; }
        dataPlate = designPoint.DataPlate;
    }
    public override void ChangeDesignPoint(Vector3 localPosition) {
        if (!IsValidDesignPoint) { return; }
        dataPlate.ChangeDesignPoint(designPoint.Index, localPosition);
    }
    public override void InsertDesignPoint(Vector3 screenPosition) {
        Vector3 position = ViewCamera.ScreenToWorldPosition(screenPosition);
        Vector3 worldPosition = position + ViewCamera.CameraWorldPosition;
        worldPosition.z = 0;
        designPoint = Physics2DOverlapCircleAll<PrefabDesignPoint>(worldPosition, DefaultLayerMask);
        if (!IsValidDesignPoint) { return; }
        dataPlate = designPoint.DataPlate;
        dataPlate?.InsertDesignPoint(designPoint.Index, position);
    }
    public override void ReleaseDesignPoint() {
        if (!IsValidDesignPoint) { return; }
        dataPlate.Compute();
        designPoint = null;
    }
    #endregion

    #region 贝塞尔曲线操作
    private PrefabBezierPoint bezierPoint;
    public override bool IsValidBezierPoint => bezierPoint != null;
    public override Vector3 BezierPointPosition => bezierPoint.Position;
    public override void SelectBezierPoint(Vector3 screenPosition) {
        bezierPoint = RayFind<PrefabBezierPoint>(screenPosition, DefaultLayerMask);
        if (!IsValidBezierPoint) { return; }
        dataPlate = bezierPoint.DataPlate;
    }
    public override void ChangeBezierPoint(Vector3 localPosition) {
        if (!IsValidBezierPoint) { return; }
        bezierPoint.Change(localPosition);
    }
    public override void ReleaseBezierPoint() {
        if (!IsValidBezierPoint) { return; }
        dataPlate.Compute();
        bezierPoint = null;
    }
    #endregion

    #region 检测方法
    private RaycastHit hitInfo;
    private readonly float CheckRange = 0.02f;
    private readonly LayerMask DefaultLayerMask = ~(1 << 0) | 1 << 0;
    /// <summary> 射线检测 </summary>
    private T RayFind<T>(Vector3 screenPosition, LayerMask layerMask) where T : Object {
        Ray ray = ViewCamera.ScreenPointToRay(screenPosition);
        Physics.Raycast(ray, out hitInfo, 200, layerMask);
        return hitInfo.transform?.GetComponent<T>();
    }
    /// <summary> 物理2D圆形检测 </summary>
    private T Physics2DOverlapCircleAll<T>(Vector3 worldPosition, LayerMask layerMask) where T : Object {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPosition, CheckRange, layerMask);
        if (colliders.Length == 0) { return null; }
        return colliders[0].GetComponentInParent<T>();
    }
    #endregion
}
