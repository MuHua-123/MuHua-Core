using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIPageGarmentSewing : ModuleUIPage {
    private TopMenu topMenu;
    private UIPlateDesign plateDesign;
    private UIPlateBaking plateBaking;
    private VisualElement TopMenuElement => Q<VisualElement>("TopMenu");
    private VisualElement PlateDesignElement => Q<VisualElement>("PlateDesign");
    private VisualElement PlateBakingElement => Q<VisualElement>("PlateBaking");
    protected override void Awake() {
        ModuleCore.CurrentPage = this;
    }
    private void Start() {
        topMenu = new TopMenu(TopMenuElement);
        plateDesign = new UIPlateDesign(PlateDesignElement);
        plateBaking = new UIPlateBaking(PlateBakingElement);

        topMenu.ClickTopMenu1 = () => { };
        topMenu.ClickTopMenu2 = () => { ModuleCore.PresetsPlateWindow.Open(null); };
    }
}
