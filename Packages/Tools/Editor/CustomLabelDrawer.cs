using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using MuHua;

namespace MuHuaEditor {
	/// <summary>
	/// 定义对带有 `CustomLabelAttribute` 特性的字段的面板内容的绘制行为。
	/// </summary>
	[CustomPropertyDrawer(typeof(CustomLabelAttribute))]
	public class CustomLabelDrawer : PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			string customName = (attribute as CustomLabelAttribute).name;
			// 如果是列表的子元素，保持原 label，否则使用自定义 label
			if (property.propertyPath.Contains("Array.data")) {
				EditorGUI.PropertyField(position, property, label);
			}
			else {
				GUIContent customLabel = new GUIContent(customName);
				EditorGUI.PropertyField(position, property, customLabel);
			}
		}
	}
}
