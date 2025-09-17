using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 强化 - 奖励卡
/// </summary>
public class RewardCardIntensify : RewardCard {
	/// <summary> 强化值 </summary>
	public float value;
	/// <summary> 容器ID </summary>
	public string containerID;
	/// <summary> 属性ID </summary>
	public string attributeID;
	/// <summary> 解锁卡牌 </summary>
	public List<RewardCard> unlocks;

	public override void Execute() {
		// 移除卡牌
		RewardCardSystem.I.RemoveCard(this);
		// 添加修改器
		TurretModifier modifier = new TurretModifier();
		modifier.value = value;
		AttributeSystem.I.AddModifier(containerID, attributeID, modifier, true);
		// 加入额外卡牌
		RewardCardSystem.I.AddCard(unlocks);
	}
}
