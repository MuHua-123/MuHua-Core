using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
    public abstract class Character : MonoBehaviour {
        /// <summary> 更新移动 </summary>
        public abstract bool UpdateMove(Vector3 position);

        /// <summary> 动画触发 </summary>
        public abstract void AnimationTrigger(string value);
        /// <summary> 动画结束 </summary>
        public abstract void AnimationEnd();
    }
}