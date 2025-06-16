using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// 测试页面
/// </summary>
public class UITestPage : ModuleUIPage {
	public VisualTreeAsset TemplateAsset;
	public List<string> list;

	private UIToggle toggle;
	private UIDropdown<string> dropdown;
	private UIScrollView scrollView;

	public override VisualElement Element => root;

	public VisualElement Toggle => Q<VisualElement>("Toggle");
	public VisualElement Dropdown => Q<VisualElement>("Dropdown");
	public VisualElement ScrollView => Q<VisualElement>("ScrollView");

	private void Awake() {
		toggle = new UIToggle(Toggle);
		toggle.ValueChanged += (value) => Debug.Log(value);

		dropdown = new UIDropdown<string>(Dropdown, root, TemplateAsset);
		dropdown.SetValue(list);
		dropdown.ValueChanged += (value) => Debug.Log(value);

		scrollView = new UIScrollView(ScrollView, root, UIDirection.Vertical, UIDirection.FromLeftToRight, UIDirection.FromTopToBottom);
	}
	private void Update() {
		dropdown.Update();
		scrollView.Update();
	}
	private void OnDestroy() {
		dropdown.Release();
	}
}
