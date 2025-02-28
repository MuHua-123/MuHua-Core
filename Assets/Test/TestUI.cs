using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

public class TestUI : MonoBehaviour
{
    /// <summary> 绑定文档 </summary>
    public UIDocument document;
    /// <summary> 根目录文档 </summary>
    public VisualElement root => document.rootVisualElement;

    private UIScrollerHorizontal scroller;
    private void Awake()
    {
        VisualElement Scroller = root.Q<VisualElement>();
        scroller = new UIScrollerHorizontal(Scroller, root);
    }
}
