using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 散列点生成多边形算法
/// </summary>
public class AlgorithmPolygon : ModuleAlgorithm<DataPlate> {
    private ModuleAlgorithmFunction<DataPolygon> EdgePoint = new AFEdgePoint();
    private ModuleAlgorithmFunction<DataPolygon> Cutting = new AFAuriculareCutting();
    private ModuleAlgorithmFunction<DataPolygon> Subdivision = new AFSubdivision();
    private ModuleAlgorithmFunction<DataPolygon> TriangleMesh = new AFTriangleMesh();

    protected override void Awake() => ModuleCore.AlgorithmPolygon = this;

    public override void Compute(DataPlate data) {
        DataPolygon polygon = new DataPolygon(data);
        //计算边缘点
        EdgePoint.Compute(polygon);
        //切割三角形
        Cutting.Compute(polygon);
        //合并三角形
        Subdivision.Compute(polygon);
        Subdivision.Compute(polygon);
        Subdivision.Compute(polygon);
        Subdivision.Compute(polygon);
        Subdivision.Compute(polygon);
        Subdivision.Compute(polygon);
        Subdivision.Compute(polygon);
        Subdivision.Compute(polygon);
        Subdivision.Compute(polygon);
        //三角形转换网格
        TriangleMesh.Compute(polygon);
    }
}
