using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBezier {
    /// <summary> 是否是a </summary>
    public bool isA;
    /// <summary> 关联的边 </summary>
    public DataSide side;
    /// <summary> 位置 </summary>
    public Vector3 position => isA ? side.aBezier : side.bBezier;

    public void SetBezierPosition(Vector3 value) {
        if (isA) { side.SetBezierPositionA(value); }
        else { side.SetBezierPositionB(value); }
        side.plate.UpdateVisual();
    }
}
