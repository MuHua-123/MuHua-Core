using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSuture {
    /// <summary> 核心模块 </summary>
    private ModuleCore ModuleCore => ModuleCore.I;
    /// <summary> 设计可视化模块 </summary>
    private ModuleVisual<DataSuture> VisualDesign => ModuleCore.VisualSutureDesign;
    /// <summary> 烘焙可视化模块 </summary>
    private ModuleVisual<DataSuture> VisualBaking => ModuleCore.VisualSutureBaking;
    /// <summary> 缝合线算法模块 </summary>
    private ModuleAlgorithm<DataSuture> AlgorithmSuture => ModuleCore.AlgorithmSuture;

    public readonly DataSutureSide a;
    public readonly DataSutureSide b;
    public DataSuture(DataPlateSide aSide, DataPlateSide bSide) {
        a = new DataSutureSide(aSide, this);
        b = new DataSutureSide(bSide, this);
        UpdateVisual();
    }

    public void UpdateVisual() {
        AlgorithmSuture.Compute(this);
        VisualDesign.UpdateVisual(this);
        VisualBaking.UpdateVisual(this);
    }

    #region 次要数据
    /// <summary> 缝合长度 </summary>
    public float length;
    /// <summary> 缝合点 </summary>
    public List<DataSuturePoint> points;
    #endregion

    #region 可视化内容
    /// <summary> 可视化内容 </summary>
    public ModulePrefab<DataSuture> design;
    /// <summary> 可视化对象 </summary>
    public ModulePrefab<DataSuture> baking;
    #endregion

}
