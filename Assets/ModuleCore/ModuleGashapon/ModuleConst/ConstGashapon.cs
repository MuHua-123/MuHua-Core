using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 扭蛋 - 常量
/// </summary>
[CreateAssetMenu(menuName = "MuHua/扭蛋模块/扭蛋")]
public class ConstGashapon : ScriptableObject {
	/// <summary> 描述 </summary>
	public string description;
	/// <summary> 贴图 </summary>
	public Sprite sprite;

	/// <summary> 转换数据 </summary>
	public Gashapon To() {
		Gashapon gashapon = new Gashapon();
		gashapon.name = name;
		gashapon.description = description;
		gashapon.sprite = sprite;
		return gashapon;
	}
}
