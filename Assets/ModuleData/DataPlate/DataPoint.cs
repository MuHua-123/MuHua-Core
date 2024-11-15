using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPoint {
    /// <summary> 绑定的板片 </summary>
    public readonly DataPlate plate;
    /// <summary> 初始化 </summary>
    public DataPoint(DataPlate plate) => this.plate = plate;

    #region 核心数据
    /// <summary> 点前(-) 是否是曲线 </summary>
    public bool isCurveFront;
    /// <summary> 点后(+) 是否是曲线 </summary>
    public bool isCurveAfter;
    /// <summary> 设计点位置(本地坐标系) </summary>
    public Vector3 position;
    /// <summary> 贝塞尔曲线前(-) </summary>
    public Vector3 frontBezier;
    /// <summary> 贝塞尔曲线后(+) </summary>
    public Vector3 afterBezier;
    #endregion

    #region 可视化数据
    /// <summary> 可视化对象 </summary>
    public Transform transform;
    /// <summary> 可视化贝塞尔点前(-) </summary>
    public Transform frontBezierTransform;
    /// <summary> 可视化贝塞尔点后(+) </summary>
    public Transform afterBezierTransform;
    /// <summary> 可视化贝塞尔线前(-) </summary>
    public LineRenderer frontBezierLineRenderer;
    /// <summary> 可视化贝塞尔线后(+) </summary>
    public LineRenderer afterBezierLineRenderer;
    #endregion
}
