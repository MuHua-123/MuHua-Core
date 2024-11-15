using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDesignSelect : UIInputDesignUnit {
    /// <summary> 板片资产 </summary>
    public ModuleAssets<DataPlate> AssetsPlate => ModuleCore.AssetsPlate;
    /// <summary> 查询点算法模块 </summary>
    public ModuleAlgorithm<DataFindPoint> AlgorithmFindPoint => ModuleCore.AlgorithmFindPoint;
    /// <summary> 广播查询数据模块 </summary>
    public ModuleSending<DataFindPoint> SendingFindPoint => ModuleCore.SendingFindPoint;

    private DataFindPoint findPoint;
    private void FindPoint(Vector3 localPosition) {
        findPoint = new DataFindPoint();
        findPoint.position = localPosition;
        findPoint.datas = AssetsPlate.Datas;
        AlgorithmFindPoint.Compute(findPoint);
    }

    public override void MouseDown(DataUIMouseInput data) {
        FindPoint(data.WorldPosition);
        SendingFindPoint.Change(findPoint);
    }
}
