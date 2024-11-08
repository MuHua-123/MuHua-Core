using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModuleViewCamera : MonoBehaviour {
    /// <summary> 视图空间位置 </summary>
    public abstract Vector3 Position { get; set; }
    /// <summary> 视图空间旋转 </summary>
    public abstract Vector3 EulerAngles { get; set; }
    /// <summary> 视图空间缩放 </summary>
    public abstract Vector3 LocalScale { get; set; }
    /// <summary> 相机正交大小 </summary>
    public abstract float OrthographicSize { get; set; }
    /// <summary> 当前视图空间的中心点 </summary>
    public abstract Vector3 CurrentViewSpaceCenter { get; }
    /// <summary> 相机的世界位置 </summary>
    public abstract Vector3 CameraWorldPosition { get; }
    /// <summary> 渲染纹理 </summary>
    public abstract RenderTexture RenderTexture { get; }

    protected abstract void Awake();

    protected virtual ModuleCore ModuleCore => ModuleCore.I;

    /// <summary> 更新渲染纹理 </summary>
    public abstract void UpdateRenderTexture(int x, int y);
    /// <summary> 屏幕坐标转换世界坐标 </summary>
    public abstract Vector2 ScreenToWorldPosition(Vector2 screenPosition);
    /// <summary> 屏幕坐标转换世界坐标 (0-1) </summary>
    public abstract Vector2 ScreenToViewPosition(Vector2 screenPosition);
    /// <summary> 从屏幕坐标发射一条射线 </summary>
    public abstract Ray ScreenPointToRay(Vector2 screenPosition);
}
