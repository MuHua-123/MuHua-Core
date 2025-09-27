using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// 场景库 - UI
/// </summary>
public class UISceneLibrary : ModuleUIPanel, UIControl {

	public Action<SceneData> callback;

	public UIScrollViewListV<UIItem, SceneData> Items;

	public UISceneLibrary(VisualElement element, VisualElement canvas, VisualTreeAsset templateAsset, Action<SceneData> callback) : base(element) {
		this.callback = callback;
		ModuleUI.AddControl(this);
		Items = new UIScrollViewListV<UIItem, SceneData>(element, canvas, templateAsset,
		(data, element) => new UIItem(data, element, this));
	}

	public void Update() {
		Items.Update();
		Items.ForEach(obj => obj.Update());
	}

	public void Dispose() => Items.Dispose();

	public void Initial() => Items.Create(SceneSystem.I.scenes);

	public void Settings(SceneData data) => callback?.Invoke(data);

	/// <summary> UI项 </summary>
	public class UIItem : ModuleUIItem<SceneData> {
		private UISceneLibrary parent;

		private float time;

		public Label Title => Q<Label>("Title");
		public VisualElement Frame => Q<VisualElement>("Frame");
		public VisualElement Image => Q<VisualElement>("Image");

		public UIItem(SceneData value, VisualElement element, UISceneLibrary parent) : base(value, element) {
			this.parent = parent;
			Title.text = value.name;
			Image.RegisterCallback<ClickEvent>(evt => Select());
		}
		public override void DefaultState() {
			Frame.EnableInClassList("scene-frame-s", false);
			Image.EnableInClassList("scene-image-s", false);
		}
		public override void SelectState() {
			time = 0.1f;
			Frame.EnableInClassList("scene-frame-s", true);
			Image.EnableInClassList("scene-image-s", true);
			parent.Settings(value);
		}
		public void Update() {
			time -= Time.deltaTime;
			Frame.EnableInClassList("scene-click", time >= 0);
			Image.EnableInClassList("scene-click", time >= 0);
		}
	}
}
