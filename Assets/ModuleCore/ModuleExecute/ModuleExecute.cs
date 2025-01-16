using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 执行模块
/// </summary>
public interface ModuleExecute<Data> {
    /// <summary> 执行 </summary>
    public void Execute(Data data);
}
