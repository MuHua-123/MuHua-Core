using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 执行模块
/// </summary>
public abstract class ModuleExecute<Data> {
    /// <summary> 执行 </summary>
    public abstract void Compute(Data data);
}
