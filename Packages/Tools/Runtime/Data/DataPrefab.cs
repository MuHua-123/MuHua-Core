using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	/// <summary>
	/// 数据预制件
	/// </summary>
	public abstract class DataPrefab<T> : MonoBehaviour where T : Data<T> {
		/// <summary> 关联的数据 </summary>
		protected T value;

		/// <summary> 关联的数据 </summary>
		public virtual T Value => value;
		/// <summary> 更新可视化内容 </summary>
		public virtual void UpdateVisual(T value) => this.value = value;
	}
}
