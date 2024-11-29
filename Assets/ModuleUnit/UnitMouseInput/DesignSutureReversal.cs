using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignSutureReversal : UnitMouseInput {
    /// <summary> 设计视图相机模块 </summary>
    public ModuleViewCamera ViewCamera => ModuleCore.ViewCameraDesign;
    /// <summary> 查询点算法模块 </summary>
    public ModuleFind<DataPlateSide> FindSide => ModuleCore.FindSide;

    public override void MouseDown(DataMouseInput data) {
        if (!FindSide.Find(data.WorldPosition, out DataPlateSide side)) { return; }
        if (side.suture.a.plateSide == side) {
            side.suture.a.isReversal = !side.suture.a.isReversal;
        }
        if (side.suture.b.plateSide == side) {
            side.suture.b.isReversal = !side.suture.b.isReversal;
        }
        side.plate.UpdateVisual();
    }
}
