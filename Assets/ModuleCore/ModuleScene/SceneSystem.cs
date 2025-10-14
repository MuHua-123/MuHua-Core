using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MuHua;

/// <summary>
/// 场景系统
/// </summary>
public class SceneSystem : Module<SceneSystem> {
	/// <summary> 场景加载完成事件 </summary>
	public static event Action OnCompleteLoad;
	/// <summary> 场景加载完成 </summary>
	public static void CompleteLoad() => OnCompleteLoad?.Invoke();

	/// <summary> 平滑进度 </summary>
	public static float smoothedProgress;
	/// <summary> 默认场景加载器 </summary>
	public static SceneLoader loader = new SceneLoaderStandard();
	/// <summary> 场景加载进度 </summary>
	public static Action<bool, float> OnProgress;
	/// <summary> 场景数据 </summary>
	public List<SceneData> scenes = new List<SceneData>();

	/// <summary> 添加场景数据 </summary>
	public static void AddScene(SceneData scene) {
		if (scene == null) return;
		if (I.scenes.Contains(scene)) return;
		I.scenes.Add(scene);
	}

	/// <summary> 加载场景(协程) </summary>
	public void Load(SceneData scene) => Load(scene.name);
	/// <summary> 加载场景(协程) </summary>
	public void Load(string sceneName, Action complete = null, LoadSceneMode mode = LoadSceneMode.Single) {
		if (loader == null) { Debug.LogError("未初始化加载器!"); return; }
		SingleManager.I.StartCoroutine(loader.ILoad(sceneName, complete, mode));
	}
	/// <summary> 加载场景(异步) </summary>
	public void LoadAsync(SceneData scene) => LoadAsync(scene.name);
	/// <summary> 加载场景(异步) </summary>
	public void LoadAsync(string sceneName, Action complete = null, LoadSceneMode mode = LoadSceneMode.Single) {
		if (loader == null) { Debug.LogError("未初始化加载器!"); return; }
		loader.LoadAsync(sceneName, complete, mode);
	}

}
