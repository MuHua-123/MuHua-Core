using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIControlBaking : UnitUIControl {

    #region UI元素
    public override VisualElement Element => ModuleUIPage.Q<VisualElement>("PlateBaking");
    public VisualElement Rendering => Element.Q<VisualElement>("Rendering");
    public Button Button1 => Element.Q<Button>("Button1");
    public Button Button2 => Element.Q<Button>("Button2");
    public Button Button3 => Element.Q<Button>("Button3");
    public Button Button4 => Element.Q<Button>("Button4");
    public Button Button5 => Element.Q<Button>("Button5");
    #endregion

    #region 引用模块
    /// <summary> 视图相机模块 </summary>
    public ModuleViewCamera ViewCamera => ModuleCore.ViewCameraBaking;
    /// <summary> UI输入模块 </summary>
    public ModuleUIInput<UnitMouseInput> UIInput => ModuleCore.UIInputBaking;
    #endregion

    public override void Awake() {
        Element.generateVisualContent += Element_GenerateVisualContent;
        Button1.clicked += () => { UIInput.ChangeInput(new BakingMobile()); };
        Button2.clicked += () => { UIInput.ChangeInput(new BakingMobilePlate()); };
        //Button3.clicked += () => { UIInput.ChangeInput(new DesignBezier()); };
        //Button4.clicked += () => { UIInputDesign.ChangeInput(new IDesignSelect()); };
        //Button5.clicked += () => { UIInputDesign.ChangeInput(new IDesignSelect()); };
    }
    private void Start() {
        UIInput.Binding(Rendering);
        UIInput.OnChangeInput += UIInputDesign_OnChangeInput;
        UIInput.ChangeInput(new BakingMobile());
    }

    #region 更新视图
    private void Element_GenerateVisualContent(MeshGenerationContext context) {
        StartCoroutine(UpdateRenderTexture());
    }
    private IEnumerator UpdateRenderTexture() {
        yield return null;
        int width = (int)Element.resolvedStyle.width;
        int height = (int)Element.resolvedStyle.height;
        ViewCamera.UpdateRenderTexture(width, height);
        Background background = Background.FromRenderTexture(ViewCamera.RenderTexture);
        StyleBackground style = new StyleBackground(background);
        Rendering.style.backgroundImage = style;
    }
    #endregion

    #region 输入功能
    private void UIInputDesign_OnChangeInput(UnitMouseInput obj) {
        Type type = obj.GetType();
        ButtonStyleChange(type, typeof(BakingMobile), Button1);
        ButtonStyleChange(type, typeof(BakingMobilePlate), Button2);
        //ButtonStyleChange(type, typeof(DesignBezier), Button3);
    }
    private void ButtonStyleChange(Type obj, Type compare, Button button) {
        if (obj == compare) { button.AddToClassList("pb-button-s"); }
        else { button.RemoveFromClassList("pb-button-s"); }
    }
    #endregion

}
