using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
    public abstract class Character : MonoBehaviour {

        public Movement movement; // 运动控制器
        public Animator animator; // 动画控制器

        /// <summary> 更新移动 </summary>
        public abstract bool UpdateMove(Vector3 position);

        /// <summary> 动画触发 </summary>
        public abstract void AnimationTrigger(string value);
        /// <summary> 动画结束 </summary>
        public abstract void AnimationEnd();
    }
}