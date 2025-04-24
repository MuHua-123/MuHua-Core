using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// 滚动列表
	/// </summary>
	public class UIScrollList<T, Data> : UIScrollView where T : ModuleUIItem<Data> {

		private ModuleUIItems<T, Data> Items;// UI项容器

		public UIScrollList(VisualElement element, VisualElement canvas, VisualTreeAsset templateAsset, Func<Data, VisualElement, T> generate,
		UIDirection direction = UIDirection.HorizontalAndVertical,
		UIDirection sh = UIDirection.FromLeftToRight,
		UIDirection sv = UIDirection.FromTopToBottom) : base(element, canvas, direction, sh, sv) {
			Items = new ModuleUIItems<T, Data>(Container, templateAsset, generate);
		}
		/// <summary> 释放资源 </summary>
		public void Release() => Items.Release();
		/// <summary> 创建UI项 </summary>
		public void Create(List<Data> datas) => Items.Create(datas);
	}
}
