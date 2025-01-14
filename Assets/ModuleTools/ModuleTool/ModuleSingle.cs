using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例模块
/// </summary>
public abstract class ModuleSingle<Data> : MonoBehaviour {
    /// <summary> 模块单例 </summary>
    public static ModuleSingle<Data> I => instance;
    /// <summary> 模块单例 </summary>
    protected static ModuleSingle<Data> instance;
    /// <summary> 初始化 </summary>
    protected virtual void Awake() {
        if (instance != null) { Destroy(instance.gameObject); }
        instance = this;
    }
}
