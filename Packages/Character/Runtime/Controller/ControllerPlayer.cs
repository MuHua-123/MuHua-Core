using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MuHua {
	public class ControllerPlayer : MonoBehaviour {
		public Transform cameraController; // 相机对象
		public CharacterPlayer character;

		private Vector2 moveInput;

		private void Update() {
			if (moveInput == Vector2.zero) { return; }
			// 获取相机的前向和右向
			Vector3 cameraForward = cameraController.transform.forward;
			Vector3 cameraRight = cameraController.transform.right;

			// 忽略相机的y轴
			cameraForward.y = 0;
			cameraRight.y = 0;

			// 归一化向量
			cameraForward.Normalize();
			cameraRight.Normalize();

			// 计算相对于相机的移动方向
			Vector3 moveDirection = (cameraForward * moveInput.y + cameraRight * moveInput.x).normalized;

			// 相对于玩家的移动方向
			Vector3 position = character.transform.position + new Vector3(moveDirection.x, 0, moveDirection.z) * 0.5f;
			KinesisMove kinesis = new KinesisMove(character, position);
			character.Transitionkinesis(kinesis);
		}

		#region 输入系统
		public void OnMove(InputValue inputValue) {
			// 获取移动输入
			moveInput = inputValue.Get<Vector2>();
		}
		#endregion
	}
}