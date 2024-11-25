using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 贝塞尔曲线计算边缘点
/// </summary>
public class UnitAlgorithmEdgePoint : UnitAlgorithm<DataPolygon> {

    /// <summary> 三阶贝塞尔曲线计算边缘点 </summary>
    public UnitAlgorithmEdgePoint() { }

    public void Compute(DataPolygon data) {
        //List<DataPoint> points = new List<DataPoint>(data.points);
        //List<Vector3> edgePoints = new List<Vector3>();
        //for (int i = 0; i < points.Count; i++) {
        //    DataPoint aPoint = points.LoopIndex(i);
        //    DataPoint bPoint = points.LoopIndex(i + 1);
        //    if (!aPoint.isCurveAfter && !bPoint.isCurveFront) {
        //        edgePoints.Add(aPoint.position);
        //    }
        //    if (!aPoint.isCurveAfter && bPoint.isCurveFront) {
        //        edgePoints.AddRange(CreateLine(aPoint, bPoint, bPoint.frontBezier, data.edgeSmooth));
        //    }
        //    if (aPoint.isCurveAfter && !bPoint.isCurveFront) {
        //        edgePoints.AddRange(CreateLine(aPoint, bPoint, aPoint.afterBezier, data.edgeSmooth));
        //    }
        //    if (aPoint.isCurveAfter && bPoint.isCurveFront) {
        //        edgePoints.AddRange(CreateLine(aPoint, bPoint, data.edgeSmooth));
        //    }
        //}
        //data.edgePoints = edgePoints;
    }

    #region 函数
    /// <summary> 二阶贝塞尔线段 </summary> 
    public List<Vector3> CreateLine(DataPoint aPoint, DataPoint bPoint, Vector3 b, float smooth) {
        List<Vector3> points = new List<Vector3>();
        //方向，距离
        Vector2 direction = (bPoint.position - aPoint.position).normalized;
        float distance = Vector2.Distance(bPoint.position, aPoint.position);
        //求余，得商数
        int quotient = Quotient(distance, smooth);
        //贝塞尔曲线点
        Vector3 a = aPoint.position;
        Vector3 c = bPoint.position;
        for (int i = 0; i < quotient; i++) {
            float t = i * (distance / quotient) / distance;
            Vector2 position = ComputeBezier(a, b, c, t);
            points.Add(position);
        }
        return points;
    }
    /// <summary> 三阶贝塞尔线段 </summary> 
    public List<Vector3> CreateLine(DataPoint aPoint, DataPoint bPoint, float smooth) {
        List<Vector3> points = new List<Vector3>();
        //方向，距离
        Vector2 direction = (bPoint.position - aPoint.position).normalized;
        float distance = Vector2.Distance(bPoint.position, aPoint.position);
        //求余，得商数
        int quotient = Quotient(distance, smooth);
        //贝塞尔曲线点
        //Vector3 a = aPoint.position;
        //Vector3 b = aPoint.afterBezier;
        //Vector3 c = bPoint.frontBezier;
        //Vector3 d = bPoint.position;
        //for (int i = 0; i < quotient; i++) {
        //    float t = i * (distance / quotient) / distance;
        //    Vector2 position = ComputeBezier(a, b, c, d, t);
        //    points.Add(position);
        //}
        return points;
    }
    #endregion

    #region 算法
    /// <summary> 商数 </summary>
    public static int Quotient(float distance, float smooth) {
        int a = (int)(distance * 1000);
        int b = (int)(smooth * 1000);
        return Math.DivRem(a, b, out int remainder);
    }
    /// <summary>
    /// 一阶贝塞尔算法
    /// </summary>
    /// <param name="a">起点</param>
    /// <param name="b">终点</param>
    /// <param name="t">进度</param>
    /// <returns></returns>
    public static Vector3 ComputeBezier(Vector3 a, Vector3 b, float t) {
        return a + (b - a) * t;
    }
    /// <summary>
    /// 二阶贝塞尔算法
    /// </summary>
    /// <param name="a">起点</param>
    /// <param name="b">贝塞尔点</param>
    /// <param name="c">终点</param>
    /// <param name="t">进度</param>
    /// <returns>当前进度的曲线点</returns>
    public static Vector3 ComputeBezier(Vector3 a, Vector3 b, Vector3 c, float t) {
        Vector3 aa = a + (b - a) * t;
        Vector3 bb = b + (c - b) * t;
        return aa + (bb - aa) * t;
    }
    /// <summary>
    /// 三阶贝塞尔算法
    /// </summary>
    /// <param name="a">起点</param>
    /// <param name="b">起点的贝塞尔点</param>
    /// <param name="c">终点的贝塞尔点</param>
    /// <param name="d">终点</param>
    /// <param name="t">进度</param>
    /// <returns>当前进度的曲线点</returns>
    public static Vector3 ComputeBezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t) {
        Vector3 aa = a + (b - a) * t;
        Vector3 bb = b + (c - b) * t;
        Vector3 cc = c + (d - c) * t;

        Vector3 aaa = aa + (bb - aa) * t;
        Vector3 bbb = bb + (cc - bb) * t;
        return aaa + (bbb - aaa) * t;
    }
    #endregion

}
