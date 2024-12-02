using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// 独立模块 安排点
/// </summary>
public class SingleArrangePoint : ModuleSingle<DataMouseInput> {
    public UniversalRendererData rendererData;
    private OutlineRendererFeature rendererFeature;
    private ModulePrefab<DataPlate> platePrefab;

    /// <summary> 安排点图层遮罩 </summary>
    private LayerMask Arrange => LayerMaskTool.Arrange;
    /// <summary> 烘焙视图相机模块 </summary>
    public ModuleViewCamera ViewCamera => ModuleCore.ViewCameraBaking;
    /// <summary> 设计UI输入模块 </summary>
    public ModuleUIInput<UnitMouseInput> UIInputBaking => ModuleCore.I.UIInputBaking;

    protected override void Awake() => ModuleCore.SingleArrangePoint = this;

    private void Start() {
        UIInputBaking.OnChangeInput += UIInputBaking_OnChangeInput; ;
        rendererFeature = rendererData.rendererFeatures.OfType<OutlineRendererFeature>().FirstOrDefault();
    }
    private void OnDestroy() {
        if (UIInputBaking == null) { return; }
        UIInputBaking.OnChangeInput -= UIInputBaking_OnChangeInput;
    }

    private void UIInputBaking_OnChangeInput(UnitMouseInput input) {
        Close();
    }
    public override void Open(DataMouseInput data) {
        UpdateArrangePoint(data.ScreenPosition);
        if (!ViewCamera.ScreenToWorldObject(data.ScreenPosition, out ModulePrefab<DataPlate> temp)) { return; }
        if (platePrefab) { Remove(platePrefab.transform); }
        platePrefab = temp;
        Add(platePrefab.transform);
        ModuleCore.BakingMobilePlate(platePrefab.Value);
    }
    public override void Complete() {

    }
    public override void Close() {
        if (platePrefab) { Remove(platePrefab.transform); }
        platePrefab = null;
        ModuleCore.BakingMobilePlate(null);
    }

    public void Add(Transform data) {
        if (rendererFeature.settings.RenderObjs.Contains(data)) { return; }
        rendererFeature.settings.RenderObjs.Add(data);
    }
    public void Remove(Transform data) {
        if (!rendererFeature.settings.RenderObjs.Contains(data)) { return; }
        rendererFeature.settings.RenderObjs.Remove(data);
    }
    private void UpdateArrangePoint(Vector3 screenPosition) {
        bool isArrange = ViewCamera.ScreenToWorldObjectParent(screenPosition, out FixedArrange arrange, Arrange);
        if (platePrefab != null && isArrange) {
            platePrefab.Value.arrange = arrange;
            platePrefab.Value.dataBaking.position = arrange.transform.localPosition;
            platePrefab.Value.dataBaking.eulerAngles = arrange.transform.localEulerAngles;
            platePrefab.Value.UpdateVisual(false);
        }
    }
}
