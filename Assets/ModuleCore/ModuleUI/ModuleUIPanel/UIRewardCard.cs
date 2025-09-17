using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// 奖励卡UI
/// </summary>
public class UIRewardCard : ModuleUIPanel {

	private ModuleUIItems<UIItem, RewardCard> items;

	public VisualElement Container => Q<VisualElement>("Container");

	public UIRewardCard(VisualElement element, VisualTreeAsset templateAsset) : base(element) {
		items = new ModuleUIItems<UIItem, RewardCard>(Container, templateAsset,
		(data, element) => new UIItem(data, element, this));
	}

	public void Settings(bool active) {
		element.EnableInClassList("document-page-hide", !active);
		if (!active) { return; }
		List<RewardCard> rewardCards = RewardCardSystem.I.Draw(3);
		items.Create(rewardCards);
	}

	/// <summary> UI项 </summary>
	public class UIItem : ModuleUIItem<RewardCard> {
		public readonly UIRewardCard parent;

		public Label Label => Q<Label>("Label");

		public UIItem(RewardCard value, VisualElement element, UIRewardCard parent) : base(value, element) {
			this.parent = parent;
			Label.text = value.name;
			element.RegisterCallback<ClickEvent>(evt => Select());
		}
		public override void SelectState() {
			value.Execute();
			parent.Settings(false);
		}
	}
}
