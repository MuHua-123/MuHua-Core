using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 扭蛋
/// </summary>
public class Gashapon {
	/// <summary> 名字 </summary>
	public string name;
	/// <summary> 描述 </summary>
	public string description;
	/// <summary> 贴图 </summary>
	public Sprite sprite;

	/// <summary> 执行 </summary>
	public void Execute() { Debug.Log(description); }
}
