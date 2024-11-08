using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModuleAlgorithm<Data> {
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;
    /// <summary> 执行算法 </summary>
    public abstract void Compute(Data data);
}
