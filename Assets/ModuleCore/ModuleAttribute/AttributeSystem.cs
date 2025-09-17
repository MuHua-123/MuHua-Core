using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

/// <summary>
/// 属性附加
/// </summary>
public class AttributeSystem : Module<AttributeSystem> {
	/// <summary> 修改事件 </summary>
	public static event Action<AttributeSystem> OnChange;
	/// <summary> 附加字典 </summary>
	public Dictionary<string, AttributeContainer> dictionary = new Dictionary<string, AttributeContainer>();

	/// <summary> 应用修改 </summary>
	public void Apply() => OnChange?.Invoke(this);
	/// <summary> 添加容器 </summary>
	public void AddInstance(string containerID, AttributeContainer container) {
		if (dictionary.ContainsKey(containerID)) { return; }
		dictionary.Add(containerID, container);
		OnChange?.Invoke(this);
	}
	/// <summary> 添加修改器 </summary>
	public void AddModifier(string containerID, string attributeID, AttributeModifier mod, bool isUpdate) {
		if (!dictionary.ContainsKey(containerID)) { return; }
		dictionary[containerID].AddModifier(attributeID, mod, isUpdate);
		OnChange?.Invoke(this);
	}
	/// <summary> 重新计算值 </summary>
	public void RecalculateValue() {
		foreach (var item in dictionary) { item.Value.RecalculateValue(); }
	}
	/// <summary> 查询容器 </summary>
	public AttributeContainer FindContainer(string containerID) {
		if (!dictionary.ContainsKey(containerID)) { return new(); }
		return dictionary[containerID];
	}
	/// <summary> 查询容器 </summary>
	public bool TryContainer(string containerID, out AttributeContainer container) {
		container = null;
		if (!dictionary.ContainsKey(containerID)) { return false; }
		container = dictionary[containerID];
		return true;
	}
}
