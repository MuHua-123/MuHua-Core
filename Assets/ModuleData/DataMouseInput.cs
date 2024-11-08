using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DataMouseInput {
    public Vector3 ScreenPosition;
    public float ScrollWheel;
    public DataMouseInput(MouseDownEvent evt) {
        ScreenPosition = evt.localMousePosition;
    }
    public DataMouseInput(MouseMoveEvent evt) {
        ScreenPosition = evt.localMousePosition;
    }
    public DataMouseInput(MouseUpEvent evt) {
        ScreenPosition = evt.localMousePosition;
    }
    public DataMouseInput(MouseOutEvent evt) {
        ScreenPosition = evt.localMousePosition;
    }
    public DataMouseInput(WheelEvent evt) {
        ScreenPosition = evt.localMousePosition;
        ScrollWheel = evt.delta.y;
    }
}
