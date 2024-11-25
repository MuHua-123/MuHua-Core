using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignInsert : UnitMouseInput {
    /// <summary> 板片资产 </summary>
    public ModuleAssets<DataPlate> AssetsPlate => ModuleCore.AssetsPlate;
    /// <summary> 查询边算法模块 </summary>
    public ModuleFind<DataSide> FindSide => ModuleCore.FindSide;
    /// <summary> 计算位置到边上最近的点 </summary>
    public ModuleAlgorithm<DataIntersect> AlgorithmSidePoint => ModuleCore.AlgorithmSidePoint;
    /// <summary> 插入点数据转换板片上的点 </summary>
    public ModuleBuilder<DataInsertPoint, DataPoint> InsertPointToPoint => ModuleCore.InsertPointToPoint;

    public override void MouseDown(DataMouseInput data) {
        if (!FindSide.Find(data.WorldPosition, out DataSide side)) { return; }

        DataIntersect intersect = new DataIntersect(side, data.WorldPosition);
        AlgorithmSidePoint.Compute(intersect);
        if (!intersect.isIntersect) { return; }

        DataInsertPoint insertPoint = new DataInsertPoint();
        insertPoint.position = intersect.intersectPoint - side.plate.designPosition;
        insertPoint.plate = side.plate;
        insertPoint.aPoint = side.aPoint;
        insertPoint.bPoint = side.bPoint;
        insertPoint.side = side;
        InsertPointToPoint.To(insertPoint);
    }
}
