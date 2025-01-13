using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单个独立模块
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

    /// <summary> 打开 </summary>
    public virtual void Open(Data data) { throw new NotImplementedException(); }
    /// <summary> 完成 </summary>
    public virtual void Complete() { throw new NotImplementedException(); }
    /// <summary> 关闭 </summary>
    public virtual void Close() { throw new NotImplementedException(); }
}
