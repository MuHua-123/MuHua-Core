using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 构造器
/// 根据原型构造数据
/// </summary>
/// <typeparam name="Origin"></typeparam>
/// <typeparam name="Data"></typeparam>
public abstract class ModuleBuilder<Origin, Data> : MonoBehaviour {
    /// <summary> 必须要初始化 </summary>
    protected abstract void Awake();
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;

    /// <summary> 根据原型构造数据 </summary>
    public abstract Data To(Origin origin);
}
