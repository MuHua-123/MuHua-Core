using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 散点边界算法
/// </summary>
public class UnitAlgorithmBorder : UnitAlgorithm<DataPlateBaking> {
    /// <summary> 散点边界算法 </summary>
    public UnitAlgorithmBorder() { }

    public void Compute(DataPlateBaking plateBaking) {
        //List<DataPlateSide> plateSides = plateBaking.plate.plateSides;

        //List<Vector3> points = new List<Vector3>();
        //for (int i = 0; i < plateSides.Count; i++) {
        //    points.AddRange(plateSides[i].dataBaking.positions);
        //}

        //plateBaking.border = Border(points.Distinct().ToArray());
    }

    public static DataBorder Border(Vector3[] points) {
        float minX = 0; float minY = 0;
        float maxX = 0; float maxY = 0;
        for (int i = 0; i < points.Length; i++) {
            if (points[i].x < minX) { minX = points[i].x; }
            if (points[i].x > maxX) { maxX = points[i].x; }
            if (points[i].y < minY) { minY = points[i].y; }
            if (points[i].y > maxY) { maxY = points[i].y; }
        }
        return new DataBorder(minX, maxX, minY, maxY, points);
    }
}
