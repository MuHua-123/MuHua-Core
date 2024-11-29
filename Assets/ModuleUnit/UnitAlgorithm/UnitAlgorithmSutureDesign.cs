using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 设计的缝合线
/// </summary>
public class UnitAlgorithmSutureDesign : UnitAlgorithm<DataSutureSide> {
    /// <summary> 设计的缝合线 </summary>
    public UnitAlgorithmSutureDesign() { }

    public class Line {
        public float origin;
        public Vector3 a;
        public Vector3 b;
        public Line(float origin, Vector3 a, Vector3 b) {
            this.origin = origin;
            this.a = a;
            this.b = b;
        }
        public float Distance => Vector3.Distance(a, b);
    }

    public void Compute(DataSutureSide sutureSide) {
        float maxLength = sutureSide.suture.length;
        Vector3 platePosition = sutureSide.plateSide.plate.dataDesign.position;
        List<Vector3> sidePositions = new List<Vector3>(sutureSide.plateSide.dataDesign.positions);

        float length = 0;
        List<Line> lines = new List<Line>();
        if (sutureSide.isReversal) { sidePositions.Reverse(); }
        for (int i = 0; i < sidePositions.Count - 1; i++) {
            Line line = new Line(length, sidePositions[i], sidePositions[i + 1]);
            lines.Add(line);
            length += line.Distance;
        }
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < lines.Count; i++) {
            Line line = lines[i];
            if (line.origin < maxLength) { positions.Add(line.a + platePosition); }
            float nextDistance = line.origin + line.Distance;
            if (nextDistance < maxLength) { continue; }
            if (nextDistance == maxLength) { positions.Add(line.b + platePosition); break; }
            float distance = maxLength - line.origin;
            Vector3 direction = (line.b - line.a).normalized;
            Vector3 position = line.a + direction * distance;
            positions.Add(position + platePosition);
            break;
        }
        sutureSide.dataDesign.positions = positions.ToArray();
    }
}
