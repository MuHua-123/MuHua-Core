using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDesignInsert : UIInputDesignUnit {
    /// <summary> 板片资产 </summary>
    public ModuleAssets<DataPlate> AssetsPlate => ModuleCore.AssetsPlate;
    /// <summary> 插入点算法模块 </summary>
    public ModuleAlgorithm<DataInsertPoint> AlgorithmInsertPoint => ModuleCore.AlgorithmInsertPoint;
    /// <summary> 插入点数据转换板片上的点 </summary>
    public ModuleBuilder<DataInsertPoint, DataPoint> InsertPointToPoint => ModuleCore.InsertPointToPoint;

    private DataInsertPoint insertPoint;
    private void FindPoint(Vector3 localPosition) {
        insertPoint = new DataInsertPoint();
        insertPoint.position = localPosition;
        insertPoint.datas = AssetsPlate.Datas;
        AlgorithmInsertPoint.Compute(insertPoint);
    }

    public override void MouseDown(DataUIMouseInput data) {
        FindPoint(data.WorldPosition);
        if (!insertPoint.IsValid) { return; }
        InsertPointToPoint.To(insertPoint);
    }
}
