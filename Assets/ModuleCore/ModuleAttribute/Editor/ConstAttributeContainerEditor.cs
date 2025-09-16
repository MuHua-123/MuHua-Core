using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 属性容器 - 自定义编辑器
/// </summary>
[CustomEditor(typeof(ConstAttributeContainer))]
public class ConstAttributeContainerEditor : Editor {

	private ConstAttributeContainer container;

	private void Awake() => container = target as ConstAttributeContainer;

	public override void OnInspectorGUI() {
		base.OnInspectorGUI();
		if (GUILayout.Button("创建属性")) { GenerateAttribute(); }
		if (GUILayout.Button("删除属性")) { DeleteInstance(); }
	}

	/// <summary> 创建属性实例 </summary>
	private void GenerateAttribute() {
		ConstAttributeInstance instance = CreateInstance<ConstAttributeInstance>();
		instance.container = container;
		// 加入容器
		container.instances.Add(instance);
		AssetDatabase.AddObjectToAsset(instance, container);
		EditorUtility.SetDirty(instance);
		EditorUtility.SetDirty(container);
		AssetDatabase.SaveAssets();
	}
	/// <summary> 删除全部实例 </summary>
	private void DeleteInstance() {
		for (int i = container.instances.Count; i-- > 0;) {
			ConstAttributeInstance instance = container.instances[i];
			container.instances.Remove(instance);
			Undo.DestroyObjectImmediate(instance);
		}
		EditorUtility.SetDirty(container);
		AssetDatabase.SaveAssets();
	}
}
