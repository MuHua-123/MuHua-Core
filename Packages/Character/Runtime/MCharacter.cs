using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	/// <summary>
	/// 角色 - 模块
	/// </summary>
	public abstract class MCharacter {

		/// <summary> 变换器 </summary>
		public Transform transform;
		/// <summary> 动画器 </summary>
		public Animator animator;
		/// <summary> 运动器 </summary>
		public Movement movement;

		public MCharacter(Animator animator) => this.animator = animator;

		/// <summary> 更新 </summary>
		public abstract void Update();
		/// <summary> 动作过渡 </summary>
		public abstract bool Transition(IKinesis kinesis);
		/// <summary> 动画结束 </summary>
		public abstract void AnimationExit();
	}
}