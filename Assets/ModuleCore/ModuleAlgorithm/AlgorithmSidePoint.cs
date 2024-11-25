using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 计算位置到边上最近的点
/// </summary>
public class AlgorithmSidePoint : ModuleAlgorithm<DataIntersect> {

    protected override void Awake() => ModuleCore.AlgorithmSidePoint = this;

    public override void Compute(DataIntersect data) {
        Vector3 position = data.position - data.side.plate.designPosition;
        for (int i = 0; i < data.side.lines.Length; i++) {
            DataLine line = data.side.lines[i];
            if (!Compute(line, position, out Vector3 intersectPoint)) { continue; }
            data.isIntersect = true;
            data.intersectPoint = intersectPoint + data.side.plate.designPosition;
            return;
        }
    }

    /// <summary> 查询匹配的边 </summary>
    private bool Compute(DataLine line, Vector3 position, out Vector3 intersectPoint) {
        return ProjectDistance(line.a, line.b, position, out intersectPoint);
    }

    /// <summary>
    /// 向量投影法
    /// 计算点c到线段ab最近的点
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns>如果不在线段上返回 false</returns>
    public static bool ProjectDistance(Vector3 a, Vector3 b, Vector3 c, out Vector3 intersectPoint) {
        Vector3 ab = b - a;
        Vector3 ac = c - a;
        Vector3 p = Vector3.Project(ac, ab.normalized);
        intersectPoint = p + a;
        if (ab.normalized != p.normalized) { return false; }
        if (ab.magnitude < p.magnitude) { return false; }
        return true;
    }
}
