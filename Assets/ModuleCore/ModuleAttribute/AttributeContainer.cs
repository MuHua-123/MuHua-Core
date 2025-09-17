using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 属性容器
/// </summary>
public class AttributeContainer {
	/// <summary> 属性字典 </summary>
	public Dictionary<string, AttributeInstance> dictionary = new Dictionary<string, AttributeInstance>();

	/// <summary> 添加属性 </summary>
	public void AddInstance(string attributeID, AttributeInstance instance) {
		if (dictionary.ContainsKey(attributeID)) { return; }
		dictionary.Add(attributeID, instance);
	}
	/// <summary> 添加修改器 </summary>
	public void AddModifier(string attributeID, AttributeModifier mod, bool isUpdate) {
		if (!dictionary.ContainsKey(attributeID)) { return; }
		dictionary[attributeID].AddModifier(mod, isUpdate);
	}
	/// <summary> 重新计算值 </summary>
	public void RecalculateValue() {
		foreach (var item in dictionary) { item.Value.RecalculateValue(); }
	}
	/// <summary> 循环属性集合 </summary>
	public void ForEach(Action<string, AttributeInstance> action) {
		foreach (var item in dictionary) { action?.Invoke(item.Key, item.Value); }
	}
	/// <summary> 查询值 </summary>
	public float FindValue(string attributeID) {
		if (!dictionary.ContainsKey(attributeID)) { return 0; }
		return dictionary[attributeID].currentValue;
	}
	/// <summary> 查询修改器 </summary>
	public List<AttributeModifier> FindModifier(string attributeID) {
		if (!dictionary.ContainsKey(attributeID)) { return new(); }
		return dictionary[attributeID].modifiers;
	}
}
