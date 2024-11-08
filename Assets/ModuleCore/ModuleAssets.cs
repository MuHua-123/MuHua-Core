using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModuleAssets<Data> : MonoBehaviour {
    [SerializeField] protected List<Data> assets;
    /// <summary> 资产列表 </summary>
    public virtual List<Data> Assets => assets;
    /// <summary> 必须要初始化 </summary>
    protected abstract void Awake();
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;
    /// <summary> 循环列表 </summary>
    public abstract void ForEach(Action<Data> action);
}
