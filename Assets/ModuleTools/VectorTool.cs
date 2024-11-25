using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorTool {
    /// <summary> 向量的xyz和 </summary>
    public static float VectorSum(this Vector3 v) {
        return v.x + v.y + v.z;
    }
}
