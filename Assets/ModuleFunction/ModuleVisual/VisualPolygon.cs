using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

public class VisualPolygon : ModuleVisual<DataPolygon> {
    public Transform parent;//预设
    public Transform trianglePrefab;//三角形预设
    public Transform pointPrefab;//贝塞尔点预设

    protected override void Awake() => ModuleCore.VisualPolygon = this;

    public override void UpdateVisual(DataPolygon data) {
        List<DataTriangle> triangles = new List<DataTriangle>(data.triangles);
        parent.DestroySon();
        for (int i = 0; i < triangles.Count; i++) {
            CreateTriangle(triangles[i], i);
        }
    }

    #region 创建
    private void CreateTriangle(DataTriangle data, int index) {
        Transform triangle = Instantiate(trianglePrefab, parent);
        triangle.gameObject.SetActive(true);
        triangle.gameObject.name = index.ToString();

        CreatePoint(data.a, triangle, "a");
        CreatePoint(data.b, triangle, "b");
        CreatePoint(data.c, triangle, "c");
    }
    private void CreatePoint(Vector3 position, Transform parent, string name) {
        Transform temp = Instantiate(pointPrefab, parent);
        temp.gameObject.SetActive(true);
        temp.localPosition = position;
        temp.gameObject.name = name;
    }
    #endregion
}
