using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// UI快捷菜单
/// </summary>
public class UIShortcutMenu : ModuleUIPanel, UIControl {
	/// <summary> 菜单模板 </summary>
	public VisualTreeAsset menuTreeAsset;
	/// <summary> 项目模板 </summary>
	public VisualTreeAsset itemTreeAsset;

	/// <summary> 数据列表 </summary>
	public List<DataMenuItem> datas = new List<DataMenuItem>();
	/// <summary> 控件列表 </summary>
	public static List<UIControl> controls = new List<UIControl>();

	public UIShortcutMenu(VisualElement element, VisualTreeAsset menuTreeAsset, VisualTreeAsset itemTreeAsset) : base(element) {
		this.menuTreeAsset = menuTreeAsset;
		this.itemTreeAsset = itemTreeAsset;
		ModuleUI.AddControl(this);
	}

	public void Update() => controls.ForEach(control => control.Update());

	public void Dispose() => controls.ForEach(control => control.Dispose());

	/// <summary> 打开菜单 </summary>
	public void Open() {
		Close();
		Vector3 position = UITool.GetMousePosition(element);
		UIMenuPanel menuPanel = Create();
		menuPanel.Settings(position, datas);
	}
	/// <summary> 关闭菜单 </summary>
	public void Close() {
		controls.ForEach(control => control.Dispose());
		controls.Clear();
		element.Clear();
	}
	/// <summary> 创建子菜单 </summary>
	public UIMenuPanel Create() {
		// 创建菜单元素
		VisualElement menu = menuTreeAsset.Instantiate();
		menu.EnableInClassList("menu", true);
		element.Add(menu);
		UIMenuPanel menuPanel = new UIMenuPanel(menu, itemTreeAsset, this);
		controls.Add(menuPanel);
		return menuPanel;
	}

	/// <summary> 添加菜单项(方法) </summary> 
	public void Add(string name, Action callback) {
		string[] names = name.Split('/');

		List<DataMenuItem> datas = this.datas;
		for (int i = 0; i < names.Length; i++) {
			DataMenuItem item = Find(names[i], datas);
			datas = item.items;
			if (i == names.Length - 1) { item.callback = callback; }
		}
	}
	/// <summary> 移除菜单项 </summary>
	public void Remove(string name) {
		string[] names = name.Split('/');
		List<DataMenuItem> datas = this.datas;

		for (int i = 0; i < names.Length; i++) {
			DataMenuItem item = Find(names[i], datas, false);
			// 未找到，直接返回
			if (item == null) { return; }
			// 找到要移除的项
			if (i == names.Length - 1) { datas.Remove(item); }
			datas = item.items;
		}
	}
	/// <summary> 查询菜单项 </summary>
	public DataMenuItem Find(string name, List<DataMenuItem> datas, bool isCreate = true) {
		DataMenuItem item = datas.Find(obj => obj.name == name);
		if (item != null || !isCreate) { return item; }
		item = new DataMenuItem { name = name };
		datas.Add(item);
		return item;
	}
}
/// <summary> 
/// UI快捷菜单面板
/// </summary>
public class UIMenuPanel : ModuleUIPanel, UIControl {
	public readonly UIShortcutMenu parent;
	public UIMenuPanel menuPanel;
	public VisualElement submenu;
	public ModuleUIItems<UIItem, DataMenuItem> items;

	public VisualElement Container => Q<VisualElement>("Container");

	public UIMenuPanel(VisualElement element, VisualTreeAsset templateAsset, UIShortcutMenu parent) : base(element) {
		this.parent = parent;
		items = new ModuleUIItems<UIItem, DataMenuItem>(Container, templateAsset,
		(data, element) => new UIItem(data, element, this, parent));
	}

	public void Update() { }

	public void Dispose() => items.Release();

	public void Settings(Vector3 position, List<DataMenuItem> datas) {
		items.Create(datas);
		element.transform.position = position;
	}

	/// <summary> 打开子菜单 </summary>
	public void Open(VisualElement submenu, List<DataMenuItem> datas) {
		if (this.submenu == submenu) { return; }
		// 更新子菜单
		this.submenu = submenu;
		if (menuPanel == null) { menuPanel = parent.Create(); }
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
		public readonly UIShortcutMenu shortcutMenu;

		public Label Name => element.Q<Label>("Name");
		public VisualElement Arrow => element.Q<VisualElement>("Arrow");

		public UIItem(DataMenuItem value, VisualElement element, UIMenuPanel parent, UIShortcutMenu shortcutMenu) : base(value, element) {
			this.parent = parent;
			this.shortcutMenu = shortcutMenu;

			Name.text = value.name;
			Arrow.EnableInClassList("menu-arrow-hide", value.items == null || value.items.Count == 0);
			element.RegisterCallback<MouseDownEvent>(MouseDownEvent);
			element.RegisterCallback<MouseMoveEvent>(MouseMoveEvent);
		}
		private void MouseDownEvent(MouseDownEvent evt) {
			value.callback?.Invoke();
			shortcutMenu.Close();
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