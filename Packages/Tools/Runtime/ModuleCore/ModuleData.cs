using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	/// <summary>
	/// 数据
	/// </summary>
	public abstract class ModuleData<T> where T : ModuleData<T> {
		/// <summary> 可视化对象 </summary>
		public ModulePrefab<T> visual;
	}
}