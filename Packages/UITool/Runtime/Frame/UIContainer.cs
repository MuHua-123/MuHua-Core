using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// UI容器
	/// </summary>
	public class UIContainer<Item, Data> where Item : UIItem<Data> {
		public readonly VisualElement container;
		public readonly VisualTreeAsset templateAsset;
		public readonly Func<Data, VisualElement, Item> generate;
		public List<Item> uiItems = new List<Item>();
		/// <summary>  UI容器  </summary>
		public UIContainer(VisualElement container, VisualTreeAsset templateAsset, Func<Data, VisualElement, Item> generate) {
			this.container = container;
			this.templateAsset = templateAsset;
			this.generate = generate;
		}
		/// <summary> 释放资源 </summary>
		public void Release() {
			container.Clear();
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
			container.Add(item.element);
			uiItems.Add(item);
		}
	}
}