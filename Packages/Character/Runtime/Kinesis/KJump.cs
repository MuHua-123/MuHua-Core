using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	/// <summary>
	/// 跳跃 - 运动
	/// </summary>
	public class KJump : IKinesis {
		/// <summary> 基础角色 </summary>
		public readonly MCharacter character;

		/// <summary> 结束跳跃 </summary>
		public bool isEndJump;
		/// <summary> 是否接地 </summary>
		public bool isGrounded;
		/// <summary> 允许转换 </summary>
		public bool isTransition;
		/// <summary> 跳跃高度 </summary>
		public float jumpHeight;
		/// <summary> 衰退速度 </summary>
		public float decaySpeed;
		/// <summary> 移动速度 </summary>
		public float moveSpeed = 2;
		/// <summary> 加速度 </summary>
		public float acceleration = 15;
		/// <summary> 移动方向 </summary>
		public Vector2 moveDirection;
		/// <summary> 初始位置 </summary>
		public Vector3 position;
		/// <summary> 初始角度 </summary>
		public Vector3 eulerAngles;
		/// <summary> 是否旋转 </summary>
		public bool isRotation;
		/// <summary> 初始设置 </summary>
		public bool isInitial = false;

		/// <summary> 变换器 </summary>
		public Transform transform => character.transform;
		/// <summary> 动画器 </summary>
		public Animator animator => character.animator;
		/// <summary> 运动器 </summary>
		public Movement movement => character.movement;

		public KJump(MCharacter character, Vector2 moveDirection, float jumpHeight, bool isRotation) {
			this.character = character;
			this.moveDirection = moveDirection;
			this.jumpHeight = jumpHeight;
		}

		public void Settings(float moveSpeed, float acceleration) {
			this.moveSpeed = moveSpeed;
			this.acceleration = acceleration;
		}
		public void Settings(Vector3 position, Vector3 eulerAngles) {
			this.position = position;
			this.eulerAngles = eulerAngles;
			isInitial = true;
		}

		public override bool Transition(IKinesis kinesis) {
			return isTransition;
		}
		public override void StartKinesis() {
			if (isInitial) {
				character.transform.position = position;
				character.transform.eulerAngles = eulerAngles;
			}
			isEndJump = false;
			isTransition = false;
			isGrounded = movement.Grounded;
			decaySpeed = movement.Speed;
			movement.Jump(jumpHeight);
			animator.SetTrigger("JumpStart");
		}
		public override void UpdateKinesis() {
			if (isEndJump) { return; }
			// 衰退速度
			decaySpeed = Mathf.Lerp(decaySpeed, 0, Time.deltaTime * 0.8f);
			movement.Move(moveDirection, decaySpeed, acceleration, isRotation);
			// 跳跃状态判断
			if (isGrounded == movement.Grounded) { return; }
			isGrounded = movement.Grounded;
			// 起跳
			if (!isGrounded) { return; }
			// 落地
			isEndJump = true;
			animator.SetTrigger("JumpLand");
			movement.Move(Vector2.zero, decaySpeed, acceleration, isRotation);
		}
		public override void FinishKinesis() {
			// throw new System.NotImplementedException();
		}
		public override void AnimationExit() {
			isTransition = true;
			// 转换到移动
			KMove kMove = new KMove(character, moveDirection, isRotation);
			kMove.Settings(moveSpeed, acceleration);
			character.Transition(kMove);
		}
	}
}
