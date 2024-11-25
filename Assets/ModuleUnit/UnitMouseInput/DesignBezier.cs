using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignBezier : UnitMouseInput {
    /// <summary> 设计视图相机模块 </summary>
    public ModuleViewCamera ViewCamera => ModuleCore.ViewCameraDesign;
    /// <summary> 查询点算法模块 </summary>
    public ModuleFind<DataSide> FindSide => ModuleCore.FindSide;

    public override void MouseDown(DataMouseInput data) {
        if (!FindSide.Find(data.WorldPosition, out DataSide side)) { return; }
        if (side.bezier == Bezier.一阶) { side.TwoRankBezier(); side.plate.UpdateVisual(); return; }
        if (side.bezier == Bezier.二阶) { side.ThreeRankBezier(); side.plate.UpdateVisual(); return; }
        if (side.bezier == Bezier.三阶) { side.OneRankBezier(); side.plate.UpdateVisual(); return; }
    }
}
