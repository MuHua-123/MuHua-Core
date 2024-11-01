using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class DataNetwork {
    public event Action<string> OnError;
    public event Action<string> OnCallback;

    public readonly string url;
    public DataNetwork(string url) => this.url = url;

    public abstract IEnumerator IWebRequest();

    public virtual void RequestResultHandle(UnityWebRequest web) {
        bool isDone = !web.isDone || web.result != UnityWebRequest.Result.Success;
        if (isDone) { OnCallback?.Invoke(web.downloadHandler.text); }
        else { OnError?.Invoke(web.downloadHandler.text); }
    }
}
