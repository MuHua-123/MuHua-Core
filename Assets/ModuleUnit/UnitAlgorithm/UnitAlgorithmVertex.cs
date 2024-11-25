using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 顶点算法
/// </summary>
public class UnitAlgorithmVertex : UnitAlgorithm<DataPlate> {

    public class SideVertex : IComparable<SideVertex> {
        public float distance;
        public DataVertex vertex;

        public int CompareTo(SideVertex other) {
            return other.distance >= distance ? 1 : -1;
        }
    }

    public void Compute(DataPlate data) {
        float smooth = data.smooth;
        DataBorder border = data.border;
        List<Vector3> edgePoints = new List<Vector3>(data.edgePoints);

        int wide = Mathf.FloorToInt(border.Wide / smooth) + 1;
        int high = Mathf.FloorToInt(border.High / smooth) + 1;
        //计算内部顶点
        data.vertexGrid = new GridTool<DataVertex>(wide, high, (x, y) => {
            Vector3 position = border.MinPoint + new Vector3(x * smooth, y * smooth);
            DataVertex vertex = new DataVertex();
            vertex.isValid = FindPlateInside(edgePoints, position);
            vertex.design = position;
            return vertex;
        });
        //边缘所有的线段
        for (int i = 0; i < data.sides.Count; i++) {
            Compute(data.sides[i], wide, high, border, smooth, data.vertexGrid);
        }
    }

    /// <summary> 向网格和边缘写入顶点数据 </summary>
    private void Compute(DataSide side, int wide, int high, DataBorder border, float smooth, GridTool<DataVertex> grid) {
        DataLine[] lines = side.lines;
        List<SideVertex> sideVertices = new List<SideVertex>();
        //计算水平线段顶点
        for (int x = 0; x < wide; x++) {
            Vector3 a = new Vector3(border.minX + x * smooth, border.minY - 1);
            Vector3 b = new Vector3(border.minX + x * smooth, border.maxY + 1);
            for (int i = 0; i < lines.Length; i++) {
                DataVertex vertex = Compute(a, b, lines[i], smooth, border, grid);
                if (vertex == null) { continue; }
                float distance = Vector3.Distance(lines[i].a, vertex.design) + lines[i].origin;
                SideVertex sideVertex = new SideVertex();
                sideVertex.distance = distance;
                sideVertex.vertex = vertex;
                sideVertices.Add(sideVertex);
            }
        }
        //计算垂直线段顶点
        for (int y = 0; y < high; y++) {
            Vector3 a = new Vector3(border.minX - 1, border.minY + y * smooth);
            Vector3 b = new Vector3(border.maxX + 1, border.minY + y * smooth);
            for (int i = 0; i < lines.Length; i++) {
                DataVertex vertex = Compute(a, b, lines[i], smooth, border, grid);
                if (vertex == null) { continue; }
                float distance = Vector3.Distance(lines[i].a, vertex.design) + lines[i].origin;
                SideVertex sideVertex = new SideVertex();
                sideVertex.distance = distance;
                sideVertex.vertex = vertex;
                sideVertices.Add(sideVertex);
            }
        }
        sideVertices.Sort();
        List<DataVertex> vertices = new List<DataVertex>();
        for (int i = 0; i < sideVertices.Count; i++) {
            vertices.Add(sideVertices[i].vertex);
        }
        side.vertices = vertices.ToArray();
    }
    /// <summary> 向网格写入边缘顶点数据 </summary>
    private DataVertex Compute(Vector3 a, Vector3 b, DataLine line, float smooth, DataBorder border, GridTool<DataVertex> grid) {
        //计算交点
        if (!TryGetIntersectPoint(a, b, line.a, line.b, out Vector3 IntersectPoint)) { return null; }
        //计算顶点xy
        Vector3 offset = new Vector3(smooth, smooth, 0) * 0.3f;
        Vector3 position = IntersectPoint - border.MinPoint + offset;
        int vertexX = Mathf.FloorToInt(position.x / smooth);
        int vertexY = Mathf.FloorToInt(position.y / smooth);
        //填充数据
        DataVertex vertex = grid.Get(vertexX, vertexY);
        vertex.isValid = true;
        vertex.design = IntersectPoint;
        return vertex;
    }

    /// <summary> 转角法查询位置是否在板片内 </summary>
    public static bool FindPlateInside(List<Vector3> points, Vector3 position) {
        double angles = 0;
        for (int i = 0; i < points.Count; i++) {
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
