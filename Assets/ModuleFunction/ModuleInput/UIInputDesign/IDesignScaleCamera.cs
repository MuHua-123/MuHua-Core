using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDesignScaleCamera : UIInputDesignUnit {
    public readonly float Min = 0.1f;
    public readonly float Max = 4f;
    public override void ScrollWheel(DataUIMouseInput data) {
        float size = ViewCamera.scale + data.ScrollWheel;
        size = Mathf.Clamp(size, Min, Max);
        ViewCamera.scale = Mathf.Lerp(ViewCamera.scale, size, Time.deltaTime * 20);
    }
}
