using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	public abstract class ICharacter : MonoBehaviour {
		public Ikinesis currentKinesis;

		public void Update() => currentKinesis.Update();

		/// <summary> 过渡动作 </summary>
		public abstract void Transitionkinesis(Ikinesis kinesis);

		/// <summary> 触发动画特效 </summary>
		public virtual void AnimationEffects() => currentKinesis.AnimationEffects();
		/// <summary> 动画结束 </summary>
		public virtual void AnimationEnd() => currentKinesis.AnimationEnd();
		/// <summary> 动画退出 </summary>
		public virtual void AnimationExit() => currentKinesis.AnimationExit();
	}
}