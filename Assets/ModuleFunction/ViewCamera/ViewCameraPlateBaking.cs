using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCameraPlateBaking : ViewCamera {
    protected override void Awake() {
        ModuleCore.PlateBakingViewCamera = this;
    }
}
