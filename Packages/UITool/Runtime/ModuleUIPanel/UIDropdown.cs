using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// 下拉框
	/// </summary>
	public class UIDropdown<T> : ModuleUIPanel {
		/// <summary> 绑定的画布 </summary>
		internal readonly VisualElement canvas;
		/// <summary> 下拉框容器 </summary>
		internal readonly VisualElement DropdownContainer;
		/// <summary> 下拉框滚动视图 </summary>
		internal readonly VisualElement DropdownScrollView;
		/// <summary> 选项模板 </summary>
		internal readonly VisualTreeAsset TemplateAsset;
		/// <summary> 值改变时 </summary>
		public event Action<T> ValueChanged;

		public T value;
		public List<T> list = new List<T>();

		internal UIScrollView scrollView;
		internal ModuleUIItems<UIDropdownItem, T> DropdownItems;

		/// <summary> 数据操作 </summary
		public T this[int index] => list[index];
		/// <summary> 总数 </summary
		public int Count => list.Count;

		internal Label Tag => Q<Label>("Tag");
		internal Label Title => Q<Label>("Title");
		internal VisualElement Input => Q<VisualElement>("Input");
		internal VisualElement Icon => Q<VisualElement>("Icon");
		internal VisualElement Positioner => Q<VisualElement>("Positioner");

		public UIDropdown(VisualElement element, VisualElement canvas, VisualTreeAsset TemplateAsset) : base(element) {
			this.canvas = canvas;
			this.TemplateAsset = TemplateAsset;

			DropdownContainer = new VisualElement();
			DropdownContainer.EnableInClassList("dropdown-container", true);
			DropdownContainer.EnableInClassList("dropdown-hide", true);
			canvas.Add(DropdownContainer);

			DropdownScrollView = Q<VisualElement>("DropdownScrollView");
			DropdownScrollView.EnableInClassList("dropdown-hide", false);
			DropdownContainer.Add(DropdownScrollView);

			scrollView = new UIScrollView(DropdownScrollView, DropdownContainer, UIDirection.FromTopToBottom);
			DropdownItems = new ModuleUIItems<UIDropdownItem, T>(scrollView.Container, TemplateAsset,
			(data, element) => new UIDropdownItem(data, element, this));

			Input.RegisterCallback<ClickEvent>(evt => OpenDropdown());
			DropdownContainer.RegisterCallback<PointerDownEvent>(evt => CloseDropdown());
		}
		public virtual void Release() {
			canvas.Remove(DropdownContainer);
			DropdownItems.Release();
		}
		public virtual void Update() {
			scrollView.Update();
		}

		/// <summary> 打开下拉框 </summary>
		public void OpenDropdown() {
			float width = Positioner.resolvedStyle.width;
			Vector2 position = Positioner.worldBound.position;

			DropdownScrollView.style.width = width;
			DropdownScrollView.style.left = position.x;
			DropdownScrollView.style.top = position.y;
			DropdownContainer.EnableInClassList("dropdown-hide", false);

			DropdownItems.Create(list);
		}
		/// <summary> 关闭下拉框 </summary>
		public void CloseDropdown() {
			DropdownContainer.EnableInClassList("dropdown-hide", true);
		}

		/// <summary> 更新值 </summary>
		public void UpdateValue(T value, bool send = true) {
			this.value = value;
			Tag.text = value.ToString();
			if (send) { ValueChanged?.Invoke(value); }
		}
		/// <summary> 设置值 </summary>
		public void SetValue(List<T> list) {
			this.list = list;
			if (list.Count > 0) { UpdateValue(list[0], false); }
		}

		#region UI项定义
		/// <summary>
		/// 设置标题 UI项
		/// </summary>
		internal class UIDropdownItem : ModuleUIItem<T> {
			public readonly UIDropdown<T> parent;

			public Button Button => Q<Button>();
			public VisualElement Check => Q<VisualElement>("Check");

			public UIDropdownItem(T value, VisualElement element, UIDropdown<T> parent) : base(value, element) {
				this.parent = parent;
				Button.text = value.ToString();
				Button.clicked += Select;
				Check.EnableInClassList("dropdown-hide", !value.Equals(parent.value));
			}
			public override void DefaultState() {
				Check.EnableInClassList("dropdown-hide", true);
			}
			public override void SelectState() {
				parent.UpdateValue(value);
				parent.CloseDropdown();
				Check.EnableInClassList("dropdown-hide", false);
			}
		}
		#endregion
	}
}