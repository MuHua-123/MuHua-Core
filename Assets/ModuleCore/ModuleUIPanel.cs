using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class ModuleUIPanel<Data> : MonoBehaviour {
    public ModuleUIPage ModuleUIPage;
    public VisualTreeAsset ModuleUIPanelAsset;

    protected VisualElement element;
    protected readonly string defaultStyleClass = "module-ui-panel";

    protected virtual ModuleCore ModuleCore => ModuleCore.I;

    /// <summary> MonoBehaviour Awake </summary>
    public abstract void Awake();
    /// <summary> 打开模块，并且传进参数 </summary>
    public abstract void Open(Data data);
    /// <summary> 关闭模块 </summary>
    public abstract void Close();

    protected void InitElement(bool hide = true) {
        element = ModuleUIPanelAsset.Instantiate();
        ModuleUIPage.Add(element);
        element.AddToClassList(defaultStyleClass);
        element.style.display = hide ? DisplayStyle.None : DisplayStyle.Flex;
    }
}
