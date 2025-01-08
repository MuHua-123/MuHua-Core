using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGlobalPage : ModuleUIPage {
    protected override void Awake() => ModuleCore.GlobalPage = this;

    private void Start() {
        string url = "https://neiyihuizhouilabtest.zgfzjy.cn/api/client/color/categroies";
        DataRequestGet request = new DataRequestGet(url);
        request.OnCallback = (obj) => { Debug.Log(obj); };
        WebRequest.Send(request);
    }
}
