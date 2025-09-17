using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 强化(连续) - 奖励卡
/// </summary>
[CreateAssetMenu(menuName = "MuHua/卡牌模块/强化卡牌(连续)")]
public class ConstRewardCardIntensifys : ConstRewardCard {
	[Header("额外属性")]
	/// <summary> 强化值 </summary>
	public float value;
	/// <summary> 容器ID </summary>
	public string containerID;
	/// <summary> 属性ID </summary>
	public string attributeID;

	public override RewardCard To() {
		RewardCardIntensify rewardCard = new RewardCardIntensify();
		rewardCard.name = name;
		rewardCard.description = description;
		rewardCard.sprite = sprite;
		rewardCard.unlocks = To(unlocks);

		rewardCard.value = value;
		rewardCard.containerID = containerID;
		rewardCard.attributeID = attributeID;
		return rewardCard;
	}
}
