using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

public class UIControlInspect : UnitUIControl {

    #region UI元素
    public override VisualElement Element => ModuleUIPage.Q<VisualElement>("Inspect");
    public VisualElement PlateSettings => Element.Q<VisualElement>("PlateSettings");
    public VisualElement PointSettings => Element.Q<VisualElement>("PointSettings");
    #endregion

    #region 引用模块
    /// <summary> 广播查询数据模块 </summary>
    //public ModuleSending<DataFind> SendingFind => ModuleCore.SendingFind;
    #endregion

    //private DataPlate Plate => SendingFind.Current.plate;
    //private DataPoint Point => SendingFind.Current.point;

    private UIPlateSettings uiPlateSettings;
    private UIPointSettings uiPointSettings;
    public override void Awake() {
        uiPlateSettings = new UIPlateSettings(PlateSettings);

        uiPointSettings = new UIPointSettings(PointSettings);
        //uiPointSettings.Toggle1.OnChange += (value) => { Point.isCurveFront = value; Plate.UpdateVisual(); };
        //uiPointSettings.Toggle2.OnChange += (value) => { Point.isCurveAfter = value; Plate.UpdateVisual(); };
    }

    private void Start() {
        //SendingFind.OnChange += SendingFind_OnChange;
    }

    //private void SendingFind_OnChange(DataFind obj) {
    //    PointSettings.style.display = DisplayStyle.None;
    //    PlateSettings.style.display = DisplayStyle.None;
    //    if (obj.IsValidPoint) { UpdateUIPointSettings(); return; }
    //    if (obj.IsValidPlate) { UpdateUIPlateSettings(); return; }
    //}
    private void UpdateUIPlateSettings() {
        PlateSettings.style.display = DisplayStyle.Flex;
    }
    private void UpdateUIPointSettings() {
        PointSettings.style.display = DisplayStyle.Flex;
        //uiPointSettings.Toggle1.SetValue(Point.isCurveFront);
        //uiPointSettings.Toggle2.SetValue(Point.isCurveAfter);
    }

    public class UIPlateSettings {
        public readonly VisualElement element;
        public UIPlateSettings(VisualElement element) => this.element = element;
    }
    public class UIPointSettings {
        public readonly VisualElement element;
        public VisualElement Bezier => element.Q<VisualElement>("Bezier");
        public MUToggle Toggle1 => element.Q<MUToggle>("Toggle1");
        public MUToggle Toggle2 => element.Q<MUToggle>("Toggle2");
        public UIPointSettings(VisualElement element) => this.element = element;
    }
}
