using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	/// <summary>
	/// 标准 - 运动器
	/// </summary>
	public class MovementStandard : Movement {
		/// <summary> 地面图层 </summary>
		public readonly LayerMask groundLayers;
		/// <summary> 变换 </summary>
		public readonly Transform transform;

		public float moveSpeed;// 移动速度
		public float currentSpeed;// 当前速度
		public float acceleration;// 加速度
		public float animationBlend;// 动画混合速度
		public Vector2 moveDirection;// 移动方向

		public bool isRotation;// 是否旋转
		public float targetRotation;// 目标旋转
		public float rotationVelocity;// 旋转速度
		public float rotationSmoothTime = 0.12f;// 旋转平滑 Range(0.0f, 0.3f)

		public bool grounded = true;// 是否接地
		public float verticalVelocity;// 垂直速度
		public float groundedRadius = 0.14f;// 地面检测半径

		/// <summary> 垂直重力 </summary>
		public float Gravity => Physics.gravity.y;

		public override float Speed => currentSpeed;

		public override bool Grounded => grounded;

		public MovementStandard(Transform transform, LayerMask groundLayers) {
			this.transform = transform;
			this.groundLayers = groundLayers;
		}

		/// <summary> 移动 </summary>
		public override void Move(Vector2 moveDirection, float moveSpeed, float acceleration, bool isRotation) {
			this.moveSpeed = moveSpeed;
			this.acceleration = acceleration;
			this.moveDirection = moveDirection;
			this.isRotation = isRotation;
		}
		/// <summary> 跳跃 </summary>
		public override void Jump(float jumpHeight) {
			verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * Gravity);
		}
		/// <summary> 更新 </summary>
		public override void Update() {
			// 如果没有输入，将目标速度设置为0
			if (moveDirection == Vector2.zero) moveSpeed = 0.0f;

			// 当前速度
			currentSpeed = Mathf.Lerp(currentSpeed, moveSpeed, Time.deltaTime * acceleration);

			// 四舍五入到小数点后3位
			currentSpeed = Mathf.Round(currentSpeed * 1000f) / 1000f;

			animationBlend = Mathf.Lerp(animationBlend, moveSpeed, Time.deltaTime * acceleration);
			if (animationBlend < 0.01f) animationBlend = 0f;

			// 使输入方向标准化
			Vector3 inputDirection = new Vector3(moveDirection.x, 0.0f, moveDirection.y).normalized;

			// 如果有移动输入，则在玩家移动时旋转玩家
			if (moveDirection != Vector2.zero && isRotation) {
				targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
				float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothTime);

				// 相对于相机位置旋转到面向输入方向
				transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
			}

			// 移动
			Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
			Vector3 horizontal = targetDirection.normalized * (currentSpeed * Time.deltaTime);
			Vector3 vertical = new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime;
			transform.position += horizontal + vertical;

			// 地面检测
			Vector3 position = transform.position;
			Vector3 rayOrigin = new Vector3(position.x, position.y + groundedRadius, position.z);
			// 射线长度稍微大于检测半径
			float rayLength = groundedRadius * 2 + 0.1f;
			// 使用射线检测地面
			grounded = Physics.Raycast(rayOrigin, Vector3.down, rayLength, groundLayers, QueryTriggerInteraction.Ignore);
			// 可选：调试显示射线
			Debug.DrawRay(rayOrigin, Vector3.down * rayLength, grounded ? Color.green : Color.red);

			// 引力
			verticalVelocity += Gravity * Time.deltaTime;
			// 站在地面上时，限制最大下落速度
			if (grounded && verticalVelocity < 0.0f) { verticalVelocity = -2f; }
		}
	}
}