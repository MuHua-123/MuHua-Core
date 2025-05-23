using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移轴相机
/// </summary>
public class CameraMoveAxis : CameraController {

	public Camera mainCamera;
	public LayerMask layerMask;

	private RaycastHit hitInfo;

	public override Vector3 Position {
		get => transform.position;
		set => transform.position = value;
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
		get => transform.eulerAngles;
		set => transform.eulerAngles = value;
	}
	public override float Distance {
		get => GetVisualField();
		set => SetVisualField(value);
	}

	private float GetVisualField() {
		return Vector3.Distance(mainCamera.transform.localPosition, Vector3.zero);
	}
	private void SetVisualField(float value) {
		value = Mathf.Clamp(value, 10, 30);
		Vector3 direction = mainCamera.transform.localPosition - Vector3.zero;
		mainCamera.transform.localPosition = direction.normalized * value;
		// if (!Volume.profile.TryGet(out DepthOfField depthOfField)) { return; }
		// depthOfField.focusDistance.SetValue(new FloatParameter(value));
	}

	public override void Initialize() {
		ModuleCamera.OnCameraMode += ModuleCamera_OnCameraMode;
	}

	private void ModuleCamera_OnCameraMode(EnumCameraMode mode) {
		gameObject.SetActive(mode == EnumCameraMode.MoveAxis);
		if (mode == EnumCameraMode.MoveAxis) { ModuleCamera.CurrentCamera = this; }
	}

	public override void ResetCamera() {
		// transform.position = HotUpdateScene.I.StartPoint.position;
		// transform.eulerAngles = HotUpdateScene.I.StartPoint.eulerAngles;
	}

	public override Vector3 ScreenToWorldPosition(Vector3 screenPosition) {
		Ray ray = mainCamera.ScreenPointToRay(screenPosition);
		Physics.Raycast(ray, out hitInfo, 200f, layerMask);
		Vector3 position = Vector3.zero;
		if (hitInfo.transform != null) { position = hitInfo.point; }
		return position;
	}
}
