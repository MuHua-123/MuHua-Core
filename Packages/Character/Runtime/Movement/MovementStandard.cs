using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
    /// <summary>
    /// 标准运动实现
    /// </summary>
    public class MovementStandard : Movement {
        public override bool UpdateMove(Vector3 position) {
            // 计算相对于世界坐标系的移动方向
            Vector3 moveDirection = (position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, position);

            // 平滑加速和减速
            currentSpeed = distance > 0.2f
                ? Mathf.MoveTowards(currentSpeed, moveSpeed, acceleration * Time.deltaTime)
                : Mathf.MoveTowards(currentSpeed, 0, acceleration * Time.deltaTime);

            // 移动玩家
            transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);

            // 如果有移动输入，则更新玩家的朝向
            if (distance != 0) {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, moveSpeed * Time.deltaTime * 100);
            }

            // 计算转向向量
            Vector3 localMoveDirection = transform.InverseTransformDirection(moveDirection * currentSpeed);
            localMoveDirection = localMoveDirection.normalized;
            // 对localMoveDirection的x和z进行分类处理
            float moveX = Convert.ToInt32(localMoveDirection.x);
            float moveZ = Convert.ToInt32(localMoveDirection.z);
            front = new Vector3(moveX, 0, moveZ);

            // 如果到达目标位置，返回 true
            distance = Vector3.Distance(transform.position, position);

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