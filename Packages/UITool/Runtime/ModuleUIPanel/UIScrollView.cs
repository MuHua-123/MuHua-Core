using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// 滚动视图
	/// </summary>
	public class UIScrollView : ModuleUIPanel {
		/// <summary> 绑定的画布 </summary>
		public readonly VisualElement canvas;
		/// <summary> 元素方向 </summary>
		public readonly UIDirection direction;
		/// <summary> 值改变时 </summary>
		public event Action<Vector2> ValueChanged;

		public Vector2 value;
		public bool isDrag;
		public Vector3 originalPosition;
		public Vector3 pointerPosition;

		public readonly UIScroller horizontal;
		public readonly UIScroller vertical;

		public VisualElement Viewport => Q<VisualElement>("Viewport");
		public VisualElement Container => Q<VisualElement>("Container");
		public VisualElement ScrollerHorizontal => Q<VisualElement>("ScrollerHorizontal");
		public VisualElement ScrollerVertical => Q<VisualElement>("ScrollerVertical");

		public UIScrollView(VisualElement element, VisualElement canvas,
		UIDirection direction = UIDirection.HorizontalAndVertical,
		UIDirection sh = UIDirection.FromLeftToRight,
		UIDirection sv = UIDirection.FromTopToBottom) : base(element) {
			this.canvas = canvas;
			this.direction = direction;

			element.generateVisualContent += ElementGenerateVisualContent;

			if (sh == UIDirection.FromLeftToRight) { horizontal = new UIScroller(ScrollerHorizontal, canvas, sh); }
			if (sh == UIDirection.FromRightToLeft) { horizontal = new UIScroller(ScrollerHorizontal, canvas, sh); }

			if (sv == UIDirection.FromTopToBottom) { vertical = new UIScroller(ScrollerVertical, canvas, sv); }
			if (sv == UIDirection.FromBottomToTop) { vertical = new UIScroller(ScrollerVertical, canvas, sv); }

			//设置事件
			horizontal.ValueChanged += (x) => { UpdateValue(new Vector2(x, value.y)); };
			vertical.ValueChanged += (y) => { UpdateValue(new Vector2(value.x, y)); };

			Viewport.RegisterCallback<WheelEvent>(ViewportWheel);
			Viewport.RegisterCallback<PointerDownEvent>(DraggerDown);
			Viewport.RegisterCallback<MouseCaptureEvent>((evt) => isDrag = false);

			canvas.RegisterCallback<PointerUpEvent>((evt) => isDrag = false);
			canvas.RegisterCallback<PointerLeaveEvent>((evt) => isDrag = false);
		}
		/// <summary> 视图原始更新 </summary>
		private void ElementGenerateVisualContent(MeshGenerationContext context) {
			float width = Mathf.Clamp01(Viewport.resolvedStyle.width / Container.resolvedStyle.width);
			float height = Mathf.Clamp01(Viewport.resolvedStyle.height / Container.resolvedStyle.height);

			horizontal.Dragger.style.width = Length.Percent(width * 100);
			vertical.Dragger.style.height = Length.Percent(height * 100);
		}
		/// <summary> 视图滚轮滑动 </summary>
		private void ViewportWheel(WheelEvent evt) {
			float wheel = Mathf.Clamp(evt.delta.y, -1, 1);
			if (direction == UIDirection.Horizontal) {
				UpdateValue(new Vector2(value.x - wheel, value.y));
			}
			else {
				UpdateValue(new Vector2(value.x, value.y - wheel));
			}
		}
		private void DraggerDown(PointerDownEvent evt) {
			isDrag = true;
			originalPosition = Container.transform.position;
			Vector3 mousePosition = UITool.GetMousePosition();
			pointerPosition = new Vector3(mousePosition.x, Screen.height - mousePosition.y);
		}

		/// <summary> 更新状态 </summary>
		public virtual void Update() {
			horizontal.Update();
			vertical.Update();

			Vector2 original = value;
			float maxX = Viewport.resolvedStyle.width < Container.resolvedStyle.width ? 1 : 0;
			float maxY = Viewport.resolvedStyle.height < Container.resolvedStyle.height ? 1 : 0;
			if (value.x < 0) { value.x = Mathf.Lerp(value.x, 0, Time.deltaTime * 10); }
			if (value.x > maxX) { value.x = Mathf.Lerp(value.x, maxX, Time.deltaTime * 10); }
			if (value.y < 0) { value.y = Mathf.Lerp(value.y, 0, Time.deltaTime * 10); }
			if (value.y > maxY) { value.y = Mathf.Lerp(value.y, maxY, Time.deltaTime * 10); }

			if (original != value) { UpdateValue(value); }

			if (!isDrag) { return; }
			Vector3 mousePosition = UITool.GetMousePosition();
			Vector3 differ = new Vector3(mousePosition.x, Screen.height - mousePosition.y) - pointerPosition;
			Vector3 offset = differ + originalPosition;
			float maxWidth = Viewport.resolvedStyle.width - Container.resolvedStyle.width;
			float maxHeight = Viewport.resolvedStyle.height - Container.resolvedStyle.height;

			float x = offset.x / maxWidth;
			float y = offset.y / maxHeight;
			UpdateValue(new Vector2(x, y));
		}
		/// <summary> 更新值(0-1) </summary>
		public void UpdateValue(Vector2 value, bool send = true) {
			if (direction == UIDirection.Horizontal) { value.y = 0; }
			if (direction == UIDirection.Vertical) { value.x = 0; }
			this.value = value;
			if (send) { ValueChanged?.Invoke(value); }
			float maxWidth = Viewport.resolvedStyle.width - Container.resolvedStyle.width;
			float maxHeight = Viewport.resolvedStyle.height - Container.resolvedStyle.height;
			float xPos = maxWidth * value.x;
			float yPos = maxHeight * value.y;
			Container.transform.position = new Vector3(xPos, yPos);

			if (horizontal.value != value.x) { horizontal.UpdateValue(value.x, false); }
			if (vertical.value != value.y) { vertical.UpdateValue(value.y, false); }
		}
	}
}