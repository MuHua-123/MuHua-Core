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
    protected abstract void Awake();

    /// <summary> 替换 </summary>
    protected virtual void Replace(bool isDontDestroy = true) {
        if (instance != null) { Destroy(instance.gameObject); }
        instance = (T)this;
        if (isDontDestroy) { DontDestroyOnLoad(gameObject); }
    }
    /// <summary> 不替换 </summary>
    protected virtual void NoReplace(bool isDontDestroy = true) {
        if (isDontDestroy) { DontDestroyOnLoad(gameObject); }
        if (instance == null) { instance = (T)this; }
        else { Destroy(gameObject); }
    }
}
