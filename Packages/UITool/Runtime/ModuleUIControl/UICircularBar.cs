using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class CircularBar : VisualElement {
	public new class UxmlFactory : UxmlFactory<CircularBar, UxmlTraits> { }
	public new class UxmlTraits : VisualElement.UxmlTraits {
		private readonly UxmlFloatAttributeDescription Fill = new() { name = "fill", defaultValue = 0.1f };

		public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
			base.Init(ve, bag, cc);
			var circularBar = ve as CircularBar;
			circularBar.Fill = Fill.GetValueFromBag(bag, cc);
			circularBar.UpdateValue();
		}
	}

	public readonly Material material;
	public readonly RenderTexture rt;

	public VisualElement visual;

	public float Fill { get; set; }

	public CircularBar() {
		visual = new VisualElement();
		visual.style.flexGrow = 1;
		hierarchy.Add(visual);

		var shader = Shader.Find("MuHua/UI/CircularBar");
		if (shader == null) {
			Debug.LogError("Failed to find Shader Graphs/CircularBar.");
			return;
		}
		material = new Material(shader);

		rt = new RenderTexture(128, 128, 0, RenderTextureFormat.ARGBFloat);

		Background background = Background.FromRenderTexture(rt);
		visual.style.backgroundImage = new StyleBackground(background);
	}

	public void UpdateValue(float fill) {
		Fill = fill;
		UpdateValue();
	}
	public void UpdateValue() {
		if (material == null) { return; }
		Texture2D texture = resolvedStyle.backgroundImage.texture;
		if (texture == null) { return; }

		material.SetFloat("_fill", Fill);

		CommandBuffer command = CommandBufferPool.Get();

		// 设置渲染目标为tempRTHandle
		CoreUtils.SetRenderTarget(command, rt);
		// 清除纹理内容
		CoreUtils.ClearRenderTarget(command, ClearFlag.All, Color.clear);
		// 渲染
		command.Blit(texture, rt, material, 0);

		Graphics.ExecuteCommandBuffer(command);
		CommandBufferPool.Release(command);

		Background background = Background.FromRenderTexture(rt);
		visual.style.backgroundImage = new StyleBackground(background);
	}
}
