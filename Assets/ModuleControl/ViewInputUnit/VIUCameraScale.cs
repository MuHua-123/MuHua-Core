using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIUCameraScale : ModuleViewInputUnit {
    public enum ScaleType { Scale, Orthographic }
    private readonly ScaleType scaleType;
    private readonly ModuleViewCamera viewCamera;
    public VIUCameraScale(ModuleViewCamera viewCamera, ScaleType scaleType = ScaleType.Scale) {
        this.viewCamera = viewCamera;
        this.scaleType = scaleType;
    }
    public override void DownMouse(DataMouseInput data) {

    }
    public override void DragMouse(DataMouseInput data) {

    }
    public override void ReleaseMouse(DataMouseInput data) {

    }
    public override void ScrollWheel(DataMouseInput data) {
        if (scaleType == ScaleType.Scale) {
            float size = viewCamera.LocalScale.x + data.ScrollWheel;
            size = Mathf.Clamp(size, 0.5f, 4);
            Vector3 localScale = new Vector3(size, size, size);
            viewCamera.LocalScale = Vector3.Lerp(viewCamera.LocalScale, localScale, Time.deltaTime * 20);
        }
        if (scaleType == ScaleType.Orthographic) {
            float size = viewCamera.OrthographicSize + data.ScrollWheel;
            size = Mathf.Clamp(size, 0.1f, 4);
            viewCamera.OrthographicSize = Mathf.Lerp(viewCamera.OrthographicSize, size, Time.deltaTime * 20);
        }
    }
}
