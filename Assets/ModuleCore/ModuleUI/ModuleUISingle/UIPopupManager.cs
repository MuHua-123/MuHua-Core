using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// UI弹出管理器
/// </summary>
public class UIPopupManager : ModuleUISingle<UIPopupManager> {

	// private UILoading loading;
	// private UIBannerTip bannerTip;

	public override VisualElement Element => root.Q<VisualElement>("Popup");

	public VisualElement Loading => Q<VisualElement>("Loading");
	public VisualElement BannerTip => Q<VisualElement>("BannerTip");

	protected override void Awake() {
		NoReplace(false);
		// loading = new UILoading(Loading, root);
		// bannerTip = new UIBannerTip(BannerTip);
	}

	public static void SettingsLoading(bool active, float value1, string value2) {
		// I.loading.Settings(active, value1, value2);
	}
	public static void SettingsBannerTip(bool active, string value) {
		// I.bannerTip.Settings(active, value);
	}
}
