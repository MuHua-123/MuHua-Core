using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using MuHua;

/// <summary>
/// 场景系统
/// </summary>
public class SceneSystem : Module<SceneSystem> {
	/// <summary> 场景加载完成 </summary>
	public static event Action OnCompleteLoad;
	/// <summary> 平滑进度 </summary>
	public float smoothedProgress;
	/// <summary> 场景数据 </summary>
	public List<SceneData> scenes = new List<SceneData>();

	/// <summary> 添加场景数据 </summary>
	public static void AddScene(SceneData scene) {
		if (scene == null) return;
		if (I.scenes.Contains(scene)) return;
		I.scenes.Add(scene);
	}

	/// <summary> 加载场景 </summary>
	public void Load(SceneData scene) {
		Debug.Log($"scene.isAddressables = {scene.isAddressables}");
		if (!scene.isAddressables) { Load(scene.name); }
		else { AddressablesLoad(scene.name); }
	}
	/// <summary> 加载场景 </summary>
	public void Load(string sceneName, Action complete = null, LoadSceneMode mode = LoadSceneMode.Single) {
		SingleManager.I.StartCoroutine(ILoad(sceneName, complete, mode));
	}
	/// <summary> 加载场景 </summary>
	public void AddressablesLoad(string sceneName, Action complete = null, LoadSceneMode mode = LoadSceneMode.Single) {
		SingleManager.I.StartCoroutine(IAddressablesLoad(sceneName, complete, mode));
	}

	#region 内置加载
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
		float progress = Mathf.Clamp01(operation.progress / 0.9f);
		smoothedProgress = Mathf.MoveTowards(smoothedProgress, progress, Time.deltaTime);
		SettingsProgress(true, smoothedProgress);
		// 当加载进度达到90%且平滑变量和实际进度一致时，激活场景
		if (smoothedProgress < 1f) { yield break; }
		operation.allowSceneActivation = true;
	}
	#endregion

	#region 地址加载
	/// <summary> 加载场景 </summary>
	public IEnumerator IAddressablesLoad(string sceneName, Action complete, LoadSceneMode mode) {
		smoothedProgress = 0f;
		AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneName, mode, false);
		while (smoothedProgress < 1f) { yield return IAddressablesLoad(handle); }
		SettingsProgress(false, smoothedProgress);
		complete?.Invoke();
		OnCompleteLoad?.Invoke();
	}
	/// <summary> 协程处理进度 </summary>
	public IEnumerator IAddressablesLoad(AsyncOperationHandle<SceneInstance> handle) {
		float progress = Mathf.Clamp01(handle.PercentComplete / 0.9f);
		smoothedProgress = Mathf.MoveTowards(smoothedProgress, progress, Time.deltaTime);
		SettingsProgress(true, smoothedProgress);
		// 当加载进度达到90%且平滑变量和实际进度一致时，激活场景
		if (smoothedProgress < 1f) { yield break; }
		handle.Result.ActivateAsync();
	}
	#endregion

	/// <summary> 设置进度 </summary>
	private void SettingsProgress(bool active, float progress) {
		UIPopupManager.SettingsLoading(active, progress, $"{(int)(progress * 100)}%");
	}
}
