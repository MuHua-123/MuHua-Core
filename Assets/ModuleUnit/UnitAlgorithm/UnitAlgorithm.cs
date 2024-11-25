using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单个算法函数
/// </summary>
/// <typeparam name="Data"></typeparam>
public interface UnitAlgorithm<Data> {
    /// <summary> 执行算法 </summary>
    public void Compute(Data data);
}