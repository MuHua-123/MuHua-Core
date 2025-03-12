using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// 页面模块
	/// </summary>
	public abstract class ModuleUIPage : MonoBehaviour {
		/// <summary> 绑定文档 </summary>
		public UIDocument document;
		/// <summary> 根目录文档 </summary>
		public VisualElement root => document.rootVisualElement;
		/// <summary> 绑定文档 </summary>
		public abstract VisualElement Element { get; }
		/// <summary> 添加UI元素 </summary>
		public void Add(VisualElement child) => Element.Add(child);
		/// <summary> 查询UI元素 </summary>
		public T Q<T>(string name = null, string className = null) where T : VisualElement => Element.Q<T>(name, className);
	}
}