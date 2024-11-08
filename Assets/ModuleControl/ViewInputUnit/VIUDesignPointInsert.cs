using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIUDesignPointInsert : ModuleViewInputUnit {
    private readonly ModuleViewCamera viewCamera;
    private ModulePlateDesign PlateDesign => ModuleCore.I.PlateDesign;
    public VIUDesignPointInsert(ModuleViewCamera viewCamera) {
        this.viewCamera = viewCamera;
    }
    public override void DownMouse(DataMouseInput data) {
        PlateDesign.InsertDesignPoint(data.ScreenPosition);
    }
}
