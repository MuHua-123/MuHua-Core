using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炮塔 - 奖励卡
/// </summary>
[CreateAssetMenu(menuName = "MuHua/卡牌模块/炮塔卡牌")]
public class ConstRewardCardTurret : ConstRewardCard {
	[Header("额外属性")]
	/// <summary> 炮塔 </summary>
	public Turret turret;

	public override RewardCard To() {
		RewardCardTurret rewardCard = new RewardCardTurret();
		rewardCard.name = name;
		rewardCard.description = description;
		rewardCard.sprite = sprite;
		rewardCard.unlocks = To(unlocks);

		rewardCard.turret = turret;
		return rewardCard;
	}
}
