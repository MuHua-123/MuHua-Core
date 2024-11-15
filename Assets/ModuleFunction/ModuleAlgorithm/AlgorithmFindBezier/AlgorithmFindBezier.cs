using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 查询贝塞尔点算法
/// </summary>
public class AlgorithmFindBezier : ModuleAlgorithm<DataFindBezier> {
    public readonly float FindRange = 0.01f;

    protected override void Awake() => ModuleCore.AlgorithmFindBezier = this;

    public override void Compute(DataFindBezier findBezier) {
        List<DataPlate> datas = findBezier.datas;
        for (int i = 0; i < datas.Count; i++) {
            if (FindPlatePoint(datas[i], findBezier)) { return; }
        }
    }
    /// <summary> 查询匹配的点 </summary>
    private bool FindPlatePoint(DataPlate plate, DataFindBezier findBezier) {
        List<DataPoint> points = plate.points;
        Vector3 position = findBezier.position - plate.position;
        for (int i = 0; i < points.Count; i++) {
            float f = Vector3.Distance(points[i].frontBezier, position);
            if (f <= FindRange && points[i].isCurveFront) {
                findBezier.isFront = true;
                findBezier.plate = plate;
                findBezier.point = points[i];
                return true;
            }
            float a = Vector3.Distance(points[i].afterBezier, position);
            if (a <= FindRange && points[i].isCurveAfter) {
                findBezier.plate = plate;
                findBezier.point = points[i];
                return true;
            }
        }
        return false;
    }
}
