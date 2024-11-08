using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TopMenu {
    public Action ClickTopMenu1;
    public Action ClickTopMenu2;
    public readonly VisualElement element;
    public VisualElement TopMenu1 => element.Q<VisualElement>("TopMenu1");
    public VisualElement TopMenu2 => element.Q<VisualElement>("TopMenu2");
    public TopMenu(VisualElement element) {
        this.element = element;
        TopMenu1.RegisterCallback<ClickEvent>(TopMenu1_ClickEvent);
        TopMenu2.RegisterCallback<ClickEvent>(TopMenu2_ClickEvent);
    }
    private void TopMenu1_ClickEvent(ClickEvent evt) {
        ClickTopMenu1?.Invoke();
    }
    private void TopMenu2_ClickEvent(ClickEvent evt) {
        ClickTopMenu2?.Invoke();
    }
}
