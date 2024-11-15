using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDesignBezier : UIInputDesignUnit {
    /// <summary> 板片资产 </summary>
    public ModuleAssets<DataPlate> AssetsPlate => ModuleCore.AssetsPlate;
    /// <summary> 查询点算法模块 </summary>
    public ModuleAlgorithm<DataFindBezier> AlgorithmFindBezier => ModuleCore.AlgorithmFindBezier;

    private Vector3 mousePosition;
    private Vector3 originalPosition;
    private DataFindBezier findBezier;
    private void FindPoint(Vector3 localPosition) {
        findBezier = new DataFindBezier();
        findBezier.position = localPosition;
        findBezier.datas = AssetsPlate.Datas;
        AlgorithmFindBezier.Compute(findBezier);
    }

    public override void MouseDown(DataUIMouseInput data) {
        FindPoint(data.WorldPosition);
        if (!findBezier.IsValid) { return; }
        mousePosition = data.ScreenPosition;
        originalPosition = findBezier.isFront ? findBezier.point.frontBezier : findBezier.point.afterBezier;
    }
    public override void MouseDrag(DataUIMouseInput data) {
        if (!findBezier.IsValid) { return; }
        Vector3 original = ViewCamera.ScreenToWorldPosition(mousePosition);
        Vector3 current = data.WorldPosition;
        Vector3 offset = current - original;
        if (findBezier.isFront) { findBezier.point.frontBezier = originalPosition + offset; }
        else { findBezier.point.afterBezier = originalPosition + offset; }
        findBezier.plate.UpdateVisual();
    }
}
