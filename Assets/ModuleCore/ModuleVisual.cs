using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生成可视化内容模块
/// </summary>
public abstract class ModuleVisual<Data> : MonoBehaviour {
    /// <summary> 必须要初始化 </summary>
    protected abstract void Awake();
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;

    /// <summary> 更新可视化 </summary>
    public abstract void UpdateVisual(Data data);
}
