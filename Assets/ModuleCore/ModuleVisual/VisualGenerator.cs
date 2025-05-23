using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可视化生成器
/// </summary>
public abstract class VisualGenerator<T> : MonoBehaviour {

	/// <summary> 更新可视化内容 </summary>
	public abstract void CreateVisual(ref T visual, Transform original);
	/// <summary> 释放可视化内容 </summary>
	public abstract void ReleaseVisual(T visual);

	/// <summary> 创建可视化内容 </summary>
	public static Type Create<Type>(Transform original, Transform parent) {
		Transform temp = Instantiate(original, parent);
		temp.gameObject.SetActive(true);
		return temp.GetComponent<Type>();
	}
}
