using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCameraBaking : ModuleViewCamera {
    public readonly Vector3 CameraOffset = new Vector3(0, 0, -1.5f);
    public Camera viewCamera;
    public Transform viewOrigin;
    public Transform viewSpace;
    private RaycastHit hitInfo;
    private RenderTexture renderTexture;

    protected override void Awake() => ModuleCore.ViewCameraBaking = this;

    public override Vector3 Position {
        get => viewOrigin.localPosition;
        set => viewOrigin.localPosition = value;
    }
    public override Vector3 EulerAngles {
        get => viewOrigin.eulerAngles;
        set => viewOrigin.eulerAngles = value;
    }
    public override float Scale {
        get => viewCamera.transform.localPosition.z;
        set => viewCamera.transform.localPosition = new Vector3(0, 0, value);
    }
    public override Vector3 Up {
        get => viewOrigin.up;
    }
    public override Vector3 Right {
        get => viewOrigin.right;
    }
    public override Vector3 Forward {
        get => viewOrigin.forward;
    }
    public override Vector3 CameraPosition {
        get => viewOrigin.localPosition;
    }
    public override RenderTexture RenderTexture {
        get => renderTexture;
    }

    public override void UpdateRenderTexture(int x, int y) {
        renderTexture = new RenderTexture(x, y, 0);
        viewCamera.targetTexture = renderTexture;
    }
    public override Vector3 ScreenToViewPosition(Vector3 screenPosition) {
        float x = screenPosition.x / viewCamera.pixelWidth;
        float y = 1 - screenPosition.y / viewCamera.pixelHeight;
        Vector3 viewPosition = new Vector3(x, y);
        return viewPosition;
    }
    public override Vector3 ScreenToWorldPosition(Vector3 screenPosition) {
        Vector3 viewPosition = ScreenToViewPosition(screenPosition);
        float aspectRatio = (float)viewCamera.pixelWidth / viewCamera.pixelHeight;
        Vector3 ratio = viewPosition - new Vector3(0.5f, 0.5f);
        return new Vector3(ratio.x * aspectRatio, ratio.y) * Scale + viewOrigin.position;
    }
    //public override Vector3 ViewToScreenPosition(Vector3 screenPosition) {
    //    throw new System.NotImplementedException();
    //}
    //public override Vector3 ViewToWorldPosition(Vector3 screenPosition) {
    //    throw new System.NotImplementedException();
    //}
    //public override Vector3 WorldToScreenPosition(Vector3 screenPosition) {
    //    throw new System.NotImplementedException();
    //}
    //public override Vector3 WorldToViewPosition(Vector3 screenPosition) {
    //    throw new System.NotImplementedException();
    //}

    public override bool ScreenToWorldObject<T>(Vector3 screenPosition, out T value) {
        return ScreenToWorldObject(screenPosition, out value, DefaultLayerMask);
    }
    public override bool ScreenToWorldObject<T>(Vector3 screenPosition, out T value, LayerMask planeLayerMask) {
        Vector3 viewPosition = ScreenToViewPosition(screenPosition);
        Ray ray = viewCamera.ViewportPointToRay(viewPosition);
        Physics.Raycast(ray, out hitInfo, 200, planeLayerMask);
        value = hitInfo.transform?.GetComponent<T>();
        return value != null;
    }
    public override bool ScreenToWorldObjectParent<T>(Vector3 screenPosition, out T value) {
        return ScreenToWorldObjectParent(screenPosition, out value, DefaultLayerMask);
    }
    public override bool ScreenToWorldObjectParent<T>(Vector3 screenPosition, out T value, LayerMask planeLayerMask) {
        Vector3 viewPosition = ScreenToViewPosition(screenPosition);
        Ray ray = viewCamera.ViewportPointToRay(viewPosition);
        Physics.Raycast(ray, out hitInfo, 200, planeLayerMask);
        value = hitInfo.transform?.GetComponentInParent<T>();
        return value != null;
    }
}
