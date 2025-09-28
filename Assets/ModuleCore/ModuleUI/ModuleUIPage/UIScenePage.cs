using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// 场景页面
/// </summary>
public class UIScenePage : ModuleUIPage {

	public VisualTreeAsset SceneTemplate;

	private UISceneList sceneList;

	public override VisualElement Element => root.Q<VisualElement>("ScenePage");

	public VisualElement SceneList => Q<VisualElement>("SceneList");

	protected void Awake() {
		sceneList = new UISceneList(SceneList, root, SceneTemplate, SceneSystem.I.Load);

		ModuleUI.OnJumpPage += ModuleUI_OnJumpPage;
	}

	private void ModuleUI_OnJumpPage(Page page) {
		Element.EnableInClassList("document-page-hide", page != Page.Scene);
		if (page != Page.Scene) { return; }
		sceneList.Initial();
	}
}
