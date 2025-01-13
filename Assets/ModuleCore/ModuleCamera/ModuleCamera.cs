using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 相机模块
/// </summary>
public abstract class ModuleCamera : MonoBehaviour {
    /// <summary> 默认图层遮罩 </summary>
    public static readonly LayerMask DefaultLayerMask = ~(1 << 0) | 1 << 0;
    /// <summary> 必须要初始化 </summary>
    protected abstract void Awake();

    /// <summary> 相机位置 </summary>
    public abstract Vector3 Position { get; set; }
    /// <summary> 相机旋转 </summary>
    public abstract Vector3 EulerAngles { get; set; }
    /// <summary> 相机视野 </summary>
    public abstract float VisualField { get; set; }
    /// <summary> 当前相机 </summary>
    public abstract Camera ViewCamera { get; }

    /// <summary> 渲染纹理 </summary>
    public virtual RenderTexture RenderTexture { get; }
    /// <summary> 更新渲染纹理 </summary>
    public virtual void UpdateRenderTexture(int x, int y) { }

    #region 坐标转换
    /// <summary> 屏幕坐标转换视图坐标(0-1) </summary>
    public virtual Vector3 ScreenToViewPosition(Vector3 screenPosition) {
        return ViewCamera.ScreenToViewportPoint(screenPosition);
    }
    /// <summary> 屏幕坐标转换世界坐标 </summary>
    public virtual Vector3 ScreenToWorldPosition(Vector3 screenPosition) {
        return ViewCamera.ScreenToWorldPoint(screenPosition);
    }
    /// <summary> 视图坐标(0-1)转换屏幕坐标</summary>
    public virtual Vector3 ViewToScreenPosition(Vector3 screenPosition) {
        return ViewCamera.ViewportToScreenPoint(screenPosition);
    }
    /// <summary> 视图坐标(0-1)转换世界坐标 </summary>
    public virtual Vector3 ViewToWorldPosition(Vector3 screenPosition) {
        return ViewCamera.ViewportToWorldPoint(screenPosition);
    }
    /// <summary> 世界坐标转换屏幕坐标 </summary>
    public virtual Vector3 WorldToScreenPosition(Vector3 screenPosition) {
        return ViewCamera.WorldToScreenPoint(screenPosition);
    }
    /// <summary> 世界坐标转换视图坐标(0-1) </summary>
    public virtual Vector3 WorldToViewPosition(Vector3 screenPosition) {
        return ViewCamera.WorldToViewportPoint(screenPosition);
    }
    #endregion

    #region 射线检测
    private Ray ray;
    private RaycastHit hitInfo;
    /// <summary> 屏幕坐标获取世界对象 </summary>
    public virtual bool ScreenToWorldObject<T>(Vector3 screenPosition, out T value) where T : Object {
        return ScreenToWorldObject(screenPosition, out value, DefaultLayerMask);
    }
    /// <summary> 屏幕坐标获取世界对象 </summary>
    public virtual bool ScreenToWorldObject<T>(Vector3 screenPosition, out T value, LayerMask planeLayerMask) where T : Object {
        ray = ViewCamera.ScreenPointToRay(screenPosition);
        Physics.Raycast(ray, out hitInfo, 200, planeLayerMask);
        value = hitInfo.transform?.GetComponent<T>();
        return value != null;
    }
    /// <summary> 屏幕坐标获取世界对象的父对象 </summary>
    public virtual bool ScreenToWorldObjectParent<T>(Vector3 screenPosition, out T value) where T : Object {
        return ScreenToWorldObjectParent(screenPosition, out value, DefaultLayerMask);
    }
    /// <summary> 屏幕坐标获取世界对象的父对象 </summary>
    public virtual bool ScreenToWorldObjectParent<T>(Vector3 screenPosition, out T value, LayerMask planeLayerMask) where T : Object {
        ray = ViewCamera.ScreenPointToRay(screenPosition);
        Physics.Raycast(ray, out hitInfo, 200, planeLayerMask);
        value = hitInfo.transform?.GetComponentInParent<T>();
        return value != null;
    }
    #endregion

    protected virtual void Update() {
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
    }
}
