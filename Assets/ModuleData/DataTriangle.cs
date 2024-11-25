using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DataTriangle {
    public Vector3 a;
    public Vector3 b;
    public Vector3 c;
    public override string ToString() {
        return $"{a} , {b} , {c}";
    }
}
