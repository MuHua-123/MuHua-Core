using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// 模组列表 - UI
/// </summary>
public class UIModuleList : ModuleUIPanel, UIControl {

	// public Action<ModuleData> callback;

	public UIScrollViewListV<UIItem, ModuleData> Items;

	public VisualElement ScrollView => Q<VisualElement>("ScrollView");

	public UIModuleList(VisualElement element, VisualElement canvas, VisualTreeAsset templateAsset) : base(element) {
		// this.callback = callback;
		ModuleUI.AddControl(this);
		Items = new UIScrollViewListV<UIItem, ModuleData>(ScrollView, canvas, templateAsset,
		(data, element) => new UIItem(data, element, this));
	}
	public void Update() => Items.Update();

	public void Dispose() => Items.Dispose();

	public void Initial() => Items.Create(ModuleSystem.I.modules);

	// public void Settings(ModuleData data) => callback?.Invoke(data);

	/// <summary> 模组 UI项 </summary>
	public class UIItem : ModuleUIItem<ModuleData> {
		public readonly UIModuleList parent;

		public Label Title => element.Q<Label>("Title");
		public VisualElement Toggle => element.Q<VisualElement>("Toggle");
		public VisualElement Check => Toggle.Q<VisualElement>("Check");

		public UIItem(ModuleData value, VisualElement element, UIModuleList parent) : base(value, element) {
			this.parent = parent;
			Title.text = value.name;
			Check.EnableInClassList("ml-template-hide", !value.isEnable);
			Toggle.RegisterCallback<ClickEvent>(EnableAndDisable);
		}
		private void EnableAndDisable(ClickEvent evt) {
			value.isEnable = !value.isEnable;
			Check.EnableInClassList("ml-template-hide", !value.isEnable);
			if (value.isEnable) { ModuleSystem.I.LoadModule(value); }
			else { ModuleSystem.I.UnloadModule(value); }
		}
	}
}
