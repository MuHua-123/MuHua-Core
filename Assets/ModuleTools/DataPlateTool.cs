using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataPlateTool {
    /// <summary> 核心模块 </summary>
    private static ModuleCore ModuleCore => ModuleCore.I;
    /// <summary> 可视化模块 </summary>
    private static ModuleVisual<DataPlate> VisualPlate => ModuleCore.VisualPlate;
    /// <summary> 多边形算法模块 </summary>
    private static ModuleAlgorithm<DataPlate> AlgorithmPolygon => ModuleCore.AlgorithmPolygon;

    public static void UpdateVisual(this DataPlate data) {
        //多边形计算
        AlgorithmPolygon.Compute(data);
        //生成可视化内容
        VisualPlate.UpdateVisual(data);
    }
}
