using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AFSubdivision : ModuleAlgorithmFunction<DataPolygon> {
    public override void Compute(DataPolygon data) {
        List<DataTriangle> triangles = new List<DataTriangle>(data.triangles);
        List<DataTriangle> subdivision = new List<DataTriangle>();
        for (int i = 0; i < triangles.Count; i++) {
            subdivision.AddRange(Subdivision(triangles[i]));
        }
        //subdivision.AddRange(Subdivision(triangles[121], data.edgeSmooth));

        data.triangles = subdivision;
    }
    private List<DataTriangle> Subdivision(DataTriangle triangle) {
        float ab = Vector3.Distance(triangle.a, triangle.b);
        float bc = Vector3.Distance(triangle.b, triangle.c);
        float ca = Vector3.Distance(triangle.c, triangle.a);
        if (ab > bc && ab > ca && ab > 0.02f) { return Subdivision(triangle.c, triangle.a, triangle.b); }
        if (bc > ab && bc > ca && bc > 0.02f) { return Subdivision(triangle.a, triangle.b, triangle.c); }
        if (ca > bc && ca > ab && ca > 0.02f) { return Subdivision(triangle.b, triangle.c, triangle.a); }

        return new List<DataTriangle> { triangle };
    }
    private List<DataTriangle> Subdivision(Vector3 a, Vector3 b, Vector3 c) {
        Vector3 direction = b - c;
        Vector3 d = c + direction * 0.5f;

        DataTriangle aT = new DataTriangle { a = a, b = d, c = c };
        DataTriangle bT = new DataTriangle { a = a, b = b, c = d };
        return new List<DataTriangle> { aT, bT, };
    }
}
