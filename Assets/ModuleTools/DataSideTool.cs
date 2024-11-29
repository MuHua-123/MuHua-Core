using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataSideTool {
    public static void SetBezierPositionA(this DataPlateSide side, Vector3 position) {
        if (side.bezier == Bezier.一阶) { return; }
        if (side.bezier == Bezier.二阶) { side.bBezier = position; }
        side.aBezier = position;
    }
    public static void SetBezierPositionB(this DataPlateSide side, Vector3 position) {
        if (side.bezier == Bezier.一阶) { return; }
        if (side.bezier == Bezier.二阶) { side.aBezier = position; }
        side.bBezier = position;
    }
    public static void OneRankBezier(this DataPlateSide side) {
        side.bezier = Bezier.一阶;
    }
    public static void TwoRankBezier(this DataPlateSide side) {
        side.bezier = Bezier.二阶;
        DataPlatePoint a = side.aPoint;
        DataPlatePoint b = side.bPoint;
        side.aBezier = a.position + (b.position - a.position) * 0.5f;
        side.bBezier = a.position + (b.position - a.position) * 0.5f;
    }
    public static void ThreeRankBezier(this DataPlateSide side) {
        side.bezier = Bezier.三阶;
        DataPlatePoint a = side.aPoint;
        DataPlatePoint b = side.bPoint;
        side.aBezier = a.position + (b.position - a.position) * 0.3f;
        side.bBezier = a.position + (b.position - a.position) * 0.7f;
    }
}
