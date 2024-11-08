using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

public class PrefabPlateEdge : MonoBehaviour {
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;

    [HideInInspector] public int index;
    [HideInInspector] public DataPlate value;

    public int MaxIndex => value.edgePoints.Count;
    public int NextIndex => DataPlateTool.NormalIndex(index + 1, MaxIndex);
    public Vector3 CurrentPosition => value.FindEdgePoint(index);
    public Vector3 NextPosition => value.FindEdgePoint(NextIndex);
    public void SetValue(int index, DataPlate value) {
        this.index = index;
        this.value = value;
        value.OnChangeEdgePoint += UpdateLineRenderer;
        UpdateLineRenderer(index);
    }
    private void OnDestroy() {
        value.OnChangeEdgePoint -= UpdateLineRenderer;
    }
    public void UpdateLineRenderer(int index) {
        if (index != this.index && index != NextIndex) { return; }
        transform.localPosition = CurrentPosition;

        Vector3 direction = NextPosition - CurrentPosition;
        lineRenderer.SetPosition(1, direction);
        edgeCollider.points = new Vector2[] { Vector2.zero, direction };
    }
}
