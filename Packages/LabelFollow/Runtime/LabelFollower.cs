using UnityEngine;

namespace MuHua
{
	public class LabelFollower : MonoBehaviour
	{
		public Transform target; // 要跟随的目标物体
		public Vector3 offset; // 标签的偏移量
		void Update()
		{
			if (target != null)
			{
				// 设置标签的位置
				transform.position = target.position + offset;

				// 使标签面向相机
				transform.rotation = Camera.main.transform.rotation;
			}
		}
	}
}