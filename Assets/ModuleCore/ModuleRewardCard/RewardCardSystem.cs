using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

/// <summary>
/// 奖励卡系统
/// </summary>
public class RewardCardSystem : Module<RewardCardSystem> {
	/// <summary> 卡牌系统 </summary>
	private List<RewardCard> cards;

	/// <summary> 初始化 </summary>
	public void Initial(List<RewardCard> cards) => this.cards = cards;
	/// <summary> 添加卡牌 </summary>
	public void AddCard(RewardCard card) => cards.Add(card);
	/// <summary> 添加卡牌 </summary>
	public void AddCard(List<RewardCard> list) => cards.AddRange(list);
	/// <summary> 移除卡牌 </summary>
	public void RemoveCard(RewardCard card) => cards.Remove(card);

	/// <summary> 抽卡 </summary>
	public List<RewardCard> Draw(int count) {
		HashSet<string> repeats = new HashSet<string>();
		List<RewardCard> result = new List<RewardCard>();

		cards = Shuffle(cards);

		foreach (var card in cards) {
			if (result.Count >= count) break;
			if (repeats.Contains(card.name)) { continue; }
			result.Add(card);
			repeats.Add(card.name);
		}

		return result;
	}
	/// <summary> 洗牌算法（Fisher-Yates） </summary>
	public List<RewardCard> Shuffle(List<RewardCard> cards) {
		for (int i = cards.Count - 1; i > 0; i--) {
			int j = Random.Range(0, i + 1);
			RewardCard temp = cards[i];
			cards[i] = cards[j];
			cards[j] = temp;
		}
		return cards;
	}
}
