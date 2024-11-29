using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 多边形耳切法
/// </summary>
public class UnitAlgorithmEarCutting : UnitAlgorithm<DataPlateDesign> {
    /// <summary> 多边形耳切法 </summary>
    public UnitAlgorithmEarCutting() { }

    public class Auriculare {
        public int index;
        public Vector3 aPoint;//+0
        public Vector3 bPoint;//-1
        public Vector3 cPoint;//+1
    }

    public void Compute(DataPlateDesign plateDesign) {
        List<Vector3> points = new List<Vector3>(plateDesign.points);
        //判断散列点排序方向
        Vector3[] allPoints = plateDesign.points;
        bool isClockWise = IsClockWise(allPoints);
        //耳切法生成三角形
        List<DataTriangle> triangles = new List<DataTriangle>();
        ComputeAuriculare(triangles, points, allPoints, isClockWise);
        plateDesign.triangles = triangles;
    }

    #region 函数
    /// <summary> 循环计算有效的耳点 </summary>
    public static void ComputeAuriculare(List<DataTriangle> triangles, List<Vector3> edgePoints, Vector3[] allPoints, bool isClockWise) {
        List<DataTriangle> temp = ComputeAuriculare(edgePoints, allPoints, isClockWise);
        if (temp.Count == 0) { return; }
        triangles.AddRange(temp);
        ComputeAuriculare(triangles, edgePoints, allPoints, isClockWise);
    }
    /// <summary> 计算一个有效的耳点 </summary>
    public static List<DataTriangle> ComputeAuriculare(List<Vector3> edgePoints, Vector3[] allPoints, bool isClockWise) {
        Vector3[] array = edgePoints.ToArray();
        List<DataTriangle> polygons = new List<DataTriangle>();
        for (int i = 0; i < array.Length; i++) {
            Auriculare auriculare = CreateAuriculare(i, array);
            // 等于180，大于180，不可能为耳点
            if (!GetAngleType(auriculare, isClockWise)) { continue; }
            // 包含其他点，不可能为耳点
            if (IsInsideTriangle(auriculare, allPoints)) { continue; }
            // 包含其他耳点，不可能成为耳点
            if (!IsInsideAuriculare(auriculare, edgePoints)) { continue; }
            edgePoints.Remove(auriculare.aPoint);
            polygons.Add(CreateAuriculareToTriangle(auriculare));
        }
        return polygons;
    }
    /// <summary> 创建耳点 </summary>
    public static Auriculare CreateAuriculare(int index, Vector3[] array) {
        Auriculare auriculare = new Auriculare();
        auriculare.index = index;
        auriculare.bPoint = array.LoopIndex(index - 1);
        auriculare.aPoint = array.LoopIndex(index);
        auriculare.cPoint = array.LoopIndex(index + 1);
        return auriculare;
    }
    /// <summary> 计算三角形内是否包含其他点 </summary>
    public static bool IsInsideTriangle(Auriculare auriculare, Vector3[] array) {
        for (int i = 0; i < array.Length; i++) {
            if (array[i] == auriculare.aPoint) { continue; }
            if (array[i] == auriculare.bPoint) { continue; }
            if (array[i] == auriculare.cPoint) { continue; }
            if (IsInsideTriangle(auriculare, array[i])) { return true; }
        }
        return false;
    }
    /// <summary> 计算三角形内是否包含其他点 </summary>
    public static bool IsInsideAuriculare(Auriculare auriculare, List<Vector3> edgePoints) {
        if (!edgePoints.Contains(auriculare.aPoint)) { return false; }
        if (!edgePoints.Contains(auriculare.bPoint)) { return false; }
        if (!edgePoints.Contains(auriculare.cPoint)) { return false; }
        return true;
    }
    /// <summary> 从节点创建三角形 </summary>
    public static DataTriangle CreateAuriculareToTriangle(Auriculare auriculare) {
        DataTriangle triangle = new DataTriangle();
        triangle.a = auriculare.aPoint;
        triangle.b = auriculare.bPoint;
        triangle.c = auriculare.cPoint;
        return triangle;
    }
    #endregion

    #region 算法
    /// <summary> 当前的点方向是否为顺时针 </summary>
    public static bool IsClockWise(Vector3[] array) {
        // 通过计算叉乘来确定方向
        float sum = 0f;
        double count = array.Length;
        Vector3 va, vb;
        for (int i = 0; i < array.Length; i++) {
            va = array[i];
            vb = (i == count - 1) ? array[0] : array[i + 1];
            sum += va.x * vb.y - va.y * vb.x;
        }
        return sum < 0;
    }
    /// <summary> 判断角的类型 </summary>
    public static bool GetAngleType(Auriculare auriculare, bool isClockWise) {
        // 角度是否小于180
        // oa & ob 之间的夹角，（右手法则）
        // 逆时针顺序是相反的
        Vector2 a = auriculare.aPoint;
        Vector2 b = auriculare.bPoint;
        Vector2 c = auriculare.cPoint;
        float f = (b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x);
        bool flag = isClockWise ? f > 0 : f < 0;
        if (f == 0) { return false;/*平角*/ }
        else if (flag) { return true;/*劣角*/ }
        else { return false;/*优角*/ }
    }
    /// <summary> p点是否在点a,b,c组成的三角形内,或边上 </summary>
    public static bool IsInsideTriangle(Auriculare auriculare, Vector2 p) {
        // p点是否在abc三角形内
        Vector2 a = auriculare.aPoint;
        Vector2 b = auriculare.bPoint;
        Vector2 c = auriculare.cPoint;
        float c1 = (b.x - a.x) * (p.y - b.y) - (b.y - a.y) * (p.x - b.x);
        float c2 = (c.x - b.x) * (p.y - c.y) - (c.y - b.y) * (p.x - c.x);
        float c3 = (a.x - c.x) * (p.y - a.y) - (a.y - c.y) * (p.x - a.x);
        return (c1 > 0f && c2 >= 0f && c3 >= 0f) || (c1 < 0f && c2 <= 0f && c3 <= 0f);
    }
    #endregion

}
