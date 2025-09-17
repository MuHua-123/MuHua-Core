using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 属性实例
/// </summary>
public class AttributeInstance {
	/// <summary> 属性类型 </summary>
	public AttributeType type;
	/// <summary> 基础值 </summary>
	public float baseValue;
	/// <summary> 当前值 </summary>
	public float currentValue;
	/// <summary> 附加值% </summary>
	public float additionValue;
	/// <summary> 最大值 </summary>
	public float minValue;
	/// <summary> 最小值 </summary>
	public float maxValue = float.MaxValue;
	/// <summary> 修改器列表 </summary>
	public List<AttributeModifier> modifiers = new List<AttributeModifier>();

	/// <summary> 添加修改器 </summary>
	public void AddModifier(AttributeModifier mod, bool isUpdate) {
		modifiers.Add(mod);
		if (isUpdate) { RecalculateValue(); }
	}
	/// <summary> 添加修改器 </summary>
	public void AddModifier(List<AttributeModifier> mods, bool isUpdate) {
		modifiers.AddRange(mods);
		if (isUpdate) { RecalculateValue(); }
	}
	/// <summary> 重新计算值 </summary>
	public void RecalculateValue() {
		currentValue = baseValue;
		modifiers.ForEach(mod => currentValue = mod.Fixed(currentValue));
		additionValue = 1;
		modifiers.ForEach(mod => additionValue = mod.Addition(additionValue));
		currentValue = currentValue * additionValue;
		currentValue = Mathf.Clamp(currentValue, minValue, maxValue);
	}
}
