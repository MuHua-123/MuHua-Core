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
	public AttributeContainer attribute;

	private void Awake() => attribute = presetAttribute.To();

	private void Start() {
		AttributeSystem.OnChange += RecalculateValue;
		RecalculateValue(AttributeSystem.I);
	}
	private void RecalculateValue(AttributeSystem system) {
		List<AttributeContainer> inherits = new List<AttributeContainer>();
		for (int i = 0; i < presetInherits.Count; i++) {
			string containerID = presetInherits[i].name;
			inherits.Add(system.FindContainer(containerID));
		}
		RecalculateValue(inherits);
	}
	private void RecalculateValue(List<AttributeContainer> inherits) {
		attribute.ForEach((attributeID, instance) => RecalculateValue(attributeID, instance, inherits));
		attribute.RecalculateValue();
	}
	private void RecalculateValue(string attributeID, AttributeInstance instance, List<AttributeContainer> inherits) {
		List<AttributeModifier> modifiers = new List<AttributeModifier>();
		for (int i = 0; i < inherits.Count; i++) {
			AttributeContainer container = inherits[i];
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
	public float value;
	/// <summary> 百分比% </summary>
	public float percentage;

	public override float Fixed(float input) => input + value;

	public override float Addition(float input) => input + percentage;
}