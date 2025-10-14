using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// UI弹出管理器
/// </summary>
public class UIPopupManager : ModuleUISingle<UIPopupManager> {
	/// <summary> 菜单模板 </summary>
	public VisualTreeAsset menuTreeAsset;
	/// <summary> 项目模板 </summary>
	public VisualTreeAsset itemTreeAsset;

	public UILoading loading;
	public UIShortcutMenu shortcutMenu;

	public override VisualElement Element => root.Q<VisualElement>("Popup");

	public VisualElement Loading => Q<VisualElement>("Loading");
	public VisualElement ShortcutMenu => Q<VisualElement>("ShortcutMenu");

	protected override void Awake() {
		NoReplace(false);
		loading = new UILoading(Loading, root);
		shortcutMenu = new UIShortcutMenu(ShortcutMenu, menuTreeAsset, itemTreeAsset);

		SceneSystem.OnProgress = (active, value) => loading.Settings(active, value, "场景加载中》》》");
	}
}
