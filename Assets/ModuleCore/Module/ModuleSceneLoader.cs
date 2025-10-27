using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

/// <summary>
/// 模组(可寻址) - 场景加载器
/// </summary>
public class ModuleSceneLoader : SceneLoader {

	public override IEnumerator ILoad(string sceneName, Action complete, LoadSceneMode mode) {
		SmoothedProgress = 0f;
		AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneName, mode, false);
		while (SmoothedProgress < 1f) { yield return ILoad(handle); }
		SceneSystem.OnProgress?.Invoke(false, SmoothedProgress);
		complete?.Invoke();
		SceneSystem.CompleteLoad();
	}
	/// <summary> 协程处理进度 </summary>
	public IEnumerator ILoad(AsyncOperationHandle<SceneInstance> handle) {
		float progress = Mathf.Clamp01(handle.PercentComplete / 0.9f);
		SceneSystem.smoothedProgress = Mathf.MoveTowards(SmoothedProgress, progress, Time.deltaTime);
		SceneSystem.OnProgress?.Invoke(true, SmoothedProgress);
		// 当加载进度达到90%且平滑变量和实际进度一致时，激活场景
		if (SmoothedProgress < 1f) { yield break; }
		handle.Result.ActivateAsync();
	}
}
