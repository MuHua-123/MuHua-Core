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

	private UISlider slider1;
	private UISlider slider2;
	private UISlider slider3;
	private UISlider slider4;

	private void Awake() {
		VisualElement ScrollView = root.Q<VisualElement>("ScrollView");
		scrollView = new UIScrollView(ScrollView, root, UIDirection.HorizontalAndVertical);

		VisualElement ScrollViewHorizontal = root.Q<VisualElement>("ScrollViewHorizontal");
		scrollViewHorizontal = new UIScrollView(ScrollViewHorizontal, root, UIDirection.Horizontal);

		VisualElement ScrollViewVertical = root.Q<VisualElement>("ScrollViewVertical");
		scrollViewVertical = new UIScrollView(ScrollViewVertical, root, UIDirection.Vertical);

		VisualElement Slider1 = root.Q<VisualElement>("SliderHorizontal1");
		slider1 = new UISlider(Slider1, root, UIDirection.FromLeftToRight);

		VisualElement Slider2 = root.Q<VisualElement>("SliderHorizontal2");
		slider2 = new UISlider(Slider2, root, UIDirection.FromRightToLeft);

		VisualElement Slider3 = root.Q<VisualElement>("SliderVertical1");
		slider3 = new UISlider(Slider3, root, UIDirection.FromTopToBottom);

		VisualElement Slider4 = root.Q<VisualElement>("SliderVertical2");
		slider4 = new UISlider(Slider4, root, UIDirection.FromBottomToTop);

		slider4.ValueChanged += (obj) => { Debug.Log(obj); };
	}
	private void Update() {
		scrollView.Update();
		scrollViewHorizontal.Update();
		scrollViewVertical.Update();

		slider1.Update();
		slider2.Update();
		slider3.Update();
		slider4.Update();
	}
}
