using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 查询点算法
/// 转角法判断点是否在多边形内
/// </summary>
public class AlgorithmFindPoint : ModuleAlgorithm<DataFindPoint> {
    public readonly float FindRange = 0.01f;

    protected override void Awake() => ModuleCore.AlgorithmFindPoint = this;

    public override void Compute(DataFindPoint findPoint) {
        List<DataPlate> datas = findPoint.datas;
        for (int i = 0; i < datas.Count; i++) {
            if (FindPlatePoint(datas[i], findPoint)) { return; }
            if (FindPlateInside(datas[i], findPoint)) { findPoint.plate = datas[i]; }
        }
    }

    /// <summary> 查询匹配的点 </summary>
    private bool FindPlatePoint(DataPlate plate, DataFindPoint findPoint) {
        List<DataPoint> points = plate.points;
        Vector3 position = findPoint.position - plate.position;
        for (int i = 0; i < points.Count; i++) {
            float distance = Vector3.Distance(points[i].position, position);
            if (distance > FindRange) { continue; }
            findPoint.plate = plate;
            findPoint.point = points[i];
            return true;
        }
        return false;
    }
    /// <summary> 转角法查询位置是否在板片内 </summary>
    private bool FindPlateInside(DataPlate plate, DataFindPoint findPoint) {
        DataPoint[] points = plate.points.ToArray();
        double angles = 0;
        Vector3 position = findPoint.position - plate.position;
        for (int i = 0; i < points.Length; i++) {
            Vector3 a = points.LoopIndex(i + 0).position - position;
            Vector3 b = points.LoopIndex(i + 1).position - position;
            float angle = Vector2.SignedAngle(a, b);
            angles += angle;
        }
        int normal = (int)(angles * 1000);
        return normal > 0;
    }
}
