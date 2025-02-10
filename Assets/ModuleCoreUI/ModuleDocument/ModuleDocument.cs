using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 文档模块
/// </summary>
public abstract class ModuleDocument : MonoBehaviour {
    /// <summary> 绑定文档 </summary>
    public UIDocument document;
    /// <summary> 根目录文档 </summary>
    public VisualElement root => document.rootVisualElement;
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;
    /// <summary> 添加UI元素 </summary>
    public void Add(VisualElement child) => root.Add(child);
    /// <summary> 查询UI元素 </summary>
    public T Q<T>(string name = null, string className = null) where T : VisualElement => root.Q<T>(name, className);
}
