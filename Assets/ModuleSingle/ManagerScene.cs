using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MuHua;

/// <summary>
/// 场景 - 管理器
/// </summary>
public class ManagerScene : ModuleSingle<ManagerScene> {
	/// <summary> 场景加载完成 </summary>
	public static event Action OnCompleteLoad;
	/// <summary> 平滑进度 </summary>
	public float smoothedProgress;

	protected override void Awake() => NoReplace(false);

	#region 协程加载
	/// <summary> 协程加载内置场景 </summary>
	public void Load(string sceneName, Action complete = null, LoadSceneMode mode = LoadSceneMode.Single) {
		StartCoroutine(ILoad(sceneName, complete, mode));
	}
	/// <summary> 协程加载内置场景 </summary>
	public IEnumerator ILoad(string sceneName, Action complete, LoadSceneMode mode) {
		smoothedProgress = 0f;
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, mode);
		operation.allowSceneActivation = false;
		while (!operation.isDone) { yield return ILoad(operation); }
		SettingsProgress(false, smoothedProgress);
		complete?.Invoke();
		OnCompleteLoad?.Invoke();
	}
	/// <summary> 协程处理进度 </summary>
	public IEnumerator ILoad(AsyncOperation operation) {
		// 输出加载进度
		float progress = Mathf.Clamp01(operation.progress / 0.9f);
		smoothedProgress = Mathf.MoveTowards(smoothedProgress, progress, Time.deltaTime);
		SettingsProgress(true, smoothedProgress);
		// 当加载进度达到90%且平滑变量和实际进度一致时，激活场景
		if (operation.progress < 0.9f || smoothedProgress < 1f) { yield break; }
		operation.allowSceneActivation = true;
	}
	#endregion

	/// <summary> 设置进度 </summary>
	private void SettingsProgress(bool active, float progress) {
		// ModuleUI.LoadingSettings(active, progress);
	}
}
