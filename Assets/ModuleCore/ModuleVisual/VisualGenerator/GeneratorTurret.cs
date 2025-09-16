using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炮塔 - 可视化生成器
/// </summary>
public class GeneratorTurret : VisualGenerator<Turret> {

	public override Turret CreateVisual(Transform original) {
		return Create<Turret>(original, transform);
	}
	public override void UpdateVisual(ref Turret visual, Transform original) {
		ReleaseVisual(visual);
		visual = Create<Turret>(original, transform);
	}
	public override void ReleaseVisual(Turret visual) {
		if (visual == null) { return; }
		Destroy(visual.gameObject);
	}
	public override void ReleaseAllVisual() {
		foreach (Transform item in transform) { Destroy(item.gameObject); }
	}
}
