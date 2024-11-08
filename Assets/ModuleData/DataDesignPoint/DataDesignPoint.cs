using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDesignPoint {
    public readonly DataPlate dataPlate;
    public DataDesignPoint(DataPlate dataPlate) { this.dataPlate = dataPlate; }

    public int index;
    public Vector2 postiton;
    public Vector2 leftBezier;//贝塞尔曲线左(逆时针+)
    public Vector2 rightBezier;//贝塞尔曲线右(顺时针-)
    public List<Vector2> edgePoints = new List<Vector2>();
}
