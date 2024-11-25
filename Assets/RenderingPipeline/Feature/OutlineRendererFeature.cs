using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OutlineRendererFeature : ScriptableRendererFeature {
    [Serializable]
    public class OutlineSettings {
        [Tooltip("渲染对象")] public float size = 5;
        [Tooltip("渲染对象")] public Material unlit;
        [Tooltip("轮廓材质")] public Material outline;
        [Tooltip("混合颜色")] public Material color;
        [Tooltip("渲染对象")] public List<Transform> RenderObjs = new List<Transform>();
        /// <summary> 是否有效设置 </summary>
        public bool isValid => unlit != null && outline != null && color != null;
    }
    /// <summary> 渲染设置 </summary>
    public OutlineSettings settings;
    /// <summary> 渲染Event </summary>
    public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
    /// <summary> 渲染通道 </summary>
    private OutlineRendererPass OutlineBlendRenderPass;
    public override void Create() {
        OutlineBlendRenderPass = new OutlineRendererPass(settings);
        OutlineBlendRenderPass.renderPassEvent = renderPassEvent;
    }
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) {
        OutlineBlendRenderPass.Setup(renderingData);
        renderer.EnqueuePass(OutlineBlendRenderPass);
        Dispose();
    }
    public class OutlineRendererPass : ScriptableRenderPass {
        public const string ProfilerTag = "OutlineBlend";
        /// <summary> 临时纹理 </summary>
        public RTHandle TempRTHandel;
        /// <summary> 轮廓纹理 </summary>
        public RTHandle OutlineRTHandel;
        /// <summary> 渲染设置 </summary>
        public OutlineSettings settings;
        /// <summary> 渲染通道 </summary>
        public OutlineRendererPass(OutlineSettings settings) {
            this.settings = settings;
        }
        /// <summary> 渲染前设置 </summary>
        public void Setup(in RenderingData renderingData) {
            if (!settings.isValid) { return; }
            settings.outline.SetFloat("_Size", settings.size);
            RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
            descriptor.depthBufferBits = (int)DepthBits.None;
            RenderingUtils.ReAllocateIfNeeded(ref OutlineRTHandel, descriptor, name: "OutlineRT");
            RenderingUtils.ReAllocateIfNeeded(ref TempRTHandel, descriptor, name: "TempRT");
        }
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData) {
            if (!settings.isValid) { return; }
            CommandBuffer command = CommandBufferPool.Get(ProfilerTag);
            //在临时纹理上渲染物体的轮廓
            CoreUtils.SetRenderTarget(command, TempRTHandel);
            DrawRenderer(command, settings.unlit);
            settings.outline.SetTexture("_MainTex", TempRTHandel);
            Blitter.BlitTexture(command, TempRTHandel, OutlineRTHandel, settings.outline, 0);
            //轮廓+颜色 混合到源上
            settings.color.SetTexture("_MainTex", OutlineRTHandel);
            Blit(command, ref renderingData, settings.color);
            context.ExecuteCommandBuffer(command);
            CommandBufferPool.Release(command);
            TempRTHandel.Release();
            OutlineRTHandel?.Release();
        }
        public void DrawRenderer(CommandBuffer command, Material material) {
            settings.RenderObjs.RemoveAll(obj => obj == null);
            for (int i = 0; i < settings.RenderObjs.Count; i++) {
                Transform obj = settings.RenderObjs[i];
                if (!obj.gameObject.activeInHierarchy) { continue; }
                DrawRenderer(obj, command, material);
            }
        }
        public void DrawRenderer(Transform obj, CommandBuffer command, Material material) {
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers.Length; i++) {
                command.DrawRenderer(renderers[i], material, 0, 0);
            }
            if (obj.TryGetComponent(out Renderer renderer)) {
                command.DrawRenderer(renderer, material, 0, 0);
            }
        }
    }
}
