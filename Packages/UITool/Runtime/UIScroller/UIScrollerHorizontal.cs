using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua
{
    /// <summary>
    /// 滑块(水平)
    /// </summary>
    public class UIScrollerHorizontal
    {
        /// <summary> 绑定的元素 </summary>
        public readonly VisualElement element;
        /// <summary> 绑定的画布 </summary>
        public readonly VisualElement canvas;
        /// <summary> 值改变时 </summary>
        public event Action<float> ValueChanged;

        public float value;
        public bool isDragger;
        public float originalPosition;
        public float pointerPosition;

        public VisualElement dragger => element.Q<VisualElement>("Dragger");

        public UIScrollerHorizontal(VisualElement element, VisualElement canvas)
        {
            this.element = element;
            this.canvas = canvas;

            //设置事件
            dragger.RegisterCallback<PointerDownEvent>(DraggerDown);
            element.RegisterCallback<PointerDownEvent>(ElementDown);

            canvas.RegisterCallback<PointerUpEvent>((evt) => isDragger = false);
            canvas.RegisterCallback<PointerLeaveEvent>((evt) => isDragger = false);
        }

        public void DraggerDown(PointerDownEvent evt)
        {
            isDragger = true;
            originalPosition = dragger.transform.position.x;
            pointerPosition = UITool.GetMousePosition().x;
        }
        public void ElementDown(PointerDownEvent evt)
        {
            float offset = evt.localPosition.x - dragger.resolvedStyle.width * 0.5f;
            float max = element.resolvedStyle.width - dragger.resolvedStyle.width;
            float value = Mathf.InverseLerp(0, max, offset);
            UpdateValue(value);
        }

        /// <summary> 更新状态 </summary>
        public virtual void Update()
        {
            if (!isDragger) { return; }
            float differ = UITool.GetMousePosition().x - pointerPosition;
            float offset = differ + originalPosition;
            float max = element.resolvedStyle.width - dragger.resolvedStyle.width;
            float value = Mathf.InverseLerp(0, max, offset);
            UpdateValue(value);
        }

        /// <summary> 更新值(0-1) </summary>
        public void UpdateValue(float value)
        {
            this.value = value;
            ValueChanged?.Invoke(value);
            float max = element.resolvedStyle.width - dragger.resolvedStyle.width;
            float x = Mathf.Lerp(0, max, value);
            dragger.transform.position = new Vector3(x, 0);
        }
    }
}
