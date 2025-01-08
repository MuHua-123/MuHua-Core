using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单个独立模块
/// </summary>
public abstract class ModuleSingle<Data> : MonoBehaviour {
    /// <summary> 必须要初始化 </summary>
    protected virtual void Awake() { }
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;

    /// <summary> 打开 </summary>
    public virtual void Open(Data data) { }
    /// <summary> 完成 </summary>
    public virtual void Complete() { }
    /// <summary> 关闭 </summary>
    public virtual void Close() { }
}
