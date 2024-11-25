using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 细分多边形算法
/// </summary>
public class AlgorithmSubdivisionPolygon : ModuleAlgorithm<DataPlate> {
    private UnitAlgorithm<DataPlate> AlgorithmSideSubdivision = new UnitAlgorithmBezier();
    private UnitAlgorithm<DataPlate> AlgorithmBorder = new UnitAlgorithmBorder();
    private UnitAlgorithm<DataPlate> AlgorithmVertex = new UnitAlgorithmVertex();
    private UnitAlgorithm<DataPlate> AlgorithmTriangle = new UnitAlgorithmRhombus();
    private UnitAlgorithm<DataPlate> AlgorithmMergeTriangle = new UnitAlgorithmMergeTriangle();

    protected override void Awake() => ModuleCore.AlgorithmSubdivisionPolygon = this;

    public override void Compute(DataPlate data) {
        //遍历计算边(DataSide)上的细分点(positions)和线(lines)
        Chronoscope("遍历计算边(DataSide)上的细分点(positions)和线(lines)消耗时间：", () => AlgorithmSideSubdivision.Compute(data));
        //计算多边形边界
        Chronoscope("计算多边形边界消耗时间：", () => AlgorithmBorder.Compute(data));
        //计算顶点
        Chronoscope("计算顶点消耗时间：", () => AlgorithmVertex.Compute(data));
        //计算三角面
        Chronoscope("计算三角面消耗时间：", () => AlgorithmTriangle.Compute(data));
        //三角面列表转换网格
        Chronoscope("三角面列表转换网格消耗时间：", () => AlgorithmMergeTriangle.Compute(data));
    }

    /// <summary> 是否启用计时器 </summary>
    private readonly bool isEnableTimer = true;
    /// <summary> 计时器 </summary>
    private void Chronoscope(string content, Action action) {
        if (!isEnableTimer) { action?.Invoke(); return; }
        float time = Time.realtimeSinceStartup;
        action?.Invoke();
        float consumed = Time.realtimeSinceStartup - time;
        Debug.Log($"{content}{consumed * 1000}");
    }
}
