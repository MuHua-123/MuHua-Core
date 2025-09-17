using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 炮塔 - 自定义编辑器
/// </summary>
[CustomEditor(typeof(Turret))]
public class TurretEditor : Editor {

	private Turret turret;

	private void Awake() => turret = target as Turret;

	public override void OnInspectorGUI() {
		base.OnInspectorGUI();
		if (!Application.isPlaying) { return; }
		EditorGUILayout.Space(20);

		if (turret.attribute == null) {
			EditorGUILayout.HelpBox("attribute 未实例化", MessageType.Warning);
			return;
		}
		Dictionary<string, AttributeInstance> dictionary = turret.attribute.dictionary;
		foreach (var item in dictionary) {
			EditorGUILayout.TextField(item.Key, item.Value.currentValue.ToString());
		}
	}
}
