using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIPresetsPlateUnit {
    public Action Click;
    public readonly VisualElement element;
    public readonly DataPresetsPlate data;
    public Button Button => element.Q<Button>("Button");
    public UIPresetsPlateUnit(VisualElement element, DataPresetsPlate data) {
        this.data = data;
        this.element = element;
        Button.text = data.name;
        Button.clicked += Button_Clicked;
    }
    private void Button_Clicked() {
        Click?.Invoke();
    }
}
