using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataNetworkGetTexture : DataNetwork {
    public Action<Texture2D> action;
    public DataNetworkGetTexture(string url, Action<Texture2D> action) : base(url) => this.action = action;

    public override IEnumerator IWebRequest() {
        using (UnityWebRequest web = UnityWebRequestTexture.GetTexture(url)) {
            yield return web.SendWebRequest();
            RequestResultHandle(web);
        }
    }
    public override void RequestResultHandle(UnityWebRequest web) {
        base.RequestResultHandle(web);
        bool isDone = !web.isDone || web.result != UnityWebRequest.Result.Success;
        if (isDone) { action?.Invoke((web.downloadHandler as DownloadHandlerTexture).texture); }
    }
}
