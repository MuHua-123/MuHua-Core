using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabBezierPoint : MonoBehaviour {
    public LineRenderer bezierLine;
    private Vector2 position;
    private DataDesignPoint value;
    private Action<Vector2> callback;
    public DataPlate DataPlate => value.dataPlate;
    public Vector2 Position => position + value.postiton;
    public void SetValue(DataDesignPoint value, Action<Vector2> callback) {
        this.value = value;
        this.callback = callback;
    }
    public void SetPosition(Vector2 position) {
        this.position = position;
        float lx = position.x * 50;
        float ly = position.y * 50;
        transform.localPosition = new Vector3(lx, ly, transform.localPosition.z);
        bezierLine.SetPosition(1, position);
    }
    public void Change(Vector2 localPosition) {
        Vector2 position = localPosition - value.postiton;
        SetPosition(position);
        callback?.Invoke(position);
    }
}
