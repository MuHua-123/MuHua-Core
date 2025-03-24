using UnityEngine;

namespace MuHua {
	/// <summary>
	/// 跟随标签控制器
	/// </summary>
	public abstract class FollowController<T> : MonoBehaviour where T : FollowController<T> {
		/// <summary> 模块单例 </summary>
		public static T I => instance;
		/// <summary> 模块单例 </summary>
		protected static T instance;
		/// <summary> 初始化 </summary>
		protected abstract void Awake();

		/// <summary> 替换，并且设置切换场景不销毁 </summary>
		protected virtual void Replace(bool isDontDestroy = true) {
			if (instance != null) { Destroy(instance.gameObject); }
			instance = (T)this;
			if (isDontDestroy) { DontDestroyOnLoad(gameObject); }
		}
		/// <summary> 不替换，并且设置切换场景不销毁 </summary>
		protected virtual void NoReplace(bool isDontDestroy = true) {
			if (isDontDestroy) { DontDestroyOnLoad(gameObject); }
			if (instance == null) { instance = (T)this; }
			else { Destroy(gameObject); }
		}

		/// <summary> 创建标签 </summary>
		public static Transform CreateLabel(Transform target, Transform labelPrefab, Transform parent, Vector3 offset) {
			Transform labelObject = Instantiate(labelPrefab, parent);
			FollowTag followObjectLabel = labelObject.GetComponent<FollowTag>();
			followObjectLabel.target = target;
			followObjectLabel.offset = offset;
			return labelObject;
		}
	}
}