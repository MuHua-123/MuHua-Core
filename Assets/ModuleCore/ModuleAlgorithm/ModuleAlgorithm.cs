using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 算法模块
/// </summary>
/// <typeparam name="Data"></typeparam>
public abstract class ModuleAlgorithm<Data> : MonoBehaviour {
    /// <summary> 必须要初始化 </summary>
    protected abstract void Awake();
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;

    /// <summary> 执行算法 </summary>
    public abstract void Compute(Data data);
}