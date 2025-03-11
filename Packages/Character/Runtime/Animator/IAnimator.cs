using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	[RequireComponent(typeof(Animator))]
	public abstract class IAnimator : MonoBehaviour {
		/// <summary> 动画过渡 </summary>
		public abstract void Transition(string name);
		/// <summary> 设置参数 </summary>
		public abstract void SetFloat(string name, float value);
	}
}
