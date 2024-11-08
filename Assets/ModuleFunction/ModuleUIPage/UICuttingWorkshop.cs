using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

public class UICuttingWorkshop : ModuleUIPage {
    [SceneName] public string scene1;
    [SceneName] public string scene2;
    [SceneName] public string scene3;
    [SceneName] public string returnScene;
    private Button Button1 => root.Q<Button>("Button1");
    private Button Button2 => root.Q<Button>("Button2");
    private Button Button3 => root.Q<Button>("Button3");
    private Button Button4 => root.Q<Button>("Button4");
    private Button Button5 => root.Q<Button>("Button5");
    protected override void Awake() {
        ModuleCore.FunctionRegister(this);
        Button1.clicked += Button1_clicked;
        Button2.clicked += Button2_clicked;
        Button3.clicked += Button3_clicked;
        Button4.clicked += Button4_clicked;
        Button5.clicked += Button5_clicked;
    }
    private void Button1_clicked() {
        //ModuleCore.ModuleScene.LoadSceneAsync(scene1);
    }
    private void Button2_clicked() {
        //ModuleCore.ModuleScene.LoadSceneAsync(scene2);
    }
    private void Button3_clicked() {
        //ModuleCore.ModuleScene.LoadSceneAsync(scene3);
    }
    private void Button4_clicked() {
        //ModuleCore.ModuleScene.LoadSceneAsync(returnScene);
    }
    private void Button5_clicked() {
        //ModuleCore.LearningVideoPanel.Open(null);
    }
}
