using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// UI项容器
	/// </summary>
	public class ModuleUIItems<TItem, TData> : ModuleUIPanel where TItem : ModuleUIItem<TData> {
		public readonly VisualTreeAsset templateAsset;// 模板资源
		public readonly Func<TData, VisualElement, TItem> generate;// 生成UI项的函数

		public List<TItem> uiItems = new List<TItem>();// UI项列表

		public TItem this[int index] { get => uiItems[index]; }// 索引器

		/// <summary>  UI容器  </summary>
		public ModuleUIItems(VisualElement element, VisualTreeAsset templateAsset, Func<TData, VisualElement, TItem> generate) : base(element) {
			this.templateAsset = templateAsset;
			this.generate = generate;
		}
		/// <summary> 释放资源 </summary>
		public void Release() {
			element.Clear();
			uiItems.ForEach(obj => obj.Release());
			uiItems = new List<TItem>();
		}
		/// <summary> 创建UI项 </summary>
		public void Create(List<TData> datas) {
			Release();
			datas.ForEach(Create);
		}
		/// <summary> 创建UI项 </summary>
		public void Create(TData data) {
			VisualElement element = templateAsset.Instantiate();
			TItem item = generate(data, element);
			this.element.Add(item.element);
			uiItems.Add(item);
		}
		/// <summary> 遍历 </summary>
		public void ForEach(Action<TItem> action) {
			uiItems.ForEach(action);
		}
	}
}
