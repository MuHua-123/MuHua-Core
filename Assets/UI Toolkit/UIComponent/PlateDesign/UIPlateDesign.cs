using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIPlateDesign {
    public readonly VisualElement element;
    public ModuleViewInput viewInput => ModuleCore.I.PlateDesignViewInput;
    public ModuleViewCamera viewCamera => ModuleCore.I.PlateDesignViewCamera;
    public VisualElement Rendering => element.Q<VisualElement>("Rendering");
    public Button Button1 => element.Q<Button>("Button1");
    public Button Button2 => element.Q<Button>("Button2");
    public Button Button3 => element.Q<Button>("Button3");
    public Button Button4 => element.Q<Button>("Button4");
    public Button Button5 => element.Q<Button>("Button5");
    public UIPlateDesign(VisualElement element) {
        this.element = element;
        viewInput.OnInputType += ViewInput_OnInputType;
        element.generateVisualContent += Element_GenerateVisualContent;
        Rendering.RegisterCallback<MouseDownEvent>(DownMouse);
        Rendering.RegisterCallback<MouseMoveEvent>(DragMouse);
        Rendering.RegisterCallback<MouseUpEvent>(ReleaseMouse);
        Rendering.RegisterCallback<MouseOutEvent>(ReleaseMouse);
        Rendering.RegisterCallback<WheelEvent>(ScrollWheel);
        //边缘点操作
        //Button1.clicked += () => { viewInput.SetPrimaryKeyInput(new VIUEdgePointMobile(viewCamera)); };
        //Button2.clicked += () => { viewInput.SetPrimaryKeyInput(new VIUEdgePointAdd(viewCamera)); };
        //设计点操作
        Button1.clicked += () => { viewInput.SetPrimaryKeyInput(new VIUDesignPointMobile(viewCamera)); };
        Button2.clicked += () => { viewInput.SetPrimaryKeyInput(new VIUDesignPointInsert(viewCamera)); };
        Button3.clicked += () => { viewInput.SetPrimaryKeyInput(new VIUBezierMobile(viewCamera)); };

        viewInput.SetPrimaryKeyInput(new VIUDesignPointMobile(viewCamera));
    }
    private void ViewInput_OnInputType(Type obj) {
        //边缘点操作
        //ChangeStyle(obj, typeof(VIUEdgePointMobile), Button1);
        //ChangeStyle(obj, typeof(VIUEdgePointAdd), Button2);
        //设计点操作
        ChangeStyle(obj, typeof(VIUDesignPointMobile), Button1);
        ChangeStyle(obj, typeof(VIUDesignPointInsert), Button2);
        ChangeStyle(obj, typeof(VIUBezierMobile), Button3);
    }
    private void ChangeStyle(Type obj, Type compare, VisualElement visualElement) {
        if (obj == compare) { visualElement.AddToClassList("pd-button-s"); }
        else { visualElement.RemoveFromClassList("pd-button-s"); }
    }
    private void Element_GenerateVisualContent(MeshGenerationContext context) {
        ModuleCore.I.ModuleAgent.StartCoroutine(UpdateRenderTexture());
    }
    private IEnumerator UpdateRenderTexture() {
        yield return null;
        int width = (int)element.resolvedStyle.width;
        int height = (int)element.resolvedStyle.height;
        viewCamera.UpdateRenderTexture(width, height);
        Background background = Background.FromRenderTexture(viewCamera.RenderTexture);
        StyleBackground style = new StyleBackground(background);
        Rendering.style.backgroundImage = style;
    }

    private bool isDownLeftMouse;
    private bool isDownRightMouse;
    private bool isDownMiddleMouse;
    public void DownMouse(MouseDownEvent evt) {
        DataMouseInput mouseInput = new DataMouseInput(evt);
        if (evt.button == 0) { viewInput.DownLeftMouse(mouseInput); isDownLeftMouse = true; }
        if (evt.button == 1) { viewInput.DownRightMouse(mouseInput); isDownRightMouse = true; }
        if (evt.button == 2) { viewInput.DownMiddleMouse(mouseInput); isDownMiddleMouse = true; }
    }
    public void DragMouse(MouseMoveEvent evt) {
        DataMouseInput mouseInput = new DataMouseInput(evt);
        if (isDownLeftMouse) { viewInput.DragLeftMouse(mouseInput); }
        if (isDownRightMouse) { viewInput.DragRightMouse(mouseInput); }
        if (isDownMiddleMouse) { viewInput.DragMiddleMouse(mouseInput); }
        if (evt.button == 0) { viewInput.MoveLeftMouse(mouseInput); }
        if (evt.button == 1) { viewInput.MoveRightMouse(mouseInput); }
        if (evt.button == 2) { viewInput.MoveMiddleMouse(mouseInput); }
    }
    public void ReleaseMouse(MouseUpEvent evt) {
        DataMouseInput mouseInput = new DataMouseInput(evt);
        viewInput.ReleaseLeftMouse(mouseInput); isDownLeftMouse = false;
        viewInput.ReleaseRightMouse(mouseInput); isDownRightMouse = false;
        viewInput.ReleaseMiddleMouse(mouseInput); isDownMiddleMouse = false;
    }
    public void ReleaseMouse(MouseOutEvent evt) {
        DataMouseInput mouseInput = new DataMouseInput(evt);
        viewInput.ReleaseLeftMouse(mouseInput); isDownLeftMouse = false;
        viewInput.ReleaseRightMouse(mouseInput); isDownRightMouse = false;
        viewInput.ReleaseMiddleMouse(mouseInput); isDownMiddleMouse = false;
    }
    public void ScrollWheel(WheelEvent evt) {
        DataMouseInput mouseInput = new DataMouseInput(evt);
        viewInput.ScrollWheel(mouseInput);
    }
}
