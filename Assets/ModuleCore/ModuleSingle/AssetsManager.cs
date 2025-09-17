using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

/// <summary>
/// 资源管理器
/// </summary>
public class AssetsManager : ModuleSingle<AssetsManager> {

	/// <summary> 炮塔列表 </summary>
	public List<Turret> turrets;
	/// <summary> 继承预设 </summary>
	public List<ConstAttributeContainer> presetInherits;

	protected override void Awake() => NoReplace(false);

	private void Start() {
		// 初始炮塔建造菜单
		turrets.ForEach(CreateTurretMenu);
		// 添加全局炮塔属性
		presetInherits.ForEach(obj => AttributeSystem.I.AddInstance(obj.name, obj.To()));
	}

	private void CreateTurretMenu(Turret turret) {
		string name = $"建造/{turret.name}";
		UIShortcutMenu.I.Add(name, () => { ModuleVisual.I.GeneratorTurret.CreateVisual(turret.transform); });
	}
}
