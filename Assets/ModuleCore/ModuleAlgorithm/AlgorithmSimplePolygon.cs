using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 散列点生成简单多边形算法
/// </summary>
public class AlgorithmSimplePolygon : ModuleAlgorithm<DataPlate> {
    private UnitAlgorithm<DataPlateDesign> AlgorithmSideSubdivision = new UnitAlgorithmJobsSideSubdivision();
    private UnitAlgorithm<DataPlateDesign> AlgorithmTriangle = new UnitAlgorithmEarCutting();
    private UnitAlgorithm<DataPlateDesign> AlgorithmMergeTriangle = new UnitAlgorithmMergeTriangle();

    protected override void Awake() => ModuleCore.AlgorithmSimplePolygon = this;

    public override void Compute(DataPlate plate) {
        //遍历计算边(DataSide)上的细分点(positions)和线(lines)
        AlgorithmSideSubdivision.Compute(plate.dataDesign);
        //计算三角面
        AlgorithmTriangle.Compute(plate.dataDesign);
        //三角面列表转换网格
        AlgorithmMergeTriangle.Compute(plate.dataDesign);
    }
}
