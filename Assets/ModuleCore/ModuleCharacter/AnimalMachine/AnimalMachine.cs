using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

// public class AnimalMachine : StateMachine {
// 	[Header("饥饿度参数")]
// 	public float hunger = 100.0f; // 饥饿度，从0到100
// 	public float maxHunger = 100.0f; // 最大饥饿度
// 	public float searchRadius = 10.0f; // 搜索食物的半径

// 	[Header("控制组件")]
// 	public Movement movement; // 运动控制器
// 	public Animator animator; // 动画控制器

// 	private float hungerTimer = 0.0f; // 计时器
// 	private float chasingCooldownTimer = 0.0f; // 追逐状态冷却计时器
// 	private const float chasingCooldown = 5.0f; // 追逐状态冷却时间

// 	protected override void InitializeStates() {
// 		RegisterState("Idle", new AnimalIdleState(this));
// 		RegisterState("Roaming", new AnimalRoamingState(this));
// 		RegisterState("Chasing", new AnimalChasingState<AnimalFood>(this));
// 		RegisterState("Eating", new AnimalEatingState(this));

// 		ChangeState("Idle");
// 	}

// 	protected override void Update() {
// 		base.Update();

// 		// 更新计时器
// 		hungerTimer += Time.deltaTime;
// 		chasingCooldownTimer += Time.deltaTime;

// 		if (hungerTimer >= 1.0f) { ConsumeHunger(); }
// 	}

// 	public virtual void ConsumeHunger() {
// 		// 重置计时器
// 		hungerTimer = 0.0f;
// 		// 每次消耗1点饥饿度
// 		hunger -= 1.0f;
// 		if (hunger < 0.0f) { hunger = 0.0f; }

// 		// 如果饥饿度低于最大饥饿度的70%，有50%的概率触发Chasing状态
// 		// 如果饥饿度低于最大饥饿度的30%，有90%的概率触发Chasing状态
// 		float foraging = hunger < maxHunger * 0.3f ? 0.9f : 0.5f;
// 		bool valid = hunger < maxHunger * 0.7f && Random.value < foraging;

// 		// 如果触发Chasing状态，且冷却时间已过，切换到Chasing状态
// 		if (valid && chasingCooldownTimer >= chasingCooldown) {
// 			ChangeState("Chasing");
// 			chasingCooldownTimer = 0.0f; // 重置冷却计时器
// 		}
// 	}

// 	// 从指定范围内查找指定类型的组件
// 	public virtual bool Find<T>(out T value) where T : Component {
// 		Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
// 		foreach (Collider collider in colliders) {
// 			T component = collider.GetComponent<T>();
// 			if (component != null) {
// 				value = component;
// 				return true;
// 			}
// 		}
// 		value = null;
// 		return false;
// 	}

// 	// public override bool UpdateMove(Vector3 position) {
// 	//     return movement.UpdateMove(position);
// 	// }
// 	// public override void AnimationTrigger(string value) {
// 	//     throw new System.NotImplementedException();
// 	// }
// 	// public override void AnimationEnd() {
// 	//     throw new System.NotImplementedException();
// 	// }
// }
