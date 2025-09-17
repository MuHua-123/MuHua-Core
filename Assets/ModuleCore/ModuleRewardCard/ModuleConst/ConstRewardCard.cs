using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 奖励卡 - 常量
/// </summary>
public abstract class ConstRewardCard : ScriptableObject {
	/// <summary> 描述 </summary>
	[TextArea]
	public string description;
	/// <summary> 贴图 </summary>
	public Sprite sprite;

	/// <summary> 解锁卡牌 </summary>
	public List<ConstRewardCard> unlocks;

	/// <summary> 转换数据 </summary>
	public abstract RewardCard To();

	public List<RewardCard> To(List<ConstRewardCard> unlocks) {
		List<RewardCard> cards = new List<RewardCard>();
		unlocks.ForEach(obj => cards.Add(obj.To()));
		return cards;
	}
}
