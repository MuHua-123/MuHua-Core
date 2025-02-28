using UnityEngine;

namespace MuHua
{
	public class LabelController : MonoBehaviour
	{
		public static LabelController Instance { get; private set; }

		public Transform parent; // 标签父物体
		public GameObject labelPrefab; // 标签预制体

		void Awake()
		{
			if (Instance == null) { Instance = this; }
			else { Destroy(gameObject); }
		}

		// 启用标签
		public static void Enable(bool enable) => Instance.parent.gameObject.SetActive(enable);

		// 创建标签
		public static GameObject CreateLabel(Transform target) => CreateLabel(target, Vector3.zero);
		public static GameObject CreateLabel(Transform target, Vector3 offset) => CreateLabel(target, Instance.labelPrefab, offset);
		public static GameObject CreateLabel(Transform target, GameObject labelPrefab, Vector3 offset)
		{
			GameObject labelObject = Instantiate(labelPrefab, Instance.parent);
			LabelFollower followObjectLabel = labelObject.GetComponent<LabelFollower>();
			followObjectLabel.target = target;
			followObjectLabel.offset = offset;

			return labelObject;
		}
	}
}