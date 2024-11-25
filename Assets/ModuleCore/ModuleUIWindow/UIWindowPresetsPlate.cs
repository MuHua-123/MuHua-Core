using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIWindowPresetsPlate : ModuleUIWindow<Action> {
    public VisualTreeAsset PresetsPlateUnitAsset;
    private UIPresetsPlate presetsPlate;
    private VisualElement element => ModuleUIPage.Q<VisualElement>("PresetsPlate");

    /// <summary> 资源模块 </summary>
    private ModuleAssets<DataPlate> AssetsPlate => ModuleCore.AssetsPlate;
    /// <summary> 预设资源模块 </summary>
    private ModuleAssets<DataPlatePresets> AssetsPlatePresets => ModuleCore.AssetsPlatePresets;
    /// <summary> 转换模块 </summary>
    private ModuleBuilder<DataPlatePresets, DataPlate> PlatePresetsToPlate => ModuleCore.PlatePresetsToPlate;

    public override void Awake() {
        ModuleCore.PresetsPlateWindow = this;
        presetsPlate = new UIPresetsPlate(element);

        presetsPlate.ClickClose = Close;
    }
    public override void Open(Action data) {
        element.style.display = DisplayStyle.Flex;
        presetsPlate.Clear();
        AssetsPlatePresets.ForEach(Create);
    }
    public override void Close() {
        presetsPlate.Clear();
        element.style.display = DisplayStyle.None;
    }
    private void Create(DataPlatePresets data) {
        VisualElement temp = PresetsPlateUnitAsset.Instantiate();
        UIPresetsPlateUnit unit = new UIPresetsPlateUnit(temp, data);
        unit.Click = () => { CreateTemplate(data); };
        presetsPlate.Add(unit);
    }
    private void CreateTemplate(DataPlatePresets data) {
        DataPlate dataPlate = PlatePresetsToPlate.To(data);
        AssetsPlate.Add(dataPlate);
        Close();
    }
}
