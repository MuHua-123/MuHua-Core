using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	/// <summary>
	/// 碰撞角色 - 模块
	/// </summary>
	public class MCharacterCollision : MCharacter {

		/// <summary> 当前动作 </summary>
		public IKinesis currentKinesis;

		public MCharacterCollision(Animator animator, CharacterController controller, LayerMask ground) : base(animator) {
			movement = new MovementCollision(controller, ground);

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