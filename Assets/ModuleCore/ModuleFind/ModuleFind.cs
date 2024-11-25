using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 查找模块
/// </summary>
public abstract class ModuleFind<Data> : MonoBehaviour {
    /// <summary> 必须要初始化 </summary>
    protected abstract void Awake();
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;

    /// <summary> 查询 </summary>
    public abstract bool Find(Vector3 position, out Data data);
}
