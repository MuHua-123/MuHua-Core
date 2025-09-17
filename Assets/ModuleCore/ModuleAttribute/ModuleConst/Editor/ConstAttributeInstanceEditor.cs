using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 属性实例 - 自定义编辑器
/// </summary>
[CustomEditor(typeof(ConstAttributeInstance))]
public class ConstAttributeInstanceEditor : Editor {

	private ConstAttributeInstance instance;

	private void Awake() => instance = target as ConstAttributeInstance;

	public override void OnInspectorGUI() {
		base.OnInspectorGUI();
		if (instance.container == null) { return; }
		EditorGUILayout.Space(20);

		// 输入字段修改 name
		EditorGUI.BeginChangeCheck();
		string newName = EditorGUILayout.TextField("属性名称", instance.name);
		if (EditorGUI.EndChangeCheck()) { ModifyName(newName); }
		// 按钮功能
		if (GUILayout.Button("删除属性")) { DeleteInstance(); }
	}

	/// <summary> 修改名字 </summary>
	private void ModifyName(string newName) {
		instance.name = newName;
		Undo.RecordObject(instance, "修改属性名称");
		EditorUtility.SetDirty(instance);
		AssetDatabase.SaveAssets();
	}
	/// <summary> 删除实例 </summary>
	private void DeleteInstance() {
		ConstAttributeContainer container = instance.container;
		container.instances.Remove(instance);
		EditorUtility.SetDirty(container);
		Undo.DestroyObjectImmediate(instance);
		AssetDatabase.SaveAssets();
	}
}
