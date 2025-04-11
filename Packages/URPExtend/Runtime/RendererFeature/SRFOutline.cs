using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MuHua {
	/// <summary>
	/// 渲染轮廓功能
	/// </summary>
	public class SRFOutline : ScriptableRendererFeature {
		[Tooltip("轮廓大小")] public float size = 5;
		[Tooltip("辅助材质")] public Material unlit;
		[Tooltip("轮廓材质")] public Material outline;
		[Tooltip("混合材质")] public Material blend;
		/// <summary> 渲染Event </summary>
		public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;

		/// <summary> 渲染对象 </summary>
		public static List<Renderer> RenderObjs = new List<Renderer>();
		/// <summary> 是否有效 </summary>
		public bool IsValid => unlit != null && outline != null && blend != null;

		/// <summary> 渲染通道 </summary>
		private SRFOutlinePass outlinePass;
		/// <summary> 渲染设置 </summary>
		private SRFOutlineSettings settings;

		public override void Create() {
			outlinePass = new SRFOutlinePass();
			settings = new SRFOutlineSettings();
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) {
			if (!IsValid) { return; }

			RenderObjs.RemoveAll(obj => obj == null);

			settings.unlit = unlit;
			settings.outline = outline;
			settings.blend = blend;
			settings.renderObjs = RenderObjs.ToArray();
			settings.renderPassEvent = renderPassEvent;

			outlinePass.Setup(settings, renderingData);
			renderer.EnqueuePass(outlinePass);
			Dispose();
		}

		/// <summary> 添加到渲染队列 </summary>
		public static void Add(Renderer renderer, bool isClear) {
			if (isClear) { Clear(); }
			if (RenderObjs.Contains(renderer)) { RenderObjs.Add(renderer); }
		}
		/// <summary> 添加到渲染队列 </summary>
		public static void Add(Renderer[] renderers, bool isClear) {
			if (isClear) { Clear(); }
			RenderObjs.AddRange(renderers);
		}
		/// <summary> 移出渲染队列 </summary>
		public static void Remove(Renderer renderer) {
			if (RenderObjs.Contains(renderer)) { RenderObjs.Remove(renderer); }
		}
		/// <summary> 清空队列 </summary>
		public static void Clear() {
			RenderObjs?.Clear();
			RenderObjs = new List<Renderer>();
		}
	}
}