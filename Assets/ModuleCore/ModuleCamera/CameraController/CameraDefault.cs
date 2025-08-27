using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 默认相机
/// </summary>
public class CameraDefault : CameraController {

	public override Vector3 Position {
		get => throw new System.NotImplementedException();
		set => throw new System.NotImplementedException();
	}
	public override Vector3 Forward {
		get => throw new System.NotImplementedException();
		set => throw new System.NotImplementedException();
	}
	public override Vector3 Right {
		get => throw new System.NotImplementedException();
		set => throw new System.NotImplementedException();
	}
	public override Vector3 EulerAngles {
		get => throw new System.NotImplementedException();
		set => throw new System.NotImplementedException();
	}
	public override float VisualField {
		get => throw new System.NotImplementedException();
		set => throw new System.NotImplementedException();
	}

	public override void ModuleCamera_OnCameraMode(CameraMode mode) {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
		return;
#endif
		gameObject.SetActive(mode == CameraMode.None);
		if (mode == CameraMode.None) { ModuleCamera.CurrentCamera = this; }
	}

	public override void ResetCamera() {
		// transform.position = HotUpdateScene.I.StartPoint.position;
		// transform.eulerAngles = HotUpdateScene.I.StartPoint.eulerAngles;
	}
}
