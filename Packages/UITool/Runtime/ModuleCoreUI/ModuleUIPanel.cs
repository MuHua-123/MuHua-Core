using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// UI控件
	/// </summary>
	public class ModuleUIPanel  {
		/// <summary> 绑定的元素 </summary>
		public readonly VisualElement element;
		/// <summary> UI控件 </summary>
		public ModuleUIPanel(VisualElement element) => this.element = element;
	}
}