using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 散点边界算法
/// </summary>
public class UnitAlgorithmBorder : UnitAlgorithm<DataPlate> {
    /// <summary> 散点边界算法 </summary>
    public UnitAlgorithmBorder() { }

    public void Compute(DataPlate plate) {
        List<Vector3> edgePoints = new List<Vector3>(plate.edgePoints);
        plate.border = Border(edgePoints);
    }

    public static DataBorder Border(List<Vector3> points) {
        float minX = 0; float minY = 0;
        float maxX = 0; float maxY = 0;
        for (int i = 0; i < points.Count; i++) {
            if (points[i].x < minX) { minX = points[i].x; }
            if (points[i].x > maxX) { maxX = points[i].x; }
            if (points[i].y < minY) { minY = points[i].y; }
            if (points[i].y > maxY) { maxY = points[i].y; }
        }
        return new DataBorder(minX, maxX, minY, maxY);
    }
}
