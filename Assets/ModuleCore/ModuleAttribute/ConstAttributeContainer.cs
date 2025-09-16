using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 属性容器 - 常量
/// </summary>
[CreateAssetMenu(menuName = "MuHua/属性模块/属性容器")]
public class ConstAttributeContainer : ScriptableObject {
	/// <summary> 属性列表 </summary>
	[HideInInspector]
	public List<ConstAttributeInstance> instances;

	/// <summary> 转换数据 </summary>
	public DataAttributeContainer To() {
		DataAttributeContainer container = new DataAttributeContainer();
		instances.ForEach(obj => container.AddInstance(obj.name, obj.To()));
		return container;
	}
}
