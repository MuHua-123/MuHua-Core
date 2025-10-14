using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景数据 - 常量
/// </summary>
[CreateAssetMenu(menuName = "MuHua/场景模块/场景数据")]
public class SceneDataConst : ScriptableObject {
	/// <summary> 场景预览 </summary>
	public Sprite preview;

	public SceneData ToData() {
		SceneData scene = new SceneData();
		scene.name = name;
		scene.preview = preview;
		return scene;
	}
}
