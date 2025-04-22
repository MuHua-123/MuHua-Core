using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// 滚动条
	/// </summary>
	public class UIScroller : ModuleUIPanel {
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

		public readonly UIScrollerFunc scrollerFunc;

		public VisualElement Dragger => Q<VisualElement>("Dragger");

		public UIScroller(VisualElement element, VisualElement canvas, UIDirection direction = UIDirection.FromLeftToRight) : base(element) {
			this.canvas = canvas;
			this.direction = direction;

			if (direction == UIDirection.FromLeftToRight) { scrollerFunc = new FromLeftToRight(this); }
			if (direction == UIDirection.FromRightToLeft) { scrollerFunc = new FromRightToLeft(this); }
			if (direction == UIDirection.FromTopToBottom) { scrollerFunc = new FromTopToBottom(this); }
			if (direction == UIDirection.FromBottomToTop) { scrollerFunc = new FromBottomToTop(this); }

			//设置事件
			Dragger.RegisterCallback<PointerDownEvent>(DraggerDown);
			element.RegisterCallback<PointerDownEvent>(ElementDown);

			canvas.RegisterCallback<PointerUpEvent>((evt) => isDragger = false);
			canvas.RegisterCallback<PointerLeaveEvent>((evt) => isDragger = false);
		}

		private void DraggerDown(PointerDownEvent evt) => scrollerFunc.DraggerDown(evt);
		private void ElementDown(PointerDownEvent evt) => scrollerFunc.ElementDown(evt);
		/// <summary> 更新状态 </summary>
		public void Update() => scrollerFunc.Update();
		/// <summary> 更新值(0-1) </summary>
		public void UpdateValue(float value, bool send = true) => scrollerFunc.UpdateValue(value, send);

		public abstract class UIScrollerFunc {
			public readonly UIScroller scroller;
			public UIScrollerFunc(UIScroller scroller) => this.scroller = scroller;

			public abstract void DraggerDown(PointerDownEvent evt);
			public abstract void ElementDown(PointerDownEvent evt);
			/// <summary> 更新状态 </summary>
			public abstract void Update();
			/// <summary> 更新值(0-1) </summary>
			public abstract void UpdateValue(float value, bool send = true);
		}

		public class FromLeftToRight : UIScrollerFunc {
			public FromLeftToRight(UIScroller scroller) : base(scroller) { }
			public override void DraggerDown(PointerDownEvent evt) {
				scroller.isDragger = true;
				scroller.originalPosition = scroller.Dragger.transform.position.x;
				scroller.pointerPosition = UITool.GetMousePosition().x;
			}
			public override void ElementDown(PointerDownEvent evt) {
				float offset = evt.localPosition.x - scroller.Dragger.resolvedStyle.width * 0.5f;
				float max = scroller.element.resolvedStyle.width - scroller.Dragger.resolvedStyle.width;
				float value = Mathf.InverseLerp(0, max, offset);
				UpdateValue(value);
			}
			public override void Update() {
				if (!scroller.isDragger) { return; }
				float differ = UITool.GetMousePosition().x - scroller.pointerPosition;
				float offset = differ + scroller.originalPosition;
				float max = scroller.element.resolvedStyle.width - scroller.Dragger.resolvedStyle.width;
				float value = Mathf.InverseLerp(0, max, offset);
				UpdateValue(value);
			}
			public override void UpdateValue(float value, bool send = true) {
				scroller.value = value;
				if (send) { scroller.ValueChanged?.Invoke(value); }
				float max = scroller.element.resolvedStyle.width - scroller.Dragger.resolvedStyle.width;
				float x = Mathf.Lerp(0, max, value);
				scroller.Dragger.transform.position = new Vector3(x, 0);
			}
		}

		public class FromRightToLeft : UIScrollerFunc {
			public FromRightToLeft(UIScroller scroller) : base(scroller) { }
			public override void DraggerDown(PointerDownEvent evt) {
				scroller.isDragger = true;
				scroller.originalPosition = scroller.Dragger.transform.position.x;
				scroller.pointerPosition = UITool.GetMousePosition().x;
			}
			public override void ElementDown(PointerDownEvent evt) {
				float offset = evt.localPosition.x - scroller.Dragger.resolvedStyle.width * 0.5f;
				float max = scroller.element.resolvedStyle.width - scroller.Dragger.resolvedStyle.width;
				float value = Mathf.InverseLerp(max, 0, offset);
				UpdateValue(value);
			}
			public override void Update() {
				if (!scroller.isDragger) { return; }
				float differ = UITool.GetMousePosition().x - scroller.pointerPosition;
				float offset = differ + scroller.originalPosition;
				float max = scroller.element.resolvedStyle.width - scroller.Dragger.resolvedStyle.width;
				float value = Mathf.InverseLerp(max, 0, offset);
				UpdateValue(value);
			}
			public override void UpdateValue(float value, bool send = true) {
				scroller.value = value;
				if (send) { scroller.ValueChanged?.Invoke(value); }
				float max = scroller.element.resolvedStyle.width - scroller.Dragger.resolvedStyle.width;
				float x = Mathf.Lerp(max, 0, value);
				scroller.Dragger.transform.position = new Vector3(x, 0);
			}
		}

		public class FromTopToBottom : UIScrollerFunc {
			public FromTopToBottom(UIScroller scroller) : base(scroller) { }
			public override void DraggerDown(PointerDownEvent evt) {
				scroller.isDragger = true;
				scroller.originalPosition = scroller.Dragger.transform.position.y;
				scroller.pointerPosition = Screen.height - UITool.GetMousePosition().y;
			}
			public override void ElementDown(PointerDownEvent evt) {
				float offset = evt.localPosition.y - scroller.Dragger.resolvedStyle.height * 0.5f;
				float max = scroller.element.resolvedStyle.height - scroller.Dragger.resolvedStyle.height;
				float value = Mathf.InverseLerp(0, max, offset);
				UpdateValue(value);
			}
			public override void Update() {
				if (!scroller.isDragger) { return; }
				float differ = Screen.height - UITool.GetMousePosition().y - scroller.pointerPosition;
				float offset = differ + scroller.originalPosition;
				float max = scroller.element.resolvedStyle.height - scroller.Dragger.resolvedStyle.height;
				float value = Mathf.InverseLerp(0, max, offset);
				UpdateValue(value);
			}
			public override void UpdateValue(float value, bool send = true) {
				scroller.value = value;
				if (send) { scroller.ValueChanged?.Invoke(value); }
				float max = scroller.element.resolvedStyle.height - scroller.Dragger.resolvedStyle.height;
				float y = Mathf.Lerp(0, max, value);
				scroller.Dragger.transform.position = new Vector3(0, y);
			}
		}

		public class FromBottomToTop : UIScrollerFunc {
			public FromBottomToTop(UIScroller scroller) : base(scroller) { }
			public override void DraggerDown(PointerDownEvent evt) {
				scroller.isDragger = true;
				scroller.originalPosition = scroller.Dragger.transform.position.y;
				scroller.pointerPosition = Screen.height - UITool.GetMousePosition().y;
			}
			public override void ElementDown(PointerDownEvent evt) {
				float offset = evt.localPosition.y - scroller.Dragger.resolvedStyle.height * 0.5f;
				float max = scroller.element.resolvedStyle.height - scroller.Dragger.resolvedStyle.height;
				float value = Mathf.InverseLerp(max, 0, offset);
				UpdateValue(value);
			}
			public override void Update() {
				if (!scroller.isDragger) { return; }
				float differ = Screen.height - UITool.GetMousePosition().y - scroller.pointerPosition;
				float offset = differ + scroller.originalPosition;
				float max = scroller.element.resolvedStyle.height - scroller.Dragger.resolvedStyle.height;
				float value = Mathf.InverseLerp(max, 0, offset);
				UpdateValue(value);
			}
			public override void UpdateValue(float value, bool send = true) {
				scroller.value = value;
				if (send) { scroller.ValueChanged?.Invoke(value); }
				float max = scroller.element.resolvedStyle.height - scroller.Dragger.resolvedStyle.height;
				float y = Mathf.Lerp(max, 0, value);
				scroller.Dragger.transform.position = new Vector3(0, y);
			}
		}
	}
}
