using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// UI模块
/// </summary>
public class ModuleUI : ModuleUISingle<ModuleUI> {
	/// <summary> 当前页面 </summary>
	public static Page page;
	/// <summary> 跳转页面事件 </summary>
	public static event Action<Page> OnJumpPage;
	/// <summary> UI控件列表 </summary>
	public static List<UIControl> controls = new List<UIControl>();
	/// <summary> 跳转页面 </summary>
	public static void Settings(Page pageType) => OnJumpPage?.Invoke(pageType);
	/// <summary> 添加UI控件 </summary>
	public static void AddControl(UIControl control) => controls.Add(control);
	/// <summary> 移除UI控件 </summary>
	public static void RemoveControl(UIControl control) => controls.Remove(control);

	public override VisualElement Element => document.rootVisualElement;

	protected override void Awake() => NoReplace();

	private void Update() => controls.ForEach(c => c.Update());

	private void OnDestroy() => controls.ForEach(c => c.Dispose());
}
/// <summary>
/// 页面类型
/// </summary>
public enum Page {
	None,

	Scene,// 场景选择
	Module,// 模组页面
}