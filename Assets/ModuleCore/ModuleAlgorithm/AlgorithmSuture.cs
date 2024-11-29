using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 缝合边算法模块
/// </summary>
public class AlgorithmSuture : ModuleAlgorithm<DataSuture> {
    /// <summary> 设计的缝合线 </summary>
    public UnitAlgorithm<DataSutureSide> SutureDesign = new UnitAlgorithmSutureDesign();
    /// <summary> 烘焙的缝合线 </summary>
    public UnitAlgorithm<DataSutureSide> SutureBaking = new UnitAlgorithmSutureBaking();

    protected override void Awake() => ModuleCore.AlgorithmSuture = this;

    public override void Compute(DataSuture suture) {
        //缝合边长
        float aLength = suture.a.plateSide.dataDesign.length;
        float bLength = suture.b.plateSide.dataDesign.length;
        suture.length = aLength < bLength ? aLength : bLength;
        //设计缝合顶点
        SutureDesign.Compute(suture.a);
        SutureDesign.Compute(suture.b);
        //烘焙缝合顶点
        SutureBaking.Compute(suture.a);
        SutureBaking.Compute(suture.b);
        //缝合锚点
        suture.points = new List<DataSuturePoint>();
        suture.points.AddRange(Compute(suture.a, suture.b));
        suture.points.AddRange(Compute(suture.b, suture.a));
    }
    private List<DataSuturePoint> Compute(DataSutureSide a, DataSutureSide b) {
        DataSutureSideVertex[] vertexs = a.dataBaking.vertexs;
        DataSutureSideVertex[] allVertexs = b.dataBaking.allVertexs;
        List<DataSuturePoint> suturePoints = new List<DataSuturePoint>();
        for (int i = 0; i < vertexs.Length; i++) {
            DataSutureSideVertex vertex = vertexs[i];
            for (int j = 0; j < allVertexs.Length; j++) {
                DataSutureSideVertex anchor = allVertexs[j];
                if (anchor.MaxDistance < vertex.origin) { continue; }
                DataSuturePoint suturePoint = new DataSuturePoint();
                suturePoint.distance = vertex.origin - anchor.origin;
                suturePoint.vertex = vertex.a;
                suturePoint.aAnchor = anchor.a;
                suturePoint.bAnchor = anchor.b;
                suturePoints.Add(suturePoint);
            }
        }
        return suturePoints;
    }
}
