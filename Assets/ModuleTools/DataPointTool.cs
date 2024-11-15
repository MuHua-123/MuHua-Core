using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataPointTool {
    public static Vector3 DefaultBezier(Vector3 a, Vector3 b) {
        return (b - a) * 0.3f;
    }
}
