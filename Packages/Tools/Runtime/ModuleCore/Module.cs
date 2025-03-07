using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	/// <summary>
	/// 单例基类
	/// </summary>
	public class Module<T> where T : Module<T>, new() {
		/// <summary> 模块单例 </summary>
		public static T I => Instantiate();

		protected static T instance;
		protected static T Instantiate() => instance == null ? instance = new T() : instance;
	}
}