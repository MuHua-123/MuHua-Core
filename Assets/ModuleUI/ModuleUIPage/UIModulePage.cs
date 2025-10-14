using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// 模组页面
/// </summary>
public class UIModulePage : ModuleUIPage {

	public VisualTreeAsset ModuleTemplate;

	private UIModuleList moduleList;

	public override VisualElement Element => root.Q<VisualElement>("ModulePage");

	public VisualElement ModuleList => Q<VisualElement>("ModuleList");

	protected void Awake() {
		moduleList = new UIModuleList(ModuleList, root, ModuleTemplate);

		ModuleUI.OnJumpPage += ModuleUI_OnJumpPage;
	}

	private void ModuleUI_OnJumpPage(Page page) {
		Element.EnableInClassList("document-page-hide", page != Page.Module);
		if (page != Page.Module) { return; }
		moduleList.Initial();
	}
}
