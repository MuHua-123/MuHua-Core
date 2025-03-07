using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// UI项容器
	/// </summary>
	public class ModuleUIItems<Item, Data> : ModuleUIPanel where Item : ModuleUIItem<Data> {
		public readonly VisualTreeAsset templateAsset;
		public readonly Func<Data, VisualElement, Item> generate;
		public List<Item> uiItems = new List<Item>();
		/// <summary>  UI容器  </summary>
		public ModuleUIItems(VisualElement element, VisualTreeAsset templateAsset, Func<Data, VisualElement, Item> generate) : base(element) {
			this.templateAsset = templateAsset;
			this.generate = generate;
		}
		/// <summary> 释放资源 </summary>
		public void Release() {
			element.Clear();
			uiItems.ForEach(obj => obj.Release());
			uiItems = new List<Item>();
		}
		/// <summary> 创建UI项 </summary>
		public void Create(List<Data> datas) {
			Release();
			datas.ForEach(Create);
		}
		/// <summary> 创建UI项 </summary>
		public void Create(Data data) {
			VisualElement element = templateAsset.Instantiate();
			Item item = generate(data, element);
			this.element.Add(item.element);
			uiItems.Add(item);
		}
	}
}