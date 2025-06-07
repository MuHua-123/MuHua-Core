using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	/// <summary>
	/// 标准角色 - 模块
	/// </summary>
	public class MCharacterStandard : MCharacter {

		/// <summary> 当前动作 </summary>
		public IKinesis currentKinesis;

		public MCharacterStandard(Animator animator, LayerMask ground) : base(animator) {
			movement = new MovementStandard(transform, ground);

			Transition(new KIdle());
		}

		public override void Update() {
			movement.Update();
			currentKinesis.UpdateKinesis();
		}
		public override bool Transition(IKinesis kinesis) {
			// 不可以转换
			if (currentKinesis != null && !currentKinesis.Transition(kinesis)) { return false; }
			// 进行转换
			currentKinesis?.FinishKinesis();
			currentKinesis = kinesis;
			currentKinesis?.StartKinesis();
			return true;
		}
		public override void AnimationExit() {
			currentKinesis.AnimationExit();
		}
	}
}
