using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 三角形转换网格
/// </summary>
public class AFTriangleMesh : ModuleAlgorithmFunction<DataPolygon> {
    public override void Compute(DataPolygon data) {
        List<DataTriangle> polygons = new List<DataTriangle>(data.triangles);
        //创建数据容器
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();
        List<int> triangles = new List<int>();
        //三角形合并
        for (int i = 0; i < polygons.Count; i++) {
            triangles.Add(AddIndexOf(vertices, polygons[i].a));
            triangles.Add(AddIndexOf(vertices, polygons[i].b));
            triangles.Add(AddIndexOf(vertices, polygons[i].c));
        }
        Debug.Log(vertices.Count);
        //展开uv (顶点去掉z坐标就是未缩放的平面UV)
        for (int i = 0; i < vertices.Count; i++) { uv.Add(vertices[i]); }
        //附加数据
        data.polygon = new Mesh();
        data.polygon.vertices = vertices.ToArray();
        data.polygon.uv = uv.ToArray();
        data.polygon.triangles = triangles.ToArray();
        data.polygon.RecalculateBounds();
        data.polygon.RecalculateNormals();
    }
    //顶点列表不包含则添加点，获得索引
    private int AddIndexOf(List<Vector3> vertices, Vector3 a) {
        if (!vertices.Contains(a)) { vertices.Add(a); }
        return vertices.IndexOf(a);
    }
}
