using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCameraDesign : ModuleViewCamera {
    public Camera viewCamera;
    public Transform viewSpace;
    private RenderTexture renderTexture;
    private readonly Vector3 CameraOffset = new Vector3(0, 0, -2);

    protected override void Awake() => ModuleCore.ViewCameraDesign = this;

    public override Vector3 position {
        get => viewCamera.transform.localPosition - CameraOffset;
        set => viewCamera.transform.localPosition = value + CameraOffset;
    }
    public override Vector3 eulerAngles {
        get => viewCamera.transform.eulerAngles;
        set => viewCamera.transform.eulerAngles = value;
    }
    public override float scale {
        get => viewCamera.orthographicSize;
        set => viewCamera.orthographicSize = value;
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
        return ScreenToViewPosition(screenPosition) * scale + position;
    }
    public override Vector3 ViewToScreenPosition(Vector3 screenPosition) {
        throw new System.NotImplementedException();
    }
    public override Vector3 ViewToWorldPosition(Vector3 screenPosition) {
        throw new System.NotImplementedException();
    }
    public override Vector3 WorldToScreenPosition(Vector3 screenPosition) {
        throw new System.NotImplementedException();
    }
    public override Vector3 WorldToViewPosition(Vector3 screenPosition) {
        throw new System.NotImplementedException();
    }
}
