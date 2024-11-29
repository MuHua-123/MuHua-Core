using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 顶点算法
/// </summary>
public class UnitAlgorithmVertex : UnitAlgorithm<DataPlateBaking> {

    public class SideVertex : IComparable<SideVertex> {
        public float distance;
        public Vector3 position;

        public int CompareTo(SideVertex other) {
            if (other == null || this == null) { return 0; }
            if (other == this) { return 0; }
            if (other.distance > distance) { return -1; }
            if (other.distance < distance) { return 1; }
            return 0;
        }
    }

    public void Compute(DataPlateBaking plateBaking) {
        DataBorder border = plateBaking.border;
        Vector3[] points = border.points;

        //计算内部顶点
        //plateBaking.grid = new GridTool<DataPlateVertex>(border.GridWide, border.GridHigh, (x, y) => {
        //    Vector3 position = new Vector3(x, y) * border.smooth + border.MinPoint;
        //    DataPlateVertex vertex = new DataPlateVertex();
        //    vertex.isValid = FindPlateInside(points, position);
        //    vertex.position = position;
        //    return vertex;
        //});

        //边缘所有的线段
        //List<DataPlateSide> plateSides = plateBaking.plate.plateSides;
        //for (int i = 0; i < plateSides.Count; i++) {
        //    SubdivideSideVertex(plateBaking, plateSides[i]);
        //}
    }

    private void SubdivideSideVertex(DataPlateBaking plateBaking, DataPlateSide plateSides) {
        DataBorder border = plateBaking.border;
        DataPlateLine[] lines = plateSides.dataBaking.lines;
        //计算水平线段顶点
        List<SideVertex> sideVertexs = new List<SideVertex>();
        for (int x = 0; x < border.GridWide; x++) {
            Vector3 a = new Vector3(border.minX + x * border.smooth, border.minY - 1);
            Vector3 b = new Vector3(border.minX + x * border.smooth, border.maxY + 1);
            for (int i = 0; i < lines.Length; i++) {
                if (!TryGetIntersectPoint(a, b, lines[i].a, lines[i].b, out Vector3 IntersectPoint)) { continue; }
                float distance = Vector3.Distance(lines[i].a, IntersectPoint) + lines[i].origin;
                SideVertex sideVertex = new SideVertex();
                sideVertex.distance = distance;
                sideVertex.position = IntersectPoint;
                sideVertexs.Add(sideVertex);
            }
        }
        //计算垂直线段顶点
        for (int y = 0; y < border.GridHigh; y++) {
            Vector3 a = new Vector3(border.minX - 1, border.minY + y * border.smooth);
            Vector3 b = new Vector3(border.maxX + 1, border.minY + y * border.smooth);
            for (int i = 0; i < lines.Length; i++) {
                if (!TryGetIntersectPoint(a, b, lines[i].a, lines[i].b, out Vector3 IntersectPoint)) { continue; }
                float distance = Vector3.Distance(lines[i].a, IntersectPoint) + lines[i].origin;
                SideVertex sideVertex = new SideVertex();
                sideVertex.distance = distance;
                sideVertex.position = IntersectPoint;
                sideVertexs.Add(sideVertex);
            }
        }
        //排序
        sideVertexs.Sort();
        //写入网格，写入顶点
        Vector3 offset = border.MinPoint - new Vector3(border.smooth, border.smooth, 0) * 0.3f;
        List<DataPlateVertex> vertexs = new List<DataPlateVertex>();
        for (int i = 0; i < sideVertexs.Count; i++) {
            Vector3 position = sideVertexs[i].position;
            Vector3 gridPosition = position - offset;
            int vertexX = Mathf.FloorToInt(gridPosition.x / border.smooth);
            int vertexY = Mathf.FloorToInt(gridPosition.y / border.smooth);
            //填充数据
            //DataPlateVertex vertex = plateBaking.grid.Get(vertexX, vertexY);
            //vertex.isValid = true;
            //vertex.position = position;
            ////写入顶点
            //vertexs.Add(vertex);
        }
        plateSides.dataBaking.vertexs = vertexs.ToArray();
    }

    /// <summary> 转角法查询位置是否在板片内 </summary>
    public static bool FindPlateInside(Vector3[] points, Vector3 position) {
        double angles = 0;
        for (int i = 0; i < points.Length; i++) {
            Vector3 a = points.LoopIndex(i + 0) - position;
            Vector3 b = points.LoopIndex(i + 1) - position;
            float angle = Vector2.SignedAngle(a, b);
            angles += angle;
        }
        int normal = (int)(angles * 1000);
        return normal > 1000;
    }
    /// <summary>
    /// 计算AB与CD两条线段的交点.
    /// </summary>
    /// <param name="a">A点</param>
    /// <param name="b">B点</param>
    /// <param name="c">C点</param>
    /// <param name="d">D点</param>
    /// <param name="intersectPos">AB与CD的交点</param>
    /// <returns>是否相交 true:相交 false:未相交</returns>
    private bool TryGetIntersectPoint(Vector3 a, Vector3 b, Vector3 c, Vector3 d, out Vector3 intersectPos) {
        intersectPos = Vector3.zero;

        Vector3 ab = b - a;
        Vector3 ca = a - c;
        Vector3 cd = d - c;

        Vector3 v1 = Vector3.Cross(ca, cd);
        // 不共面
        if (Mathf.Abs(Vector3.Dot(v1, ab)) > 1e-6) { return false; }
        // 平行
        if (Vector3.Cross(ab, cd).sqrMagnitude <= 1e-6) { return false; }

        Vector3 ad = d - a;
        Vector3 cb = b - c;
        // 快速排斥
        if (Mathf.Min(a.x, b.x) > Mathf.Max(c.x, d.x) || Mathf.Max(a.x, b.x) < Mathf.Min(c.x, d.x)
           || Mathf.Min(a.y, b.y) > Mathf.Max(c.y, d.y) || Mathf.Max(a.y, b.y) < Mathf.Min(c.y, d.y)
           || Mathf.Min(a.z, b.z) > Mathf.Max(c.z, d.z) || Mathf.Max(a.z, b.z) < Mathf.Min(c.z, d.z)) {
            return false;
        }

        // 跨立试验
        if (Vector3.Dot(Vector3.Cross(-ca, ab), Vector3.Cross(ab, ad)) > 0
            && Vector3.Dot(Vector3.Cross(ca, cd), Vector3.Cross(cd, cb)) > 0) {
            Vector3 v2 = Vector3.Cross(cd, ab);
            float ratio = Vector3.Dot(v1, v2) / v2.sqrMagnitude;
            intersectPos = a + ab * ratio;
            return true;
        }

        return false;
    }
}
