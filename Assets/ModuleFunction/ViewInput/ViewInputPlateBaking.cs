using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewInputPlateBaking : ViewInput {
    protected override void Awake() {
        ModuleCore.PlateBakingViewInput = this;
    }
    private void Start() {
        leftViewInputUnit = new VIUCameraMobile(ModuleCore.PlateBakingViewCamera);
        rightViewInputUnit = new VIUCameraRotate(ModuleCore.PlateBakingViewCamera);
        scrollViewInputUnit = new VIUCameraScale(ModuleCore.PlateBakingViewCamera);
    }
}
