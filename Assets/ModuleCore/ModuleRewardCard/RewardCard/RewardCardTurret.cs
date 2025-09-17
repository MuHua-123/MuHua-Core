using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炮塔 - 奖励卡
/// </summary>
public class RewardCardTurret : RewardCard {
	/// <summary> 炮塔 </summary>
	public Turret turret;
	/// <summary> 解锁卡牌 </summary>
	public List<RewardCard> unlocks;

	public override void Execute() {
		// 移除卡牌
		RewardCardSystem.I.RemoveCard(this);
		// 创建建造指令
		string name = $"建造/{turret.name}";
		UIShortcutMenu.I.Add(name, () => { ModuleVisual.I.GeneratorTurret.CreateVisual(turret.transform); });
		// 加入额外卡牌
		RewardCardSystem.I.AddCard(unlocks);
	}
}
