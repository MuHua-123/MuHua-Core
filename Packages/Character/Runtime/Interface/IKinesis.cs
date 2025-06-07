using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	/// <summary>
	/// 运动
	/// </summary>
	public abstract class IKinesis {
		/// <summary> 动作过渡 </summary>
		public abstract bool Transition(IKinesis kinesis);
		/// <summary> 开始动作 </summary>
		public abstract void StartKinesis();
		/// <summary> 更新动作 </summary>
		public abstract void UpdateKinesis();
		/// <summary> 完成动作 </summary>
		public abstract void FinishKinesis();
		/// <summary> 动画结束 </summary>
		public abstract void AnimationExit();
	}
}