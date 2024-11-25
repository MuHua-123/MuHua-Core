using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIControlDesign : UnitUIControl {

    #region UI元素
    public override VisualElement Element => ModuleUIPage.Q<VisualElement>("PlateDesign");
    public VisualElement Rendering => Element.Q<VisualElement>("Rendering");
    public Button Button1 => Element.Q<Button>("Button1");
    public Button Button2 => Element.Q<Button>("Button2");
    public Button Button3 => Element.Q<Button>("Button3");
    public Button Button4 => Element.Q<Button>("Button4");
    public Button Button5 => Element.Q<Button>("Button5");
    public Button Button6 => Element.Q<Button>("Button6");
    public Button Button7 => Element.Q<Button>("Button7");
    #endregion

    #region 引用模块
    /// <summary> 设计视图相机模块 </summary>
    public ModuleViewCamera ViewCamera => ModuleCore.ViewCameraDesign;
    /// <summary> 设计UI输入模块 </summary>
    public ModuleUIInput<UnitMouseInput> UIInputDesign => ModuleCore.UIInputDesign;
    #endregion

    public override void Awake() {
        Element.generateVisualContent += Element_GenerateVisualContent;
        Button1.clicked += () => { UIInputDesign.ChangeInput(new DesignMobile()); };
        Button2.clicked += () => { UIInputDesign.ChangeInput(new DesignInsert()); };
        Button3.clicked += () => { UIInputDesign.ChangeInput(new DesignBezier()); };
        Button4.clicked += () => { UIInputDesign.ChangeInput(new DesignSuture()); };
        Button5.clicked += () => { UIInputDesign.ChangeInput(new DesignSutureReversal()); };
    }
    private void Start() {
        UIInputDesign.Binding(Rendering);
        UIInputDesign.OnChangeInput += UIInputDesign_OnChangeInput;
        UIInputDesign.ChangeInput(new DesignMobile());
    }
    private void OnDestroy() {
        if (UIInputDesign == null) { return; }
        UIInputDesign.OnChangeInput -= UIInputDesign_OnChangeInput;
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

    private void UIInputDesign_OnChangeInput(UnitMouseInput obj) {
        Type type = obj.GetType();
        ButtonStyleChange(type, typeof(DesignMobile), Button1);
        ButtonStyleChange(type, typeof(DesignInsert), Button2);
        ButtonStyleChange(type, typeof(DesignBezier), Button3);
        ButtonStyleChange(type, typeof(DesignSuture), Button4);
        ButtonStyleChange(type, typeof(DesignSutureReversal), Button5);
    }
    private void ButtonStyleChange(Type obj, Type compare, Button button) {
        if (obj == compare) { button.AddToClassList("pd-button-s"); }
        else { button.RemoveFromClassList("pd-button-s"); }
    }
}
