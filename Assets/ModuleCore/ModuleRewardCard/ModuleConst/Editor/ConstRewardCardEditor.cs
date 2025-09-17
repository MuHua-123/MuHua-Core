using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 奖励卡 - 自定义编辑器
/// </summary>
[CustomEditor(typeof(ConstRewardCard), true)]
public class ConstRewardCardEditor : Editor {

	private ConstRewardCard rewardCard;

	private void Awake() => rewardCard = target as ConstRewardCard;

	public override void OnInspectorGUI() {
		// 输入字段修改 name
		// if (rewardCard.parent != null) {
		// 	EditorGUI.BeginChangeCheck();
		// 	string newName = EditorGUILayout.TextField("卡牌名称", rewardCard.name);
		// 	if (EditorGUI.EndChangeCheck()) { ModifyName(newName); }
		// }

		base.OnInspectorGUI();

		// EditorGUILayout.Space(20);
		// if (GUILayout.Button("创建炮塔卡牌")) { Create<ConstRewardCardTurret>(); }
		// if (GUILayout.Button("创建强化卡牌")) { Create<ConstRewardCardIntensify>(); }
		// if (GUILayout.Button("删除全部卡牌")) { DeleteAll(); }
		// if (rewardCard.parent == null) { return; }
		// if (GUILayout.Button("移出解锁队列")) { Delete(); }
	}

	// /// <summary> 修改名字 </summary>
	// private void ModifyName(string newName) {
	// 	rewardCard.name = newName;
	// 	Undo.RecordObject(rewardCard, "修改卡牌名称");
	// 	EditorUtility.SetDirty(rewardCard);
	// 	AssetDatabase.SaveAssets();
	// }
	// /// <summary> 创建 </summary>
	// private void Create<RewardCard>() where RewardCard : ConstRewardCard {
	// 	RewardCard instance = CreateInstance<RewardCard>();
	// 	instance.parent = rewardCard;
	// 	// 加入解锁队列
	// 	rewardCard.unlocks.Add(instance);
	// 	AssetDatabase.AddObjectToAsset(instance, rewardCard);
	// 	EditorUtility.SetDirty(instance);
	// 	EditorUtility.SetDirty(rewardCard);
	// 	AssetDatabase.SaveAssets();
	// }
	// /// <summary> 删除 </summary>
	// private void DeleteAll() {
	// 	for (int i = rewardCard.unlocks.Count; i-- > 0;) {
	// 		ConstRewardCard instance = rewardCard.unlocks[i];
	// 		rewardCard.unlocks.Remove(instance);
	// 		Undo.DestroyObjectImmediate(instance);
	// 	}
	// 	EditorUtility.SetDirty(rewardCard);
	// 	AssetDatabase.SaveAssets();
	// }
	// /// <summary> 删除实例 </summary>
	// private void Delete() {
	// 	ConstRewardCard parent = rewardCard.parent;
	// 	parent.unlocks.Remove(rewardCard);
	// 	EditorUtility.SetDirty(parent);
	// 	Undo.DestroyObjectImmediate(rewardCard);
	// 	AssetDatabase.SaveAssets();
	// }
}
