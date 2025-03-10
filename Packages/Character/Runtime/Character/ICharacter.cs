using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	public abstract class ICharacter : MonoBehaviour {
		public Ikinesis kinesis;

		public void Update() => kinesis.Update();

		/// <summary> 更新动作 </summary>
		public abstract void Updatekinesis(Ikinesis kinesis);

		/// <summary> 触发动画特效 </summary>
		public virtual void AnimationEffects() => kinesis.AnimationEffects();
		/// <summary> 动画结束 </summary>
		public virtual void AnimationEnd() => kinesis.AnimationEnd();
		/// <summary> 动画退出 </summary>
		public virtual void AnimationExit() => kinesis.AnimationExit();
	}
}