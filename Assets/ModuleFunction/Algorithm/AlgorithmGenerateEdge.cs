using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// 算法：根据设计点来生成边缘点
/// 依据：???
/// </summary>
public class AlgorithmGenerateEdge : ModuleAlgorithm<DataPlate> {
    /// <summary> 算法：根据设计点来生成边缘点 </summary>
    public AlgorithmGenerateEdge() { }

    public override void Compute(DataPlate data) {
        data.edgePoints = new List<Vector2>();
        int maxIndex = data.designPoints.Count;
        for (int i = 0; i < maxIndex; i++) {
            DataDesignPoint designPoint = data.FindDesignPoint(i);
            DataDesignPoint nextDesignPoint = data.FindDesignPoint(i + 1);
            CreateStraightLine(data, designPoint, nextDesignPoint);
        }
    }
    public void CreateStraightLine(DataPlate data, DataDesignPoint designPoint, DataDesignPoint nextDesignPoint) {
        designPoint.edgePoints = new List<Vector2>();
        //方向，距离
        Vector2 direction = (nextDesignPoint.postiton - designPoint.postiton).normalized;
        float distance = Vector2.Distance(nextDesignPoint.postiton, designPoint.postiton);
        //求余，得商数
        int a = (int)(distance * 1000);
        int b = (int)(data.edgeSmooth * 1000);
        int quotient = Math.DivRem(a, b, out int remainder);
        //点位间距
        float segment = distance / quotient;
        Vector3 ap = designPoint.postiton;
        Vector3 bp = designPoint.leftBezier + designPoint.postiton;
        Vector3 cp = nextDesignPoint.rightBezier + nextDesignPoint.postiton;
        Vector3 dp = nextDesignPoint.postiton;
        for (int i = 0; i < quotient; i++) {
            float t = segment * i / distance;
            Vector2 position = ComputeBezier(ap, bp, cp, dp, t);
            designPoint.edgePoints.Add(position);
        }
        data.edgePoints.AddRange(designPoint.edgePoints);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="a">起点</param>
    /// <param name="b">起点的贝塞尔点</param>
    /// <param name="c">终点的贝塞尔点</param>
    /// <param name="d">终点</param>
    /// <param name="t">进度</param>
    /// <returns></returns>
    public Vector3 ComputeBezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t) {
        Vector3 aa = a + (b - a) * t;
        Vector3 bb = b + (c - b) * t;
        Vector3 cc = c + (d - c) * t;

        Vector3 aaa = aa + (bb - aa) * t;
        Vector3 bbb = bb + (cc - bb) * t;
        return aaa + (bbb - aaa) * t;
    }

}
