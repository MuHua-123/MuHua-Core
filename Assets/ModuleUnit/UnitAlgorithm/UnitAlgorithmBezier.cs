using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 贝塞尔算法
/// </summary>
public class UnitAlgorithmBezier : UnitAlgorithm<DataPlate> {
    /// <summary> 贝塞尔算法 </summary>
    public UnitAlgorithmBezier() { }

    public void Compute(DataPlate data) {
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < data.plateSides.Count; i++) {
            Compute(data.plateSides[i]);
            //points.AddRange(data.plateSides[i].positions);
        }
        //去除重复边缘点
        points = points.Distinct().ToList();
        //data.edgePoints = points;
    }
    public void Compute(DataPlateSide data) {
        DataBezier dataBezier = new DataBezier();
        dataBezier.bezier = data.bezier;
        //dataBezier.smooth = data.plate.smooth;
        dataBezier.aPoint = data.aPoint.position;
        dataBezier.bPoint = data.bPoint.position;
        dataBezier.aBezier = data.aBezier;
        dataBezier.bBezier = data.bBezier;

        dataBezier.Compute();

        //data.length = dataBezier.length;
        //data.positions = dataBezier.positions.ToArray();
        //data.lines = dataBezier.lines.ToArray();
    }

    public class DataBezier {
        //输入
        public float smooth;
        public Bezier bezier;
        public Vector3 aPoint;
        public Vector3 bPoint;
        public Vector3 aBezier;
        public Vector3 bBezier;
        //输出
        public float length;
        public List<Vector3> positions = new List<Vector3>();
        public List<DataPlateLine> lines = new List<DataPlateLine>();
        /// <summary> 计算曲线细分点 </summary>
        public void Compute() {
            //细分点
            if (bezier == Bezier.一阶) { positions = new List<Vector3> { aPoint, bPoint }; }
            if (bezier == Bezier.二阶) { positions = Compute(aPoint, aBezier, bPoint); }
            if (bezier == Bezier.三阶) { positions = Compute(aPoint, aBezier, bBezier, bPoint); }
            //线段
            lines = new List<DataPlateLine>();
            for (int i = 0; i < positions.Count - 1; i++) {
                DataPlateLine line = new DataPlateLine();
                line.a = positions.LoopIndex(i + 0);
                line.b = positions.LoopIndex(i + 1);
                line.origin = length;
                lines.Add(line);
                length += line.Distance;
            }
        }
        /// <summary> 二阶贝塞尔线段 </summary> 
        private List<Vector3> Compute(Vector3 a, Vector3 b, Vector3 c) {
            List<Vector3> points = new List<Vector3>();
            //方向，距离
            float distance = Vector2.Distance(c, a);
            //求余，得商数
            int quotient = Quotient(distance, smooth);
            //贝塞尔曲线点
            for (int i = 0; i < quotient; i++) {
                float t = i * (distance / quotient) / distance;
                Vector2 position = ComputeBezier(a, b, c, t);
                points.Add(position);
            }
            points.Add(c);
            return points;
        }
        /// <summary> 三阶贝塞尔线段 </summary> 
        private List<Vector3> Compute(Vector3 a, Vector3 b, Vector3 c, Vector3 d) {
            List<Vector3> points = new List<Vector3>();
            //方向，距离
            Vector2 direction = (d - a).normalized;
            float distance = Vector2.Distance(d, a);
            //求余，得商数
            int quotient = Quotient(distance, smooth);
            //贝塞尔曲线点
            for (int i = 0; i < quotient; i++) {
                float t = i * (distance / quotient) / distance;
                Vector2 position = ComputeBezier(a, b, c, d, t);
                points.Add(position);
            }
            points.Add(d);
            return points;
        }
    }

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
}
