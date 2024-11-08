using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIWindowPresetsPlate : ModuleUIWindow<Action> {
    public VisualTreeAsset PresetsPlateUnitAsset;
    private UIPresetsPlate presetsPlate;
    private VisualElement element => ModuleUIPage.Q<VisualElement>("PresetsPlate");
    private ModuleAssets<DataPresetsPlate> PresetsPlateAssets => ModuleCore.PresetsPlateAssets;
    public override void Awake() {
        ModuleCore.PresetsPlateWindow = this;
        presetsPlate = new UIPresetsPlate(element);

        presetsPlate.ClickClose = Close;
    }
    public override void Open(Action data) {
        element.style.display = DisplayStyle.Flex;
        presetsPlate.Clear();
        PresetsPlateAssets.ForEach(Create);
    }
    public override void Close() {
        presetsPlate.Clear();
        element.style.display = DisplayStyle.None;
    }
    private void Create(DataPresetsPlate data) {
        VisualElement temp = PresetsPlateUnitAsset.Instantiate();
        UIPresetsPlateUnit unit = new UIPresetsPlateUnit(temp, data);
        unit.Click = () => { CreateTemplate(data); };
        presetsPlate.Add(unit);
    }
    private void CreateTemplate(DataPresetsPlate data) {
        ModuleCore.PlateDesign.AddData(data.ToPlate());
        Close();
    }
}
