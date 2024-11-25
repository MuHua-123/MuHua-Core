using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSuture {
    /// <summary> 核心模块 </summary>
    private ModuleCore ModuleCore => ModuleCore.I;
    /// <summary> 简单多边形算法模块 </summary>
    private ModuleAlgorithm<DataSutureSide> AlgorithmSutureSide => ModuleCore.AlgorithmSutureSide;

    public readonly DataSutureSide a;
    public readonly DataSutureSide b;
    public DataSuture(DataSide aSide, DataSide bSide) {
        a = new DataSutureSide(aSide, this);
        b = new DataSutureSide(bSide, this);
        Update();
    }

    public void Update() {
        AlgorithmSutureSide.Compute(a);
        AlgorithmSutureSide.Compute(b);
    }

    /// <summary> 缝合长度 </summary>
    public float length => this.SutureLength();

    #region 可视化内容
    /// <summary> 可视化内容 </summary>
    public ModulePrefab<DataSuture> design;
    /// <summary> 可视化对象 </summary>
    public ModulePrefab<DataSuture> baking;
    #endregion

}
