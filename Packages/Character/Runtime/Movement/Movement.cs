using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
    /// <summary>
    /// 运动控制器
    /// </summary>
    public abstract class Movement : MonoBehaviour {
        public float moveSpeed = 5.0f; // 最大移动速度
        public float acceleration = 20.0f; // 加速度
        public float currentSpeed = 0.0f; // 当前速度
        public Vector3 front; // 面向

        public abstract bool UpdateMove(Vector3 position);
        public abstract Vector3 RandomTargetPosition();
        public abstract void StopMoving();
    }
}