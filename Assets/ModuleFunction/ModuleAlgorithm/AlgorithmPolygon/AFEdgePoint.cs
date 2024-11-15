using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 三阶贝塞尔曲线计算边缘点
/// </summary>
public class AFEdgePoint : ModuleAlgorithmFunction<DataPolygon> {

    /// <summary> 三阶贝塞尔曲线计算边缘点 </summary>
    public AFEdgePoint() { }

    public override void Compute(DataPolygon data) {
        List<DataPoint> points = new List<DataPoint>(data.points);
        List<Vector3> edgePoints = new List<Vector3>();
        for (int i = 0; i < points.Count; i++) {
            DataPoint current = points.LoopIndex(i);
            DataPoint next = points.LoopIndex(i + 1);
            edgePoints.AddRange(CreateLine(current, next, data.edgeSmooth));
        }
        data.edgePoints = edgePoints;
    }

    #region 函数
    public List<Vector3> CreateLine(DataPoint current, DataPoint next, float edgeSmooth) {
        List<Vector3> edgePoints = new List<Vector3>();
        //方向，距离
        Vector2 direction = (next.position - current.position).normalized;
        float distance = Vector2.Distance(next.position, current.position);
        //求余，得商数
        int quotient = Quotient(distance, edgeSmooth);
        //点位间距
        float segment = distance / quotient;
        //贝塞尔曲线点
        Vector3 ap = current.position;
        Vector3 bp = current.isCurveAfter ? current.afterBezier : current.position;
        Vector3 cp = next.isCurveFront ? next.frontBezier : next.position;
        Vector3 dp = next.position;
        for (int i = 0; i < quotient; i++) {
            float t = segment * i / distance;
            Vector2 position = ComputeBezier(ap, bp, cp, dp, t);
            edgePoints.Add(position);
        }
        return edgePoints;
    }
    #endregion

    #region 算法
    /// <summary> 商数 </summary>
    public static int Quotient(float distance, float edgeSmooth) {
        int a = (int)(distance * 1000);
        int b = (int)(edgeSmooth * 1000);
        return Math.DivRem(a, b, out int remainder);
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
