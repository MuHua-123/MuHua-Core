using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// 轮廓渲染资源模块
/// </summary>
public class AssetsOutline : ModuleAssets<Transform> {
    public UniversalRendererData rendererData;
    private OutlineRendererFeature rendererFeature;

    public override int Count => rendererFeature.settings.RenderObjs.Count;
    public override List<Transform> Datas => rendererFeature.settings.RenderObjs;

    protected override void Awake() {
        ModuleCore.AssetsOutline = this;
        rendererFeature = rendererData.rendererFeatures.OfType<OutlineRendererFeature>().FirstOrDefault();
    }

    public override void Add(Transform data) {
        if (Datas.Contains(data)) { return; }
        Datas.Add(data);
    }
    public override void Remove(Transform data) {
        if (!Datas.Contains(data)) { return; }
        Datas.Remove(data);
    }
    public override Transform Find(int index) {
        return Datas.LoopIndex(index);
    }
    public override void ForEach(Action<Transform> action) {
        Datas.ForEach(action);
    }
}
