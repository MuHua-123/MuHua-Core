using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualPlate : ModuleVisual<DataPlate> {
    public Transform viewSpace;
    public Transform platePrefab;
    public Transform edgeLinePrefab;

    protected override void Awake() => ModuleCore.VisualPlate = this;

    public override void UpdateVisual(DataPlate data) {
        if (data.transform == null) { CreateTransform(data); }
        data.transform.localPosition = data.position;
        data.polygonMeshFilter.mesh = data.polygon;
        if (data.edgeLineRenderer == null) { CreateEdgeLineRenderer(data); }
        data.edgeLineRenderer.positionCount = data.edgePoints.Count;
        data.edgeLineRenderer.SetPositions(data.edgePoints.ToArray());
        //更新全部数据点的可视化内容
        data.points.ForEach(ModuleCore.VisualPoint.UpdateVisual);
    }

    private void CreateTransform(DataPlate data) {
        Transform temp = Instantiate(platePrefab, viewSpace);
        temp.gameObject.SetActive(true);
        data.transform = temp;
        data.polygonMeshFilter = temp.GetComponent<MeshFilter>();
    }
    private void CreateEdgeLineRenderer(DataPlate data) {
        Transform temp = Instantiate(edgeLinePrefab, data.transform);
        temp.gameObject.SetActive(true);
        data.edgeLineRenderer = temp.GetComponent<LineRenderer>();
    }
}
