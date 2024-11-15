using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 插入点算法
/// </summary>
public class AlgorithmInsertPoint : ModuleAlgorithm<DataInsertPoint> {
    public class Segment {
        public DataPlate plate;
        public DataPoint aPoint;
        public DataPoint bPoint;
        public float distance = float.MaxValue;
    }

    protected override void Awake() => ModuleCore.AlgorithmInsertPoint = this;

    public override void Compute(DataInsertPoint insertPoint) {
        List<DataPlate> datas = insertPoint.datas;
        List<Segment> segments = new List<Segment>();
        Vector3 position = insertPoint.position;

        for (int i = 0; i < datas.Count; i++) {
            if (!FindSegment(datas[i], position, out Segment temp)) { continue; }
            segments.Add(temp);
        }

        if (segments.Count <= 0) { return; }
        Segment segment = segments[0];
        for (int i = 0; i < segments.Count; i++) {
            if (segment.distance < segments[i].distance) { continue; }
            segment = segments[i];
        }
        insertPoint.plate = segment.plate;
        insertPoint.aPoint = segment.aPoint;
        insertPoint.bPoint = segment.bPoint;
    }
    /// <summary> 查询匹配的线 </summary>
    private bool FindSegment(DataPlate plate, Vector3 position, out Segment segment) {
        List<DataPoint> points = plate.points;
        Vector3 c = position - plate.position;
        segment = new Segment();
        segment.plate = plate;
        for (int i = 0; i < points.Count; i++) {
            Vector3 a = points.LoopIndex(i + 0).position;
            Vector3 b = points.LoopIndex(i + 1).position;
            float distance = ProjectDistance(a, b, c);
            if (segment.distance < distance) { continue; }
            segment.aPoint = points.LoopIndex(i + 0);
            segment.bPoint = points.LoopIndex(i + 1);
            segment.distance = distance;
        }
        return segment.distance != float.MaxValue;
    }

    /// <summary>
    /// 向量投影法
    /// 计算点c到线段ab最近的点
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns>如果不在线段上返回 float.MaxValue</returns>
    private float ProjectDistance(Vector3 a, Vector3 b, Vector3 c) {
        Vector3 ab = b - a;
        Vector3 ac = c - a;
        Vector3 p = Vector3.Project(ac, ab);
        //Debug.Log($"{a} , {b} , {c} , {p} , {ab.normalized} , {p.normalized} , {ab.normalized != p.normalized} , {ab.magnitude < p.magnitude}");
        if (ab.normalized != p.normalized) { return float.MaxValue; }
        if (ab.magnitude < p.magnitude) { return float.MaxValue; }
        //Debug.Log($"{a} , {b} , {c} , {Vector3.Distance(c, p)}");
        return Vector3.Distance(c, p + a);
    }
}
