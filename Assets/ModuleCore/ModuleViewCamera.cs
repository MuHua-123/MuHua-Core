using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 视图相机，把内容渲染到渲染纹理上
/// </summary>
public abstract class ModuleViewCamera : MonoBehaviour {
    /// <summary> 必须要初始化 </summary>
    protected abstract void Awake();
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;

    /// <summary> 视图位置 </summary>
    public abstract Vector3 position { get; set; }
    /// <summary> 视图旋转 </summary>
    public abstract Vector3 eulerAngles { get; set; }
    /// <summary> 视图缩放 </summary>
    public abstract float scale { get; set; }
    /// <summary> 渲染纹理 </summary>
    public abstract RenderTexture RenderTexture { get; }

    /// <summary> 更新渲染纹理 </summary>
    public abstract void UpdateRenderTexture(int x, int y);
    /// <summary> 屏幕坐标转换视图坐标(0-1) </summary>
    public abstract Vector3 ScreenToViewPosition(Vector3 screenPosition);
    /// <summary> 屏幕坐标转换世界坐标 </summary>
    public abstract Vector3 ScreenToWorldPosition(Vector3 screenPosition);
    /// <summary> 视图坐标(0-1)转换屏幕坐标 </summary>
    public abstract Vector3 ViewToScreenPosition(Vector3 screenPosition);
    /// <summary> 视图坐标(0-1)转换世界坐标 </summary>
    public abstract Vector3 ViewToWorldPosition(Vector3 screenPosition);
    /// <summary> 世界坐标转换屏幕坐标 </summary>
    public abstract Vector3 WorldToScreenPosition(Vector3 screenPosition);
    /// <summary> 世界坐标转换视图坐标(0-1) </summary>
    public abstract Vector3 WorldToViewPosition(Vector3 screenPosition);
}
