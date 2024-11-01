using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class ModuleUIPage : MonoBehaviour {
    public UIDocument document;
    public VisualElement root => document.rootVisualElement;

    protected virtual ModuleCore ModuleCore => ModuleCore.I;
    protected virtual void Awake() => ModuleCore.FunctionRegister(this);

    public void Add(VisualElement child) {
        root.Add(child);
    }
    public T Q<T>(string name = null, string className = null) where T : VisualElement {
        return root.Q<T>(name, className);
    }
}
