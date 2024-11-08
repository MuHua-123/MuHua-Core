using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModulePlateDesign : MonoBehaviour {
    /// <summary> 必须初始化 </summary>
    protected virtual void Awake() {
        ModuleCore.PlateDesign = this;
    }
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;
    /// <summary> 视图相机模块 </summary>
    protected virtual ModuleViewCamera ViewCamera => ModuleCore.PlateDesignViewCamera;

    /// <summary> 添加一个板片数据 </summary>
    public abstract void AddData(DataPlate data);

    #region 边缘点操作
    /// <summary> 是否有效的边缘点 </summary>
    public abstract bool IsValidEdgePoint { get; }
    /// <summary> 返回当前边缘点位置 </summary>
    public abstract Vector3 EdgePointPosition { get; }
    /// <summary> 选中一个边缘点 </summary>
    public abstract void SelectEdgePoint(Vector3 screenPosition);
    /// <summary> 改变边缘点位置 </summary>
    public abstract void ChangeEdgePoint(Vector3 localPosition);
    /// <summary> 插入一个边缘点 </summary>
    public abstract void InsertEdgePoint(Vector3 screenPosition);
    /// <summary> 释放边缘点 </summary>
    public abstract void ReleaseEdgePoint();
    #endregion

    #region 设计点操作
    /// <summary> 是否有效的设计点 </summary>
    public abstract bool IsValidDesignPoint { get; }
    /// <summary> 返回当前设计点位置 </summary>
    public abstract Vector3 DesignPointPosition { get; }
    /// <summary> 选中一个设计点 </summary>
    public abstract void SelectDesignPoint(Vector3 screenPosition);
    /// <summary> 改变设计点位置 </summary>
    public abstract void ChangeDesignPoint(Vector3 localPosition);
    /// <summary> 插入一个设计点 </summary>
    public abstract void InsertDesignPoint(Vector3 screenPosition);
    /// <summary> 释放设计点 </summary>
    public abstract void ReleaseDesignPoint();
    #endregion

    #region 贝塞尔曲线操作
    /// <summary> 是否有效的贝塞尔点 </summary>
    public abstract bool IsValidBezierPoint { get; }
    /// <summary> 返回当前贝塞尔点位置 </summary>
    public abstract Vector3 BezierPointPosition { get; }
    /// <summary> 选中一个贝塞尔点 </summary>
    public abstract void SelectBezierPoint(Vector3 screenPosition);
    /// <summary> 改变贝塞尔点位置 </summary>
    public abstract void ChangeBezierPoint(Vector3 localPosition);
    /// <summary> 释放贝塞尔点 </summary>
    public abstract void ReleaseBezierPoint();
    #endregion

}
