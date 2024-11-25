using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIPageGarmentSewing : ModuleUIPage {
    private TopMenu topMenu;
    private VisualElement TopMenuElement => Q<VisualElement>("TopMenu");
    protected override void Awake() => ModuleCore.CurrentPage = this;

    private void Start() {
        topMenu = new TopMenu(TopMenuElement);
        topMenu.ClickTopMenu1 = () => { };
        topMenu.ClickTopMenu2 = () => { ModuleCore.PresetsPlateWindow.Open(null); };
    }
}
