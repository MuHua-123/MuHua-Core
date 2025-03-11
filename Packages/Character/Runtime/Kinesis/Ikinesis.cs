using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	/// <summary>
	/// 角色动作
	/// </summary>
	public abstract class Ikinesis {
		/// <summary> 持续更新 </summary>
		public virtual void Update() { }

		/// <summary> 动作过渡 </summary>
		public abstract bool Transition(Ikinesis kinesis);

		public virtual void Startkinesis() { }

		/// <summary> 触发动画特效 </summary>
		public virtual void AnimationEffects() { }
		/// <summary> 动画结束 </summary>
		public virtual void AnimationEnd() { }
		/// <summary> 动画退出 </summary>
		public virtual void AnimationExit() { }
	}
}
