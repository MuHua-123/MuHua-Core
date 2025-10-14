using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

/// <summary>
/// 资源管理器
/// </summary>
public class AssetsManager : ModuleSingle<AssetsManager> {

	public List<SceneDataConst> sceneDatas;

	protected override void Awake() {
		NoReplace(false);
		ModuleSystem.OnChange += UpdateScene;
		ModuleSystem.LoadModules();
	}

	private void UpdateScene() {
		SceneSystem.I.scenes.Clear();
		sceneDatas.ForEach(obj => SceneSystem.AddScene(obj.ToData()));
		ModuleSystem.Loads<SceneDataConst>("default", obj => SceneSystem.AddScene(obj.ToData()));
	}
}
