using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

public class TestUI : MonoBehaviour {
	/// <summary> 绑定文档 </summary>
	public UIDocument document;
	/// <summary> 根目录文档 </summary>
	public VisualElement root => document.rootVisualElement;

	private UIScrollView scrollView;
	private UIScrollView scrollViewHorizontal;
	private UIScrollView scrollViewVertical;
	private void Awake() {
		VisualElement ScrollView = root.Q<VisualElement>("ScrollView");
		scrollView = new UIScrollView(ScrollView, root, UIDirection.HorizontalAndVertical);

		VisualElement ScrollViewHorizontal = root.Q<VisualElement>("ScrollViewHorizontal");
		scrollViewHorizontal = new UIScrollView(ScrollViewHorizontal, root, UIDirection.Horizontal);

		VisualElement ScrollViewVertical = root.Q<VisualElement>("ScrollViewVertical");
		scrollViewVertical = new UIScrollView(ScrollViewVertical, root, UIDirection.Vertical);
	}
	private void Update() {
		scrollView.Update();
		scrollViewHorizontal.Update();
		scrollViewVertical.Update();
	}
}
