using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 标准 - 场景加载器
/// </summary>
public class SceneLoaderStandard : SceneLoader {

	/// <summary> 加载场景 </summary>
	public override IEnumerator ILoad(string sceneName, Action complete, LoadSceneMode mode) {
		SmoothedProgress = 0f;
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, mode);
		operation.allowSceneActivation = false;
		while (!operation.isDone) { yield return ILoad(operation); }
		SceneSystem.OnProgress?.Invoke(false, SmoothedProgress);
		complete?.Invoke();
		SceneSystem.CompleteLoad();
	}
	/// <summary> 协程处理进度 </summary>
	public IEnumerator ILoad(AsyncOperation operation) {
		float progress = Mathf.Clamp01(operation.progress / 0.9f);
		SmoothedProgress = Mathf.MoveTowards(SmoothedProgress, progress, Time.deltaTime);
		SceneSystem.OnProgress?.Invoke(true, SmoothedProgress);
		// 当加载进度达到90%且平滑变量和实际进度一致时，激活场景
		if (SmoothedProgress < 1f) { yield break; }
		operation.allowSceneActivation = true;
	}
}
