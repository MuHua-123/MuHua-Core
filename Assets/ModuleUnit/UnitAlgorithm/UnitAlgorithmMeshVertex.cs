using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAlgorithmMeshVertex : UnitAlgorithm<DataPolygon> {
    public class Line {
        public Vector3 aPoint;
        public Vector3 bPoint;
    }

    public class VertexUnit {
        public Vector3 position;
    }

    public VertexUnit[,] unitArray;

    public void Compute(DataPolygon data) {
        //List<Vector3> edgePoints = new List<Vector3>(data.edgePoints);
        ////计算边界
        //float minX = 0; float minY = 0;
        //float maxX = 0; float maxY = 0;
        //for (int i = 0; i < edgePoints.Count; i++) {
        //    if (edgePoints[i].x < minX) { minX = edgePoints[i].x; }
        //    if (edgePoints[i].x > maxX) { maxX = edgePoints[i].x; }
        //    if (edgePoints[i].y < minY) { minY = edgePoints[i].y; }
        //    if (edgePoints[i].y > maxY) { maxY = edgePoints[i].y; }
        //}
        //float wide = maxX - minX;
        //float high = maxY - minY;
        ////求余，得商数
        //int wideQuotient = Quotient(wide, 0.01f) + 1;
        //int highQuotient = Quotient(high, 0.01f) + 1;
        ////平均间隔
        //float wideAverage = wide / wideQuotient;
        //float highAverage = high / highQuotient;
        ////计算内部网格点
        //List<Vector3> vertices = new List<Vector3>();
        //Vector3 origin = new Vector3(minX, minY);
        //for (int i = 0; i < highQuotient; i++) {
        //    for (int j = 0; j < wideQuotient; j++) {
        //        Vector3 position = origin + new Vector3(j * wideAverage, i * highAverage);
        //        if (FindPlateInside(edgePoints, position)) { vertices.Add(position); }
        //    }
        //}
        ////创建边缘线段，区分方向
        //List<Line> horizontal = new List<Line>();
        //List<Line> vertical = new List<Line>();
        //for (int i = 0; i < edgePoints.Count; i++) {
        //    Line line = new Line();
        //    line.aPoint = edgePoints.LoopIndex(i + 0);
        //    line.bPoint = edgePoints.LoopIndex(i + 1);
        //    Vector3 direction = (line.bPoint - line.aPoint).normalized;
        //    if (Mathf.Abs(direction.x) >= 0.9) { horizontal.Add(line); }
        //    else { vertical.Add(line); }
        //}
        ////计算水平线段顶点
        //for (int i = 0; i < wideQuotient; i++) {
        //    Vector3 a = new Vector3(minX + i * wideAverage, minY - 1);
        //    Vector3 b = new Vector3(minX + i * wideAverage, maxY + 1);
        //    //Debug.Log($"线段：{a} , {b}");
        //    for (int j = 0; j < horizontal.Count; j++) {
        //        Line line = horizontal[j];
        //        //Debug.Log($"线段：{a} , {b} , {line.aPoint} , {line.bPoint}");
        //        if (!TryGetIntersectPoint(a, b, line.aPoint, line.bPoint, out Vector3 intersectPos)) { continue; }
        //        //Debug.Log($"交点：{intersectPos}");
        //        vertices.Add(intersectPos);
        //    }
        //}
        ////计算垂直线段顶点
        //for (int i = 0; i < highQuotient; i++) {
        //    Vector3 a = new Vector3(minX - 1, minY + i * highAverage);
        //    Vector3 b = new Vector3(maxX + 1, minY + i * highAverage);
        //    //Debug.Log($"线段：{a} , {b}");
        //    for (int j = 0; j < vertical.Count; j++) {
        //        Line line = vertical[j];
        //        //Debug.Log($"线段：{a} , {b} , {line.aPoint} , {line.bPoint}");
        //        if (!TryGetIntersectPoint(a, b, line.aPoint, line.bPoint, out Vector3 intersectPos)) { continue; }
        //        //Debug.Log($"交点：{intersectPos}");
        //        vertices.Add(intersectPos);
        //    }
        //}
        //Debug.Log(vertices.Count);
        //data.vertices = vertices;
    }


    /// <summary> 商数 </summary>
    public static int Quotient(float distance, float smooth) {
        int a = (int)(distance * 1000);
        int b = (int)(smooth * 1000);
        return Math.DivRem(a, b, out int remainder);
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
