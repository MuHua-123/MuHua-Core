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
	private UIScrollView scrollView1;
	private UIScrollView scrollView2;
	private UIScrollView scrollView3;
	private UIScrollView scrollView4;

	public override VisualElement Element => root;

	public VisualElement Toggle => Q<VisualElement>("Toggle");
	public VisualElement Dropdown => Q<VisualElement>("Dropdown");
	public VisualElement ScrollView1 => Q<VisualElement>("ScrollView1");
	public VisualElement ScrollView2 => Q<VisualElement>("ScrollView2");
	public VisualElement ScrollView3 => Q<VisualElement>("ScrollView3");
	public VisualElement ScrollView4 => Q<VisualElement>("ScrollView4");

	private void Awake() {
		toggle = new UIToggle(Toggle);
		toggle.ValueChanged += (value) => Debug.Log(value);

		dropdown = new UIDropdown<string>(Dropdown, root, TemplateAsset);
		dropdown.SetValue(list);
		dropdown.ValueChanged += (value) => Debug.Log(value);

		scrollView1 = new UIScrollView(ScrollView1, root, UIDirection.Vertical, UIDirection.FromLeftToRight, UIDirection.FromTopToBottom);
		scrollView2 = new UIScrollView(ScrollView2, root, UIDirection.Vertical, UIDirection.FromLeftToRight, UIDirection.FromBottomToTop);
		scrollView3 = new UIScrollView(ScrollView3, root, UIDirection.Horizontal, UIDirection.FromLeftToRight);
		scrollView4 = new UIScrollView(ScrollView4, root, UIDirection.Horizontal, UIDirection.FromLeftToRight);
	}
	private void Update() {
		dropdown.Update();
		scrollView1.Update();
		scrollView2.Update();
		scrollView3.Update();
		scrollView4.Update();
	}
	private void OnDestroy() {
		dropdown.Release();
	}
}
