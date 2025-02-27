using UnityEngine;

namespace MuHua
{
	public class LabelController : MonoBehaviour
	{
		public static LabelController Instance { get; private set; }

		public Canvas canvas; // 包含标签的Canvas
		public GameObject labelPrefab; // 标签预制体

		void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(gameObject);
			}
		}

		public static GameObject CreateLabel(Transform target, Vector3 offset)
		{
			return CreateLabel(target, Instance.labelPrefab, offset);
		}
		public static GameObject CreateLabel(Transform target, GameObject labelPrefab, Vector3 offset)
		{
			GameObject labelObject = Instantiate(labelPrefab, Instance.canvas.transform);
			LabelFollower followObjectLabel = labelObject.GetComponent<LabelFollower>();
			followObjectLabel.target = target;
			followObjectLabel.offset = offset;

			return labelObject;
		}
	}
}