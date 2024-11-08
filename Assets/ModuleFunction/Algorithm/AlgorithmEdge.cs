using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary> 
/// 算法：中心角度排序法
/// 依据：???
/// </summary>
public class AlgorithmEdge : ModuleAlgorithm<DataPlate> {
    /// <summary> 算法：中心角度排序法 </summary>
    public AlgorithmEdge() { }

    public class EdgeAngle {
        public float angle;
        public Vector3 position;
    }

    public override void Compute(DataPlate data) {
        List<Vector2> edgePoints = data.edgePoints;
        //计算多边形中心点
        float x = edgePoints.Average((v3) => v3.x);
        float y = edgePoints.Average((v3) => v3.y);
        Vector2 center = new Vector2(x, y);
        //计算所有点的夹角
        Vector3 direction = edgePoints[0] - center;
        List<EdgeAngle> angleList = new List<EdgeAngle>();
        for (int i = 0; i < edgePoints.Count; i++) {
            Vector3 normal = edgePoints[i] - center;
            EdgeAngle edgeAngle = new EdgeAngle();
            edgeAngle.angle = Angle(direction, normal);
            edgeAngle.position = normal;
            angleList.Add(edgeAngle);
        }
        data.centerOffset = center;
        //排序
        angleList.Sort((x, y) => x.angle.CompareTo(y.angle));
        //把排序好的边缘点重新添加
        data.edgePoints = new List<Vector2>();
        for (int i = 0; i < angleList.Count; i++) {
            data.edgePoints.Add(angleList[i].position);
        }
    }
    /// <summary>
    /// 计算两点夹角
    /// </summary>
    /// <param name="direction">0度点位置</param>
    /// <param name="position">目标点</param>
    /// <returns></returns>
    private float Angle(Vector3 direction, Vector3 position) {
        float angle = Vector2.SignedAngle(direction, position);
        return angle;
    }
}
