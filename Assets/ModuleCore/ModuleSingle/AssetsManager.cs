using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

/// <summary>
/// 资源管理器
/// </summary>
public class AssetsManager : ModuleSingle<AssetsManager> {

	public List<ConstSceneData> sceneDatas;

	protected override void Awake() {
		NoReplace(false);
		sceneDatas.ForEach(obj => SceneSystem.AddScene(obj.ToData()));
	}

}
