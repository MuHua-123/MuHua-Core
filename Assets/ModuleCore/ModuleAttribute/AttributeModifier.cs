using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 属性调整
/// </summary>
public abstract class AttributeModifier {
	/// <summary> 固定值 </summary>
	public abstract float Fixed(float input);
	/// <summary> 附加值% </summary>
	public abstract float Addition(float input);
}
