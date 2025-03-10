using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	/// <summary>
	/// 运动控制器
	/// </summary>
	public abstract class Movement : MonoBehaviour {
		/// <summary> 当前速度 </summary>
		public abstract float CurrentSpeed { get; }
		/// <summary> 当前方向 </summary>
		public abstract Vector3 Direction { get; }

		/// <summary> 更新移动 </summary>
		public abstract bool UpdateMove(Vector3 position);
		/// <summary> 获取随机位置 </summary>
		public abstract Vector3 RandomTargetPosition();
		/// <summary> 停止移动 </summary>
		public abstract void StopMoving();
	}
}