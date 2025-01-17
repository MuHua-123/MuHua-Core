using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例模块
/// </summary>
public abstract class ModuleSingle<T> : MonoBehaviour where T : ModuleSingle<T> {
    /// <summary> 模块单例 </summary>
    public static T I => instance;
    /// <summary> 模块单例 </summary>
    protected static T instance;
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;
    /// <summary> 初始化 </summary>
    protected virtual void Awake() {
        if (instance != null) { Destroy(instance.gameObject); }
        instance = (T)this;
    }
}
