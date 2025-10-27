using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景加载器
/// </summary>
public abstract class SceneLoader {

	/// <summary> 平滑进度 </summary>
	public float SmoothedProgress {
		get => SceneSystem.smoothedProgress;
		set => SceneSystem.smoothedProgress = value;
	}

	/// <summary> 加载场景(协程) </summary>
	public abstract IEnumerator ILoad(string sceneName, Action complete, LoadSceneMode mode);
}
