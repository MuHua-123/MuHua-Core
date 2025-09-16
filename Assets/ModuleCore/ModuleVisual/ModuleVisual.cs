using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

/// <summary>
/// 可视化模块
/// </summary>
public class ModuleVisual : ModuleSingle<ModuleVisual> {

	public VisualGenerator<Turret> GeneratorTurret;

	protected override void Awake() => NoReplace();

}
