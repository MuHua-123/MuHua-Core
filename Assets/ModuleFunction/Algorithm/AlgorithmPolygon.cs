using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// 算法：耳切法
/// 依据：简单多边形的双耳定理
/// </summary>
public class AlgorithmPolygon : ModuleAlgorithm<DataPlate> {
    /// <summary> 算法：耳切法 </summary>
    public AlgorithmPolygon() { }

    public enum AngleType {
        /// <summary> 平角 = 180 </summary>
        StraightAngle = 0,
        /// <summary> 优角 >180 </summary>
        ReflexAngle = 1,
        /// <summary> 劣角 <180 </summary>
        InferiorAngle = 2
    }

    public class PointNode {
        public int index;
        public Vector2 Position;
        public Vector2 PreviousPosition;
        public Vector2 NextPosition;
    }

    public class Triangle {
        public Vector2 a;
        public Vector2 b;
        public Vector2 c;
    }

    public override void Compute(DataPlate data) {
        List<Vector2> edgePoints = new List<Vector2>(data.edgePoints);
        List<Triangle> polygons = new List<Triangle>();
        Vector2[] allArray = edgePoints.ToArray();
        bool isClockWise = IsClockWise(allArray);
        //耳切法生成三角形
        ComputeEarTriangle(polygons, edgePoints, allArray, isClockWise);
        MergeTriangles(data, polygons);
    }
    /// <summary> 循环计算耳点 </summary>
    public void ComputeEarTriangle(List<Triangle> polygons, List<Vector2> edgePoints, Vector2[] allArray, bool isClockWise) {
        List<Triangle> temp = ComputeEarTriangle(edgePoints, allArray, isClockWise);
        if (temp.Count == 0) { return; }
        polygons.AddRange(temp);
        ComputeEarTriangle(polygons, edgePoints, allArray, isClockWise);
    }
    /// <summary> 计算一个耳点 </summary>
    public List<Triangle> ComputeEarTriangle(List<Vector2> edgePoints, Vector2[] allArray, bool isClockWise) {
        Vector2[] array = edgePoints.ToArray();
        List<Triangle> polygons = new List<Triangle>();
        for (int i = 0; i < array.Length; i++) {
            PointNode pointNode = CreatePointNode(i, array);
            AngleType angleType = GetAngleType(pointNode, isClockWise);
            // 等于180，不可能为耳点
            if (angleType == AngleType.StraightAngle) { continue; }
            // 大于180，不可能为耳点
            if (angleType == AngleType.ReflexAngle) { continue; }
            // 包含其他点，不可能为耳点
            if (IsInsideTriangle(pointNode, allArray)) { continue; }
            // 包含其他耳点，不可能成为耳点
            if (!IsInsideEarTriangle(pointNode, edgePoints)) { continue; }
            edgePoints.Remove(pointNode.Position);
            polygons.Add(CreateTriangle(pointNode));
        }
        return polygons;
    }
    /// <summary> 创建节点 </summary>
    public PointNode CreatePointNode(int index, Vector2[] array) {
        int maxIndex = array.Length;
        PointNode pointNode = new PointNode();
        pointNode.index = index;
        pointNode.PreviousPosition = array[NormalIndex(index - 1, maxIndex)];
        pointNode.Position = array[NormalIndex(index + 0, maxIndex)];
        pointNode.NextPosition = array[NormalIndex(index + 1, maxIndex)];
        return pointNode;
    }
    /// <summary> 计算三角形内是否包含其他点 </summary>
    public bool IsInsideTriangle(PointNode node, Vector2[] array) {
        for (int i = 0; i < array.Length; i++) {
            if (array[i] == node.Position) { continue; }
            if (array[i] == node.PreviousPosition) { continue; }
            if (array[i] == node.NextPosition) { continue; }
            if (IsInsideTriangle(node, array[i])) { return true; }
        }
        return false;
    }
    /// <summary> 计算三角形内是否包含其他点 </summary>
    public bool IsInsideEarTriangle(PointNode node, List<Vector2> edgePoints) {
        if (!edgePoints.Contains(node.Position)) { return false; }
        if (!edgePoints.Contains(node.PreviousPosition)) { return false; }
        if (!edgePoints.Contains(node.NextPosition)) { return false; }
        return true;
    }
    /// <summary> 从节点创建三角形 </summary>
    public Triangle CreateTriangle(PointNode node) {
        Triangle triangle = new Triangle();
        triangle.a = node.Position;
        triangle.b = node.PreviousPosition;
        triangle.c = node.NextPosition;
        return triangle;
    }
    /// <summary> 合并三角形 </summary>
    public void MergeTriangles(DataPlate data, List<Triangle> polygons) {
        //创建数据容器
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();
        List<int> triangles = new List<int>();
        //三角形合并
        for (int i = 0; i < polygons.Count; i++) {
            Vector3 a = polygons[i].a;
            int aIndex = vertices.Count - 1;
            if (!vertices.Contains(a)) { vertices.Add(a); aIndex = vertices.Count - 1; }
            else { aIndex = vertices.IndexOf(a); }

            Vector3 b = polygons[i].b;
            int bIndex = vertices.Count - 1;
            if (!vertices.Contains(b)) { vertices.Add(b); bIndex = vertices.Count - 1; }
            else { bIndex = vertices.IndexOf(b); }

            Vector3 c = polygons[i].c;
            int cIndex = vertices.Count - 1;
            if (!vertices.Contains(c)) { vertices.Add(c); cIndex = vertices.Count - 1; }
            else { cIndex = vertices.IndexOf(c); }

            triangles.Add(aIndex);
            triangles.Add(bIndex);
            triangles.Add(cIndex);
        }
        //展开uv (顶点去掉z坐标就是未缩放的平面UV)
        for (int i = 0; i < vertices.Count; i++) { uv.Add(vertices[i]); }
        //附加数据
        data.vertices = vertices;
        data.uv = uv;
        data.triangles = triangles;
    }

