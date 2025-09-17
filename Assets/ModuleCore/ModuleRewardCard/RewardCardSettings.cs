using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 奖励卡设置
/// </summary>
public class RewardCardSettings : MonoBehaviour {
	/// <summary> 初始卡组 </summary>
	public List<ConstRewardCard> rewardCards;

	private void Awake() {
		List<RewardCard> cards = new List<RewardCard>();
		rewardCards.ForEach(obj => cards.Add(obj.To()));
		RewardCardSystem.I.Initial(cards);
	}
}
