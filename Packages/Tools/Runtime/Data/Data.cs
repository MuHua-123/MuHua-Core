using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	/// <summary>
	/// 数据
	/// </summary>
	public abstract class Data<T> where T : Data<T> {
		/// <summary> 可视化对象 </summary>
		public DataPrefab<T> visual;
	}
}