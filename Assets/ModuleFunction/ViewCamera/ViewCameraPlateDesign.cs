using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCameraPlateDesign : ViewCamera {
    protected override void Awake() {
        ModuleCore.PlateDesignViewCamera = this;
    }
}
