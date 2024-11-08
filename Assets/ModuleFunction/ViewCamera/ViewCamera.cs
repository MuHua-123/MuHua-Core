using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 未初始化的视图相机模块 </summary>
public class ViewCamera : ModuleViewCamera {
    public Camera viewCamera;
    public Transform viewSpace;

    private RenderTexture renderTexture;

    public override Vector3 Position { get => viewSpace.position; set => viewSpace.position = value; }
    public override Vector3 EulerAngles { get => viewSpace.eulerAngles; set => viewSpace.eulerAngles = value; }
    public override Vector3 LocalScale { get => viewSpace.localScale; set => viewSpace.localScale = value; }
    public override float OrthographicSize { get => viewCamera.orthographicSize; set => viewCamera.orthographicSize = value; }
    public override Vector3 CurrentViewSpaceCenter => viewSpace.localPosition * -1;
    public override Vector3 CameraWorldPosition => viewCamera.transform.position;
    public override RenderTexture RenderTexture => renderTexture;

    protected override void Awake() {
        Debug.LogError("需要重写 ViewCamera 的 Awake 方法!");
    }
    public override void UpdateRenderTexture(int x, int y) {
        renderTexture = new RenderTexture(x, y, 0);
        viewCamera.targetTexture = renderTexture;
    }
    public override Vector2 ScreenToWorldPosition(Vector2 screenPosition) {
        return ScreenToViewPosition(screenPosition) * OrthographicSize;
    }
    public override Vector2 ScreenToViewPosition(Vector2 screenPosition) {
        float x = screenPosition.x / viewCamera.pixelWidth;
        float y = 1 - screenPosition.y / viewCamera.pixelHeight;
        Vector2 mouseRatio = new Vector2(x - 0.5f, y - 0.5f);
        float aspectRatio = (float)viewCamera.pixelWidth / viewCamera.pixelHeight;
        return new Vector2(mouseRatio.x * aspectRatio, mouseRatio.y) * 2;
    }
    public override Ray ScreenPointToRay(Vector2 screenPosition) {
        Vector3 mousePosition = ScreenToWorldPosition(screenPosition);
        Vector3 worldPosition = mousePosition + viewCamera.transform.position;
        return new Ray(worldPosition, viewCamera.transform.forward);
    }
}
