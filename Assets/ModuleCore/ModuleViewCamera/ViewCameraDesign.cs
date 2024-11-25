using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCameraDesign : ModuleViewCamera {
    public Camera viewCamera;
    public Transform viewSpace;
    private RaycastHit hitInfo;
    private RenderTexture renderTexture;
    private readonly Vector3 CameraOffset = new Vector3(0, 0, -1.5f);

    protected override void Awake() => ModuleCore.ViewCameraDesign = this;

    public override Vector3 Position {
        get => viewCamera.transform.localPosition - CameraOffset;
        set => viewCamera.transform.localPosition = value + CameraOffset;
    }
    public override Vector3 EulerAngles {
        get => viewCamera.transform.eulerAngles;
        set => viewCamera.transform.eulerAngles = value;
    }
    public override float Scale {
        get => viewCamera.orthographicSize;
        set => viewCamera.orthographicSize = value;
    }
    public override Vector3 Up {
        get => viewCamera.transform.up;
    }
    public override Vector3 Right {
        get => viewCamera.transform.right;
    }
    public override Vector3 Forward {
        get => viewCamera.transform.forward;
    }
    public override Vector3 CameraPosition {
        get => viewCamera.transform.localPosition - CameraOffset;
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
        Vector3 mouseRatio = new Vector3(x - 0.5f, y - 0.5f);
        float aspectRatio = (float)viewCamera.pixelWidth / viewCamera.pixelHeight;
        return new Vector3(mouseRatio.x * aspectRatio, mouseRatio.y) * 2;
    }
    public override Vector3 ScreenToWorldPosition(Vector3 screenPosition) {
        return ScreenToViewPosition(screenPosition) * Scale + Position;
    }

    public override bool ScreenToWorldObject<T>(Vector3 screenPosition, out T value) {
        return ScreenToWorldObject(screenPosition, out value, DefaultLayerMask);
    }
    public override bool ScreenToWorldObject<T>(Vector3 screenPosition, out T value, LayerMask planeLayerMask) {
        Vector3 viewPosition = ScreenToViewPosition(screenPosition);
        Vector3 worldPosition = viewPosition * Scale + viewCamera.transform.position;
        Ray ray = new Ray(worldPosition, viewCamera.transform.forward);
        Physics.Raycast(ray, out hitInfo, 200, planeLayerMask);
        value = hitInfo.transform?.GetComponent<T>();
        return value != null;
    }
    public override bool ScreenToWorldObjectParent<T>(Vector3 screenPosition, out T value) {
        return ScreenToWorldObjectParent(screenPosition, out value, DefaultLayerMask);
    }
    public override bool ScreenToWorldObjectParent<T>(Vector3 screenPosition, out T value, LayerMask planeLayerMask) {
        Vector3 viewPosition = ScreenToViewPosition(screenPosition);
        Vector3 worldPosition = viewPosition * Scale + viewCamera.transform.position;
        Ray ray = new Ray(worldPosition, viewCamera.transform.forward);
        Physics.Raycast(ray, out hitInfo, 200, planeLayerMask);
        value = hitInfo.transform?.GetComponentInParent<T>();
        return value != null;
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
}
