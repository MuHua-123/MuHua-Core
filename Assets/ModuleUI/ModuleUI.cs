using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// UI模块
/// </summary>
public class ModuleUI : ModuleSingle<ModuleUI> {
	public static Page page;
	public static event Action<Page> OnJumpPage;

	public UIDocument document;// 绑定文档

	/// <summary> 根目录文档 </summary>
	public VisualElement root => document.rootVisualElement;

	protected override void Awake() => NoReplace();

	/// <summary> 跳转页面 </summary>
	public static void Jump(Page pageType) => OnJumpPage?.Invoke(pageType);
}
/// <summary>
/// 页面类型
/// </summary>
public enum Page {
	None,

	Menu,// 主菜单
	Scene,// 场景选择
	Prepare,// 准备游戏
	Battle,// 战斗页面
	Settlement,// 结算页面

	Settings,// 游戏设置
}