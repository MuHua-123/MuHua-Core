using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

/// <summary>
/// 炮塔
/// </summary>
public class Turret : MonoBehaviour {
	/// <summary> 预设 </summary>
	public ConstAttributeContainer presetAttribute;
	/// <summary> 继承预设 </summary>
	public List<ConstAttributeContainer> presetInherits;

	/// <summary> 属性 </summary>
	public DataAttributeContainer attribute;
	/// <summary> 继承属性 </summary>
	public List<DataAttributeContainer> inherits;

	private void Awake() => attribute = presetAttribute.To();

	private void Start() {
		inherits = new List<DataAttributeContainer>();
		for (int i = 0; i < presetInherits.Count; i++) {
			string containerID = presetInherits[i].name;
			inherits.Add(AttributeBonus.I.FindContainer(containerID));
		}
		RecalculateValue();
	}
	private void RecalculateValue() {
		attribute.ForEach(RecalculateValue);
		attribute.RecalculateValue();
	}
	private void RecalculateValue(string attributeID, DataAttributeInstance instance) {
		List<AttributeModifier> modifiers = new List<AttributeModifier>();
		for (int i = 0; i < inherits.Count; i++) {
			DataAttributeContainer container = inherits[i];
			modifiers.AddRange(container.FindModifier(attributeID));
		}
		instance.AddModifier(modifiers, false);
	}
}
/// <summary>
/// 攻击力 - 属性调整
/// </summary>
public class TurretModifier : AttributeModifier {
	/// <summary> 固定数值 </summary>
	public int value;
	/// <summary> 百分比% </summary>
	public int percentage;

	public override float Fixed(float input) => input + value;

	public override float Addition(float input) => input + percentage;
}