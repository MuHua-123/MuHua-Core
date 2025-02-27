using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景中的固定模块
/// </summary>
public abstract class ModuleFixed : MonoBehaviour {
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;
}
