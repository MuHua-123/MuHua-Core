using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 合并三角形
/// </summary>
public class UnitAlgorithmMergeTriangles : UnitAlgorithm<DataPolygon> {
    public void Compute(DataPolygon data) {
        //List<DataTriangle> triangles = new List<DataTriangle>(data.triangles);
        //index = 0;
        //Merge(triangles);
        //data.triangles = triangles;
        //ModuleCore.I.VisualPolygon.UpdateVisual(data);
    }
    private int index;
    private int maxIndex;
    /// <summary> 取一个三角形出来 匹配剩下的三角形 符合条件则合并 </summary>
    private void Merge(List<DataTriangle> triangles) {
        if (index > triangles.Count) { return; }

        DataTriangle aT = triangles[0];
        triangles.Remove(aT);

        maxIndex = triangles.Count;
        for (int i = 0; i < triangles.Count; i++) {
            DataTriangle bT = triangles[i];
            //ab同边
            if (MergeConditions(bT.a, bT.b, bT, ref aT)) { triangles.Remove(bT); continue; }
            //bc同边
            if (MergeConditions(bT.b, bT.c, bT, ref aT)) { triangles.Remove(bT); continue; }
            //ca同边
            if (MergeConditions(bT.c, bT.a, bT, ref aT)) { triangles.Remove(bT); continue; }
        }
        index = maxIndex == triangles.Count ? index + 1 : 0;
        Debug.Log($"{index} , {maxIndex} , {triangles.Count}");

        triangles.Add(aT);
        Merge(triangles);
    }
    /// <summary> 匹配三角形 符合条件则合并 无法合并则返回 true </summary>
    private bool Merge(List<DataTriangle> triangles, DataTriangle aT) {
        for (int i = 0; i < triangles.Count; i++) {
            DataTriangle bT = triangles[i];
            //ab同边
            if (MergeConditions(bT.a, bT.b, bT, ref aT)) { triangles.Remove(bT); }
            //bc同边
            if (MergeConditions(bT.b, bT.c, bT, ref aT)) { triangles.Remove(bT); }
            //ca同边
            if (MergeConditions(bT.c, bT.a, bT, ref aT)) { triangles.Remove(bT); }
        }
        return true;
    }
    /// <summary> 计算三角形内是否包含其他点 </summary>
    private bool IsInsideTriangle(DataTriangle triangle, Vector3 point) {
        if (triangle.a == point) { return true; }
        if (triangle.b == point) { return true; }
        if (triangle.c == point) { return true; }
        return false;
    }
    //检测合并条件是否满足
    private bool MergeConditions(Vector3 a, Vector3 b, DataTriangle bT, ref DataTriangle aT) {
        if (!IsInsideTriangle(aT, a, b, out Vector3 o)) { return false; }
        if (!IsInsideTriangle(bT, a, b, out Vector3 c)) { return false; }
        if (IsInsideTriangle(aT, c)) { return true; }
        Vector3 oa = (o - a).normalized;
        Vector3 ob = (o - b).normalized;
        Vector3 oc = (o - c).normalized;
        if (oc == oa) { aT.a = o; aT.b = b; aT.c = c; return true; }
        if (oc == ob) { aT.a = o; aT.b = a; aT.c = c; return true; }
        return false;
    }
    /// <summary> 计算三角形内是否包含其他点 </summary>
    private bool IsInsideTriangle(DataTriangle triangle, Vector3 a, Vector3 b, out Vector3 o) {
        if (triangle.a == a && triangle.b == b) { o = triangle.c; return true; }
        if (triangle.a == b && triangle.b == a) { o = triangle.c; return true; }
        if (triangle.a == a && triangle.c == b) { o = triangle.b; return true; }
        if (triangle.a == b && triangle.c == a) { o = triangle.b; return true; }
        if (triangle.b == a && triangle.c == b) { o = triangle.a; return true; }
        if (triangle.b == b && triangle.c == a) { o = triangle.a; return true; }
        o = a; return false;
    }

    /// <summary> p点是否在点a,b,c组成的三角形内,或边上 </summary>
    public static bool IsInsideTriangle(DataTriangle auriculare, Vector2 p) {
        // p点是否在abc三角形内
        Vector2 a = auriculare.a;
        Vector2 b = auriculare.b;
        Vector2 c = auriculare.c;
        float c1 = (b.x - a.x) * (p.y - b.y) - (b.y - a.y) * (p.x - b.x);
        float c2 = (c.x - b.x) * (p.y - c.y) - (c.y - b.y) * (p.x - c.x);
        float c3 = (a.x - c.x) * (p.y - a.y) - (a.y - c.y) * (p.x - a.x);
        return (c1 > 0f && c2 >= 0f && c3 >= 0f) || (c1 < 0f && c2 <= 0f && c3 <= 0f);
    }
}