    /// <summary> 头尾循环标准化索引 </summary>
    public static int NormalIndex(int index, int maxIndex) {
        if (maxIndex == 0) { Debug.LogError("错误索引：maxIndex = 0"); return 0; }
        if (index < 0) { return NormalIndex(index + maxIndex, maxIndex); }
        if (index >= maxIndex) { return NormalIndex(index - maxIndex, maxIndex); }
        return index;
    }
    /// <summary> 当前的点方向是否为顺时针 </summary>
    public static bool IsClockWise(Vector2[] array) {
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
    public static AngleType GetAngleType(PointNode node, bool isClockWise) {
        // 角度是否小于180
        // oa & ob 之间的夹角，（右手法则）
        // 逆时针顺序是相反的
        Vector2 o = node.Position;
        Vector2 a = node.PreviousPosition;
        Vector2 b = node.NextPosition;
        float f = (a.x - o.x) * (b.y - o.y) - (a.y - o.y) * (b.x - o.x);
        bool flag = isClockWise ? f > 0 : f < 0;
        if (f == 0) { return AngleType.StraightAngle; }
        else if (flag) { return AngleType.InferiorAngle; }
        else { return AngleType.ReflexAngle; }
    }
    /// <summary> p点是否在点和其左右两个点组成的三角形内,或ca,cb边上 </summary>
    public static bool IsInsideTriangle(PointNode node, Vector2 p) {
        // p点是否在abc三角形内
        Vector2 a = node.PreviousPosition;
        Vector2 b = node.NextPosition;
        Vector2 c = node.Position;
        float c1 = (b.x - a.x) * (p.y - b.y) - (b.y - a.y) * (p.x - b.x);
        float c2 = (c.x - b.x) * (p.y - c.y) - (c.y - b.y) * (p.x - c.x);
        float c3 = (a.x - c.x) * (p.y - a.y) - (a.y - c.y) * (p.x - a.x);
        return (c1 > 0f && c2 >= 0f && c3 >= 0f) || (c1 < 0f && c2 <= 0f && c3 <= 0f);
    }
}