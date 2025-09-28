using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// 加载页面
/// </summary>
public class UILoading : ModuleUIPanel {

	public UISliderH slider;

	public VisualElement Slider => Q<VisualElement>("Slider");

	public UILoading(VisualElement element, VisualElement canvas) : base(element) {
		slider = new UISliderH(Slider, canvas);
	}

	/// <summary> 设置加载进度 </summary> 
	public void Settings(bool active, float value1, string value2) {
		element.EnableInClassList("document-page-hide", !active);
		slider.UpdateValue(value1);
		slider.Title.text = value2;
	}
}
