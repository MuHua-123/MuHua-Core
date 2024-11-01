using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

public class UIFactoryScenery : ModuleUIPage {
    public VisualTreeAsset NavigationUnitlAsset;
    [SceneName] public string returnScene;
    public List<NavigationData> navigationDatas = new List<NavigationData>();
    private Button Button1 => root.Q<Button>("Button1");
    private Button Button2 => root.Q<Button>("Button2");
    private Button Button3 => root.Q<Button>("Button3");
    private Button Button4 => root.Q<Button>("Button4");
    private MUFoldout Foldout => root.Q<MUFoldout>("MUFoldout");
    protected override void Awake() {
        base.Awake();
        Button1.clicked += Button1_clicked;
        Button2.clicked += Button2_clicked;
        Button3.clicked += Button3_clicked;
        Button4.clicked += Button4_clicked;
        navigationDatas.ForEach(CreateUINavigationUnit);
    }
    private void Button1_clicked() {
        Debug.Log("自动漫游");
    }
    private void Button2_clicked() {
        Debug.Log("手动漫游");
    }
    private void Button3_clicked() {
        Debug.Log("地图导航");
    }
    private void Button4_clicked() {
        ModuleCore.ModuleScene.LoadSceneAsync(returnScene);
    }
    public void CreateUINavigationUnit(NavigationData data) {
        VisualElement element = NavigationUnitlAsset.Instantiate();
        UINavigationUnit foldout = new UINavigationUnit(data, element);
        Foldout.AddContainer(foldout.element);
    }
}
[Serializable]
public class NavigationData {
    public string name;
    [SceneName] public string scene;
}
public class UINavigationUnit {
    public readonly NavigationData value;
    public readonly VisualElement element;
    public Button Button => element.Q<Button>();
    public ModuleCore ModuleCore => ModuleCore.I;
    public UINavigationUnit(NavigationData value, VisualElement element) {
        this.value = value;
        this.element = element;
        Button.text = value.name;
        Button.clicked += Button_clicked;
    }
    private void Button_clicked() {
        ModuleCore.ModuleScene.LoadSceneAsync(value.scene);
    }
}