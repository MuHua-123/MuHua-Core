using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

/// <summary>
/// 游戏管理
/// </summary>
public class SingleManager : ModuleSingle<SingleManager> {

	/// <summary> 运行模式 </summary>
	public static EnumRunningMode runningMode;

	/// <summary> 设置运行模式 </summary>
	public static void SetRunningMode(EnumRunningMode runningMode) {
		SingleManager.runningMode = runningMode;
	}

	protected override void Awake() {
		NoReplace();
		// ManagerScene.OnComplete += ManagerScene_OnComplete;
	}
	private void Start() {
		// ModuleUI.Jump(EnumPage.Menu);
		// ModuleInput.Mode(EnumInputMode.None);
		// ModuleCamera.Mode(EnumCameraMode.None);
		// SceneManager.LoadScene("MenuScene");
	}

	private void ManagerScene_OnComplete() {
		// if (runningMode == EnumRunningMode.None) {
		// 	ModuleUI.Jump(EnumPage.Menu);
		// 	ModuleInput.Mode(EnumInputMode.None);
		// 	ModuleCamera.Mode(EnumCameraMode.None);
		// }
		// if (runningMode == EnumRunningMode.Standard) {
		// 	ModuleUI.Jump(EnumPage.Battle);
		// 	// ModuleInput.Mode(EnumInputMode.ThirdPerson);
		// 	// ModuleCamera.Mode(EnumCameraMode.ThirdPerson);
		// }
	}

	public void StartGame() {
		// ManagerScene.LoadScene(null);
		// ModuleUI.Jump(EnumPage.Battle);
		// ModuleInput.Mode(EnumInputMode.Standard);
		// ModuleCamera.Mode(EnumCameraMode.MoveAxis);
	}
}
