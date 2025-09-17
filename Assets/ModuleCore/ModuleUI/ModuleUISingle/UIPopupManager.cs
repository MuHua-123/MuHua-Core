using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// 弹出 - UI管理器
/// </summary>
public class UIPopupManager : ModuleUISingle<UIPopupManager> {

	public VisualTreeAsset rewardCardTemplate;

	public UIRewardCard rewardCard;

	public override VisualElement Element => root.Q<VisualElement>("Popup");

	public VisualElement RewardCard => Q<VisualElement>("RewardCard");

	protected override void Awake() {
		NoReplace(false);
		rewardCard = new UIRewardCard(RewardCard, rewardCardTemplate);
	}
	private void Start() {
		UIShortcutMenu.I.Add("抽卡", () => { rewardCard.Settings(true); });
	}
}
