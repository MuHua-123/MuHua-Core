using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	/// <summary>
	/// 标准运动实现
	/// </summary>
	public class MovementStandard : IMovement {
		public Vector3 position;
		public float moveSpeed = 5.0f; // 最大移动速度
		public float acceleration = 20.0f; // 加速度
		public float steeringSpeed = 180.0f; // 加速度
		public float currentSpeed = 0.0f; // 当前速度
		public Vector3 direction; // 面向
		public CharacterController controller;

		private float _targetRotation = 0.0f;
		private float _rotationVelocity;
		private float _verticalVelocity;

		public override float CurrentSpeed => currentSpeed;

		public override Vector3 Direction => direction;

		private void Awake() => position = transform.position;

		private void Update() {
			// 计算相对于世界坐标系的移动方向
			Vector3 moveDirection = (position - transform.position).normalized;
			float distance = Vector3.Distance(transform.position, position);

			// float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;
			float targetSpeed = moveSpeed;

			// 注意：Vector2的==运算符使用近似值，因此不易出现浮点误差，并且比幅度便宜
			// 如果没有输入，将目标速度设置为0
			if (distance < 0.2f) { targetSpeed = 0.0f; }

			// 角色当前水平速度的参考
			//float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			//float inputMagnitude = moveDirection.magnitude;

			// 加速或减速至目标速度
			if (currentSpeed < targetSpeed - speedOffset || currentSpeed > targetSpeed + speedOffset) {
				// 产生弯曲的结果，而不是线性的结果，从而产生更有机的速度变化
				// 注意Lerp中的T是夹紧的，所以我们不需要夹紧我们的速度
				currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * acceleration);

				// 四舍五入到小数点后3位
				//currentSpeed = Mathf.Round(currentSpeed * 1000f) / 1000f;
			}
			else { currentSpeed = targetSpeed; }

			// 注意：矢量2！=运算符使用近似值，因此不易出现浮点误差，而且比幅度便宜
			// 如果有移动输入，则在玩家移动时旋转玩家
			// if (moveDirection != Vector3.zero) {
			// 	_targetRotation = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
			// 	float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, steeringSpeed);

			// 	// 相对于相机位置旋转到面向输入方向
			// 	transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
			// }


			//Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

			// 移动角色
			// controller.Move(targetDirection.normalized * (currentSpeed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

			// 平滑加速和减速
			//currentSpeed = Mathf.MoveTowards(currentSpeed, moveSpeed, acceleration * Time.deltaTime);

			// 移动玩家
			//transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);

			controller.Move(moveDirection * currentSpeed * Time.deltaTime);

			// 如果有移动输入，则更新玩家的朝向
			if (distance > 0.1f) {
				Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, moveSpeed * Time.deltaTime * steeringSpeed);
			}

			// 计算转向向量
			Vector3 localMoveDirection = transform.InverseTransformDirection(moveDirection * currentSpeed);
			localMoveDirection = localMoveDirection.normalized;
			// 对localMoveDirection的x和z进行分类处理
			float moveX = Convert.ToInt32(localMoveDirection.x);
			float moveZ = Convert.ToInt32(localMoveDirection.z);
			direction = new Vector3(moveX, 0, moveZ);
		}

		public override bool UpdateMove(Vector3 position, float moveSpeed) {
			this.position = position;
			this.moveSpeed = moveSpeed;

			// 如果到达目标位置，返回 true
			float distance = Vector3.Distance(transform.position, position);

			return distance < 0.05f;
		}
		public override Vector3 RandomTargetPosition() {
			float randomX = UnityEngine.Random.Range(-10.0f, 10.0f);
			float randomZ = UnityEngine.Random.Range(-10.0f, 10.0f);
			return transform.position + new Vector3(randomX, 0, randomZ);
		}
		public override void StopMoving() {
			currentSpeed = 0;
		}
	}
}