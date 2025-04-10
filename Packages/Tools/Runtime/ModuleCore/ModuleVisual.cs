using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	/// <summary>
	/// 数据可视化
	/// </summary>
	public class ModuleVisual {
		/// <summary> 创建可视化内容 </summary>
		public static void Create<Type>(ref Type value, Transform original, Transform parent) {
			if (value != null) { return; }
			Transform temp = CreateTransform(original, parent);
			value = temp.GetComponent<Type>();
		}
		/// <summary> 创建Transform </summary>
		public static Transform CreateTransform(Transform original, Transform parent) {
			Transform temp = Transform.Instantiate(original, parent);
			temp.gameObject.SetActive(true);
			return temp;
		}
		/// <summary> 删除可视化内容 </summary>
		public static void Remove<Type>(Type visual) where Type : Component {
			if (visual != null) { GameObject.Destroy(visual.gameObject); }
		}
	}
}