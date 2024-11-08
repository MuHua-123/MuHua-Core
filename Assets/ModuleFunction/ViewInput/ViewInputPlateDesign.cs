using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewInputPlateDesign : ViewInput {
    protected override void Awake() {
        ModuleCore.PlateDesignViewInput = this;
    }
    private void Start() {
        //leftViewInputUnit = new VIUEdgePointMobile(ModuleCore.PlateDesignViewCamera);
        //leftViewInputUnit = new VIUEdgePointAdd(ModuleCore.PlateDesignViewCamera);
        rightViewInputUnit = new VIUCameraMobile(ModuleCore.PlateDesignViewCamera);
        //rightViewInputUnit = new VIUEdgePointMobile(ModuleCore.PlateDesignViewCamera);
        scrollViewInputUnit = new VIUCameraScale(ModuleCore.PlateDesignViewCamera, VIUCameraScale.ScaleType.Orthographic);

        //Debug.Log(leftViewInputUnit.GetType());
    }
}
