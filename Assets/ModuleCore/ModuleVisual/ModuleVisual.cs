using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生成可视化内容模块
/// </summary>
public interface ModuleVisual<Data> {
    /// <summary> 更新可视化内容 </summary>
    public void UpdateVisual(Data data);
    /// <summary> 释放可视化内容 </summary>
    public void ReleaseVisual(Data data);
}
/// <summary>
/// 生成可视化内容模块工具
/// </summary>
public static class VisualTool {
    /// <summary> 创建可视化内容 </summary>
    public static void Create<T>(ref T value, Transform original, Transform parent) {
        if (value != null) { return; }
        Transform temp = CreateTransform(original, parent);
        value = temp.GetComponent<T>();
    }
    /// <summary> 创建Transform </summary>
    public static Transform CreateTransform(Transform original, Transform parent) {
        Transform temp = Transform.Instantiate(original, parent);
        temp.gameObject.SetActive(true);
        return temp;
    }
}