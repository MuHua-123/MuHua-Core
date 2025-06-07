using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	/// <summary>
	/// 移动 - 运动
	/// </summary>
	public class KMove : IKinesis {
		/// <summary> 基础角色 </summary>
		public readonly MCharacter character;

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

		public KMove(MCharacter character, Vector2 moveDirection, bool isRotation) {
			this.character = character;
			this.moveDirection = moveDirection;
			this.isRotation = isRotation;
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
			return true;
		}
		public override void StartKinesis() {
			movement.Move(moveDirection, moveSpeed, acceleration, isRotation);
			if (!isInitial) { return; }
			transform.position = position;
			transform.eulerAngles = eulerAngles;
		}
		public override void UpdateKinesis() {
			// 更新动画器
			animator.SetFloat("MoveSpeed", movement.Speed);
			// 移动结束
			if (movement.Speed == 0) { character.Transition(new KIdle()); }
		}
		public override void FinishKinesis() {
			// throw new System.NotImplementedException();
		}
		public override void AnimationExit() {
			// throw new System.NotImplementedException();
		}
	}
}