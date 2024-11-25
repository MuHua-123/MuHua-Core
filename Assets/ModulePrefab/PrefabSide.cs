using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSide : ModulePrefab<DataSide> {
    public Transform aPoint;
    public Transform bPoint;
    public LineRenderer lineRenderer;
    public LineRenderer aBezier;
    public LineRenderer bBezier;
    private DataSide side;

    #region 引用模块
    /// <summary> 设计UI输入模块 </summary>
    public ModuleUIInput<UnitMouseInput> UIInputDesign => ModuleCore.I.UIInputDesign;
    #endregion

    public override DataSide Value => side;

    private void Awake() {
        UIInputDesign.OnChangeInput += UIInputDesign_OnChangeInput;
    }
    private void OnDestroy() {
        if (UIInputDesign == null) { return; }
        UIInputDesign.OnChangeInput -= UIInputDesign_OnChangeInput;
    }

    private void UIInputDesign_OnChangeInput(UnitMouseInput obj) {
        Type type = UIInputDesign.Current.GetType();
        if (type == typeof(DesignBezier)) { UpdateVisual(side); return; }
        aPoint.gameObject.SetActive(false);
        bPoint.gameObject.SetActive(false);
        aBezier.gameObject.SetActive(false);
        bBezier.gameObject.SetActive(false);
    }
    public override void UpdateVisual(DataSide side) {
        this.side = side;

        lineRenderer.positionCount = side.positions.Length;
        lineRenderer.SetPositions(side.positions);

        Type type = UIInputDesign.Current.GetType();
        if (type != typeof(DesignBezier)) { ActiveGameObject(false); return; }
        if (side.bezier == Bezier.一阶) { ActiveGameObject(false); return; }
        if (side.bezier == Bezier.二阶) { ActiveGameObject(true); }
        if (side.bezier == Bezier.三阶) { ActiveGameObject(true); }

        aPoint.localPosition = side.aBezier;
        bPoint.localPosition = side.bBezier;
        aBezier.SetPosition(0, side.aPoint.position);
        aBezier.SetPosition(1, side.aBezier);
        bBezier.SetPosition(0, side.bPoint.position);
        bBezier.SetPosition(1, side.bBezier);
    }
    private void ActiveGameObject(bool isActive) {
        aPoint.gameObject.SetActive(isActive);
        bPoint.gameObject.SetActive(isActive);
        aBezier.gameObject.SetActive(isActive);
        bBezier.gameObject.SetActive(isActive);
    }

}
