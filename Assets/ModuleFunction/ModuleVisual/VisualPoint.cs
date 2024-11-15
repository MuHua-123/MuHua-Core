using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualPoint : ModuleVisual<DataPoint> {
    public Transform pointPrefab;//预设
    public Transform bezierPrefab;//贝塞尔点预设
    public Transform linePrefab;//线渲染器预设

    protected override void Awake() => ModuleCore.VisualPoint = this;

    public override void UpdateVisual(DataPoint data) {
        if (data.transform == null) { CreateTransform(data); }
        data.transform.localPosition = data.position;
        //贝塞尔曲线可视内容
        if (data.isCurveFront) { FrontBezier(data); }
        else { ReleaseBezier(data.frontBezierTransform, data.frontBezierLineRenderer); }
        if (data.isCurveAfter) { AfterBezier(data); }
        else { ReleaseBezier(data.afterBezierTransform, data.afterBezierLineRenderer); }
    }

    /// <summary> 创建和改变 前贝塞尔点(-) 的可视内容 </summary>
    private void FrontBezier(DataPoint data) {
        if (data.frontBezierTransform == null) {
            data.frontBezierTransform = CreateBezierTransform(data);
        }
        if (data.frontBezierLineRenderer == null) {
            data.frontBezierLineRenderer = CreateBezierLineRenderer(data);
        }
        Vector3 position = (data.frontBezier - data.position) * 50;
        data.frontBezierTransform.localPosition = position;
        data.frontBezierLineRenderer.SetPosition(1, position);
    }
    /// <summary> 创建和改变 后贝塞尔点(+) 的可视内容 </summary>
    private void AfterBezier(DataPoint data) {
        if (data.afterBezierTransform == null) {
            data.afterBezierTransform = CreateBezierTransform(data);
        }
        if (data.afterBezierLineRenderer == null) {
            data.afterBezierLineRenderer = CreateBezierLineRenderer(data);
        }
        Vector3 position = (data.afterBezier - data.position) * 50;
        data.afterBezierTransform.localPosition = position;
        data.afterBezierLineRenderer.SetPosition(1, position);
    }
    /// <summary> 释放贝塞尔点可视内容 </summary>
    private void ReleaseBezier(Transform transform, LineRenderer lineRenderer) {
        if (transform != null) { Destroy(transform.gameObject); }
        if (lineRenderer != null) { Destroy(lineRenderer.gameObject); }
    }

    #region 创建
    private void CreateTransform(DataPoint data) {
        Transform parent = data.plate.transform;
        data.transform = Instantiate(pointPrefab, parent);
        data.transform.gameObject.SetActive(true);
    }
    private Transform CreateBezierTransform(DataPoint data) {
        Transform parent = data.transform;
        Transform temp = Instantiate(bezierPrefab, parent);
        temp.gameObject.SetActive(true);
        return temp;
    }
    private LineRenderer CreateBezierLineRenderer(DataPoint data) {
        Transform parent = data.transform;
        Transform temp = Instantiate(linePrefab, parent);
        temp.gameObject.SetActive(true);
        return temp.GetComponent<LineRenderer>();
    }
    #endregion

}
