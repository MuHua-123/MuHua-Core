using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// 开关
	/// </summary>
	public class UIToggle : ModuleUIPanel {
		/// <summary> 值改变时 </summary>
		public event Action<bool> ValueChanged;

		public bool value;// 当前值

		/// <summary> 标题 </summary>
		public string title {
			get => Title.text;
			set => Title.text = value;
		}

		public Label Title => Q<Label>("Title");
		public VisualElement Input => Q<VisualElement>("Input");
		public VisualElement Check => Q<VisualElement>("Check");

		public UIToggle(VisualElement element) : base(element) {
			Input.RegisterCallback<ClickEvent>(evt => UpdateValue(!value));
		}
		/// <summary> 更新值 </summary>
		public void UpdateValue(bool value, bool send = true) {
			this.value = value;
			Check.EnableInClassList("toggle-check-hide", !value);
			if (send) { ValueChanged?.Invoke(value); }
		}
	}
}
