using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	/// <summary>
	/// 数据可视化
	/// </summary>
	public class ModuleVisual<T> : ModuleSingle<ModuleVisual<T>> where T : ModuleData<T> {
		/// <summary> 生成空间 </summary>
		public Transform space;
		/// <summary> 数据预制件 </summary>
		public Transform prefab;

		/// <summary> 替换旧的 </summary>
		protected override void Awake() => Replace();

		/// <summary> 更新可视化内容 </summary>
		public virtual void UpdateVisual(T data) {
			Create(ref data.visual, prefab, space);
			data.visual.UpdateVisual(data);
		}
		/// <summary> 释放可视化内容 </summary>
		public virtual void ReleaseVisual(T data) {
			if (data.visual != null) { Destroy(data.visual.gameObject); }
			data.visual = null;
		}
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
	}
}