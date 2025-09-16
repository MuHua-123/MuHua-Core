using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// UI快捷菜单
/// </summary>
public class UIShortcutMenu : ModuleUISingle<UIShortcutMenu> {
	/// <summary> 菜单模板 </summary>
	public VisualTreeAsset menuTreeAsset;
	/// <summary> 项目模板 </summary>
	public VisualTreeAsset itemTreeAsset;

	/// <summary> 数据列表 </summary>
	public List<DataMenuItem> datas = new List<DataMenuItem>();
	/// <summary> 控件列表 </summary>
	public static List<UIControl> controls = new List<UIControl>();

	public override VisualElement Element => root.Q<VisualElement>("ShortcutMenu");

	protected override void Awake() => NoReplace(false);

	private void Update() => controls.ForEach(control => control.Update());

	private void OnDestroy() => controls.ForEach(control => control.Dispose());

	/// <summary> 打开菜单 </summary>
	public void Open() {
		Close();
		Vector3 position = UITool.GetMousePosition(Element);
		UIMenuPanel menuPanel = Create();
		menuPanel.Settings(position, datas);
	}
	/// <summary> 关闭菜单 </summary>
	public void Close() {
		controls.ForEach(control => control.Dispose());
		controls.Clear();
		Element.Clear();
	}
	/// <summary> 创建子菜单 </summary>
	public UIMenuPanel Create() {
		// 创建菜单元素
		VisualElement element = menuTreeAsset.Instantiate();
		element.EnableInClassList("menu", true);
		Element.Add(element);
		UIMenuPanel menuPanel = new UIMenuPanel(element, itemTreeAsset);
		controls.Add(menuPanel);
		return menuPanel;
	}

	/// <summary> 添加菜单项(方法) </summary> 
	public void Add(string name, Action callback) {
		string[] names = name.Split('/');

		List<DataMenuItem> datas = this.datas;
		for (int i = 0; i < names.Length; i++) {
			string menu = names[i];
			DataMenuItem item = datas.Find(obj => obj.name == menu);
			if (item == null) {
				item = new DataMenuItem { name = menu };
				if (i == names.Length - 1) { item.callback = callback; }
				datas.Add(item);
			}
			datas = item.items;
		}
	}
	/// <summary> 移除菜单项 </summary>
	public void Remove(string name) {
		string[] names = name.Split('/');
		List<DataMenuItem> datas = this.datas;
		DataMenuItem parent = null;
		DataMenuItem target = null;

		for (int i = 0; i < names.Length; i++) {
			string menu = names[i];
			target = datas.Find(obj => obj.name == menu);
			if (target == null) return; // 未找到，直接返回
			if (i == names.Length - 1) {
				// 找到要移除的项
				datas.Remove(target);
				return;
			}
			parent = target;
			datas = target.items;
		}
	}
}
/// <summary> 
/// UI快捷菜单面板
/// </summary>
public class UIMenuPanel : ModuleUIPanel, UIControl {

	public UIMenuPanel menuPanel;
	public VisualElement submenu;
	public ModuleUIItems<UIItem, DataMenuItem> items;

	public VisualElement Container => Q<VisualElement>("Container");

	public UIMenuPanel(VisualElement element, VisualTreeAsset templateAsset) : base(element) {
		items = new ModuleUIItems<UIItem, DataMenuItem>(Container, templateAsset,
		(data, element) => new UIItem(data, element, this));
	}
	public void Update() {
		// throw new NotImplementedException();
	}
	public void Dispose() {
		items.Release();
	}

	public void Settings(Vector3 position, List<DataMenuItem> datas) {
		items.Create(datas);
		element.transform.position = position;
	}

	/// <summary> 打开子菜单 </summary>
	public void Open(VisualElement submenu, List<DataMenuItem> datas) {
		if (this.submenu == submenu) { return; }
		// 更新子菜单
		this.submenu = submenu;
		if (menuPanel == null) { menuPanel = UIShortcutMenu.I.Create(); }
		float x = submenu.worldBound.position.x + submenu.resolvedStyle.width;
		float y = submenu.worldBound.position.y - 5;
		Vector3 position = new Vector3(x, y, 0);
		menuPanel.Settings(position, datas);
		bool isEnable = datas != null && datas.Count > 0;
		menuPanel.OpenSubmenu(isEnable);
	}
	public void OpenSubmenu(bool open) {
		element.EnableInClassList("menu-hide", !open);
		menuPanel?.OpenSubmenu(false);
	}

	/// <summary> UI项目 </summary>
	public class UIItem : ModuleUIItem<DataMenuItem> {
		public readonly UIMenuPanel parent;

		public Label Name => element.Q<Label>("Name");
		public VisualElement Arrow => element.Q<VisualElement>("Arrow");

		public UIItem(DataMenuItem value, VisualElement element, UIMenuPanel parent) : base(value, element) {
			this.parent = parent;
			Name.text = value.name;
			Arrow.EnableInClassList("menu-arrow-hide", value.items == null || value.items.Count == 0);
			element.RegisterCallback<MouseDownEvent>(MouseDownEvent);
			element.RegisterCallback<MouseMoveEvent>(MouseMoveEvent);
		}
		private void MouseDownEvent(MouseDownEvent evt) {
			value.callback?.Invoke();
			UIShortcutMenu.I.Close();
		}
		private void MouseMoveEvent(MouseMoveEvent evt) {
			parent.Open(element, value.items);
		}
	}
}
/// <summary>
/// 菜单项目
/// </summary>
public class DataMenuItem {
	/// <summary> 名称 </summary>
	public string name;
	/// <summary> 回调 </summary>
	public Action callback;
	/// <summary> 子菜单项 </summary>
	public List<DataMenuItem> items = new List<DataMenuItem>();
}