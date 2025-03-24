using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

namespace MuHua.Sample {
	public class StandardLabel : MonoBehaviour {
		public GameObject labelPrefab;
		public Vector3 offset = new Vector3(0, 1, 0);
		private GameObject labelObject;

		void Start() {
			// labelObject = FollowerController.CreateLabel(transform, labelPrefab, offset);
		}
		void OnDestroy() {
			if (labelObject != null) { Destroy(labelObject); }
		}
	}
}

