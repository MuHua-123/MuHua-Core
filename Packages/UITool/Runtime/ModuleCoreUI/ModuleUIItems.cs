using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// UI项容器
	/// </summary>
	public class ModuleUIItems<T, Data> : ModuleUIPanel where T : ModuleUIItem<Data> {
		public readonly VisualTreeAsset templateAsset;
		public readonly Func<Data, VisualElement, T> generate;

		public List<T> uiItems = new List<T>();

		/// <summary> 数据操作 </summary
		public virtual T this[int index] => uiItems[index];

		/// <summary>  UI容器  </summary>
		public ModuleUIItems(VisualElement element, VisualTreeAsset templateAsset, Func<Data, VisualElement, T> generate) : base(element) {
			this.templateAsset = templateAsset;
			this.generate = generate;
		}
		/// <summary> 释放资源 </summary>
		public void Release() {
			element.Clear();
			uiItems.ForEach(obj => obj.Release());
			uiItems = new List<T>();
		}
		/// <summary> 创建UI项 </summary>
		public void Create(List<Data> datas) {
			Release();
			datas.ForEach(Create);
		}
		/// <summary> 创建UI项 </summary>
		public void Create(Data data) {
			VisualElement element = templateAsset.Instantiate();
			T item = generate(data, element);
			this.element.Add(item.element);
			uiItems.Add(item);
		}
	}
}