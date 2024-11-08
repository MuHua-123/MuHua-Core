using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIPresetsPlate {
    public Action ClickClose;
    public readonly VisualElement element;
    public Button Close => element.Q<Button>("Close");
    public VisualElement Container => element.Q<VisualElement>("Container");
    public UIPresetsPlate(VisualElement element) {
        this.element = element;
        Container.Clear();
        Close.clicked += Close_Clicked;
    }
    public void Add(UIPresetsPlateUnit unit) {
        Container.Add(unit.element);
        unit.element.AddToClassList("pp-unit");
    }
    public void Add(VisualElement visualElement) {
        Container.Add(visualElement);
    }
    public void Clear() {
        Container.Clear();
    }
    private void Close_Clicked() {
        ClickClose?.Invoke();
    }
}
