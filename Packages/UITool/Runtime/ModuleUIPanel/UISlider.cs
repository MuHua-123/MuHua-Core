using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// 滑块
	/// </summary>
	public class UISlider : ModuleUIPanel {
		/// <summary> 绑定的画布 </summary>
		public readonly VisualElement canvas;
		/// <summary> 元素方向 </summary>
		public readonly UIDirection direction;
		/// <summary> 值改变时 </summary>
		public event Action<float> ValueChanged;

		public float value;
		public bool isDragger;
		public float originalPosition;
		public float pointerPosition;

		public readonly UISliderFunc sliderFunc;

		public readonly VisualElement Container;
		public readonly VisualElement Tracker;
		public readonly VisualElement Dragger;

		public UISlider(VisualElement element, VisualElement canvas, UIDirection direction = UIDirection.FromLeftToRight) : base(element) {
			this.canvas = canvas;
			this.direction = direction;

			Container = element.Q<VisualElement>("Container");
			Tracker = element.Q<VisualElement>("Tracker");
			Dragger = element.Q<VisualElement>("Dragger");

			if (direction == UIDirection.FromLeftToRight) { sliderFunc = new FromLeftToRight(this); }
			if (direction == UIDirection.FromRightToLeft) { sliderFunc = new FromRightToLeft(this); }
			if (direction == UIDirection.FromTopToBottom) { sliderFunc = new FromTopToBottom(this); }
			if (direction == UIDirection.FromBottomToTop) { sliderFunc = new FromBottomToTop(this); }

			//设置事件
			Dragger.RegisterCallback<PointerDownEvent>(DraggerDown);
			Container.RegisterCallback<PointerDownEvent>(ElementDown);

			canvas.RegisterCallback<PointerUpEvent>((evt) => isDragger = false);
			canvas.RegisterCallback<PointerLeaveEvent>((evt) => isDragger = false);
		}

		private void DraggerDown(PointerDownEvent evt) => sliderFunc.DraggerDown(evt);
		private void ElementDown(PointerDownEvent evt) => sliderFunc.ElementDown(evt);
		/// <summary> 更新状态 </summary>
		public void Update() => sliderFunc.Update();
		/// <summary> 更新值(0-1) </summary>
		public void UpdateValue(float value, bool send = true) => sliderFunc.UpdateValue(value, send);

		public abstract class UISliderFunc {
			public readonly UISlider slider;
			public UISliderFunc(UISlider slider) => this.slider = slider;

			public abstract void DraggerDown(PointerDownEvent evt);
			public abstract void ElementDown(PointerDownEvent evt);
			/// <summary> 更新状态 </summary>
			public abstract void Update();
			/// <summary> 更新值(0-1) </summary>
			public abstract void UpdateValue(float value, bool send = true);
		}

		public class FromLeftToRight : UISliderFunc {
			public FromLeftToRight(UISlider slider) : base(slider) { }
			public override void DraggerDown(PointerDownEvent evt) {
				slider.isDragger = true;
				slider.originalPosition = slider.Tracker.resolvedStyle.width;
				slider.pointerPosition = UITool.GetMousePosition().x;
			}
			public override void ElementDown(PointerDownEvent evt) {
				float offset = evt.localPosition.x;
				float max = slider.Container.resolvedStyle.width;
				float value = Mathf.InverseLerp(0, max, offset);
				UpdateValue(value);
			}
			public override void Update() {
				if (!slider.isDragger) { return; }
				float differ = UITool.GetMousePosition().x - slider.pointerPosition;
				float offset = differ + slider.originalPosition;
				float max = slider.Container.resolvedStyle.width;
				float value = Mathf.InverseLerp(0, max, offset);
				UpdateValue(value);
			}
			public override void UpdateValue(float value, bool send = true) {
				slider.value = value;
				if (send) { slider.ValueChanged?.Invoke(value); }
				slider.Tracker.style.width = Length.Percent(value * 100);
			}
		}

		public class FromRightToLeft : UISliderFunc {
			public FromRightToLeft(UISlider slider) : base(slider) { }
			public override void DraggerDown(PointerDownEvent evt) {
				slider.isDragger = true;
				slider.originalPosition = slider.Container.resolvedStyle.width - slider.Tracker.resolvedStyle.width;
				slider.pointerPosition = UITool.GetMousePosition().x;
			}
			public override void ElementDown(PointerDownEvent evt) {
				float offset = evt.localPosition.x;
				float max = slider.Container.resolvedStyle.width;
				float value = Mathf.InverseLerp(max, 0, offset);
				UpdateValue(value);
			}
			public override void Update() {
				if (!slider.isDragger) { return; }
				float differ = UITool.GetMousePosition().x - slider.pointerPosition;
				float offset = differ + slider.originalPosition;
				float max = slider.Container.resolvedStyle.width;
				float value = Mathf.InverseLerp(max, 0, offset);
				UpdateValue(value);
			}
			public override void UpdateValue(float value, bool send = true) {
				slider.value = value;
				if (send) { slider.ValueChanged?.Invoke(value); }
				slider.Tracker.style.width = Length.Percent(value * 100);
			}
		}

		public class FromTopToBottom : UISliderFunc {
			public FromTopToBottom(UISlider slider) : base(slider) { }
			public override void DraggerDown(PointerDownEvent evt) {
				slider.isDragger = true;
				slider.originalPosition = slider.Tracker.resolvedStyle.height;
				slider.pointerPosition = Screen.height - UITool.GetMousePosition().y;
			}
			public override void ElementDown(PointerDownEvent evt) {
				float offset = evt.localPosition.y;
				float max = slider.Container.resolvedStyle.height;
				float value = Mathf.InverseLerp(0, max, offset);
				UpdateValue(value);
			}
			public override void Update() {
				if (!slider.isDragger) { return; }
				float differ = Screen.height - UITool.GetMousePosition().y - slider.pointerPosition;
				float offset = differ + slider.originalPosition;
				float max = slider.Container.resolvedStyle.height;
				float value = Mathf.InverseLerp(0, max, offset);
				UpdateValue(value);
			}
			public override void UpdateValue(float value, bool send = true) {
				slider.value = value;
				if (send) { slider.ValueChanged?.Invoke(value); }
				slider.Tracker.style.height = Length.Percent(value * 100);
			}
		}

		public class FromBottomToTop : UISliderFunc {
			public FromBottomToTop(UISlider slider) : base(slider) { }
			public override void DraggerDown(PointerDownEvent evt) {
				slider.isDragger = true;
				slider.originalPosition = slider.Container.resolvedStyle.height - slider.Tracker.resolvedStyle.height;
				slider.pointerPosition = Screen.height - UITool.GetMousePosition().y;
			}
			public override void ElementDown(PointerDownEvent evt) {
				float offset = evt.localPosition.y;
				float max = slider.Container.resolvedStyle.height;
				float value = Mathf.InverseLerp(max, 0, offset);
				UpdateValue(value);
			}
			public override void Update() {
				if (!slider.isDragger) { return; }
				float differ = Screen.height - UITool.GetMousePosition().y - slider.pointerPosition;
				float offset = differ + slider.originalPosition;
				float max = slider.Container.resolvedStyle.height;
				float value = Mathf.InverseLerp(max, 0, offset);
				UpdateValue(value);
			}
			public override void UpdateValue(float value, bool send = true) {
				slider.value = value;
				if (send) { slider.ValueChanged?.Invoke(value); }
				slider.Tracker.style.height = Length.Percent(value * 100);
			}
		}
	}
}
