using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

/// <summary>
/// Web请求执行模块
/// </summary>
public class ExecuteWebRequest : ModuleExecute<DataRequest> {
    /// <summary> 发送请求 </summary>
    public void Execute(DataRequest request) {
        if (request.RequestType == WebRequestType.GET) { Get(request); }
        if (request.RequestType == WebRequestType.PostForm) { PostForm(request); }
        if (request.RequestType == WebRequestType.PostJson) { PostJson(request); }
        if (request.RequestType == WebRequestType.Texture) { Texture(request); }
    }
    public static async void Get(DataRequest request) {
        string url = request.Url;
        using UnityWebRequest web = UnityWebRequest.Get(url);
        await web.SendWebRequest();
        bool isDone = web.isDone && web.result == UnityWebRequest.Result.Success;
        request.RequestResultHandle(isDone, web.downloadHandler);
    }
    public static async void PostForm(DataRequest request) {
        string url = request.Url;
        WWWForm form = request.Form;
        using UnityWebRequest web = UnityWebRequest.Post(url, form);
        await web.SendWebRequest();
        bool isDone = web.isDone && web.result == UnityWebRequest.Result.Success;
        request.RequestResultHandle(isDone, web.downloadHandler);
    }
    public static async void PostJson(DataRequest request) {
        string url = request.Url;
        string json = request.Json;
        byte[] postBytes = System.Text.Encoding.Default.GetBytes(json);
#if UNITY_2022
        using UnityWebRequest web = UnityWebRequest.PostWwwForm(url, "POST");
#else
        using UnityWebRequest web = UnityWebRequest.Post(url, "POST");
#endif
        web.uploadHandler.Dispose();
        web.uploadHandler = new UploadHandlerRaw(postBytes);
        web.SetRequestHeader("Content-Type", "application/json");
        await web.SendWebRequest();
        bool isDone = web.isDone && web.result == UnityWebRequest.Result.Success;
        request.RequestResultHandle(isDone, web.downloadHandler);

    }
    public static async void Texture(DataRequest request) {
        string url = request.Url;
        using UnityWebRequest web = UnityWebRequestTexture.GetTexture(url);
        await web.SendWebRequest();
        bool isDone = web.isDone && web.result == UnityWebRequest.Result.Success;
        request.RequestResultHandle(isDone, web.downloadHandler);
    }
}
/// <summary>
/// Web请求执行模块工具
/// </summary>
public static class ExecuteWebRequestTool {
    public static TaskAwaiter<object> GetAwaiter(this UnityWebRequestAsyncOperation op) {
        var tcs = new TaskCompletionSource<object>();
        op.completed += (obj) => { tcs.SetResult(null); };
        return tcs.Task.GetAwaiter();
    }
}
/// <summary>
/// Web请求类型
/// </summary>
public enum WebRequestType {
    /// <summary> GET </summary>
    GET = 0,
    /// <summary> POST 表单 </summary>
    PostForm = 1,
    /// <summary> POST Json </summary>
    PostJson = 2,
    /// <summary> GET 获取图片 </summary>
    Texture = 3
}
/// <summary>
/// 请求数据
/// </summary>
public abstract class DataRequest {
    /// <summary> Web请求地址 </summary>
    public abstract string Url { get; }
    /// <summary> Web请求类型 </summary>
    public abstract WebRequestType RequestType { get; }
    /// <summary> 提交json数据 </summary>
    public virtual string Json { get; }
    /// <summary> 提交Form表单数据 </summary>
    public virtual WWWForm Form { get; }

    /// <summary> Web请求结果处理 </summary>
    public abstract void RequestResultHandle(bool isDone, DownloadHandler downloadHandler);
}
/// <summary>
/// Get请求数据
/// </summary>
public class DataRequestGet : DataRequest {
    public readonly string url;

    public Action<string> OnError;
    public Action<string> OnCallback;

    public override string Url => url;
    public override WebRequestType RequestType => WebRequestType.GET;

    /// <summary> Web Get请求数据 </summary>
    public DataRequestGet(string url, Action<string> OnCallback = null) {
        this.url = url;
        this.OnCallback = OnCallback;
    }

    public override void RequestResultHandle(bool isDone, DownloadHandler downloadHandler) {
        if (!isDone) { OnError?.Invoke(downloadHandler.text); return; }
        OnCallback?.Invoke(downloadHandler.text);
    }
}
/// <summary>
/// Post请求数据
/// </summary>
public class DataRequestPost : DataRequest {
    public readonly string url;
    public readonly string json;
    public readonly WWWForm form;
    public readonly WebRequestType type;

    public Action<string> OnError;
    public Action<string> OnCallback;

    public override string Url => url;
    public override WebRequestType RequestType => type;
    public override string Json => json;
    public override WWWForm Form => form;

    /// <summary> Web Post请求 提交json数据 </summary>
    public DataRequestPost(string url, string json, Action<string> OnCallback = null) {
        this.url = url;
        this.json = json;
        this.OnCallback = OnCallback;
        type = WebRequestType.PostJson;
    }
    /// <summary> Web Post请求 提交WWWForm数据 </summary>
    public DataRequestPost(string url, WWWForm form, Action<string> OnCallback = null) {
        this.url = url;
        this.form = form;
        this.OnCallback = OnCallback;
        type = WebRequestType.PostForm;
    }

    public override void RequestResultHandle(bool isDone, DownloadHandler downloadHandler) {
        if (!isDone) { OnError?.Invoke(downloadHandler.text); return; }
        OnCallback?.Invoke(downloadHandler.text);
    }
}
/// <summary>
/// Get下载 Texture
/// </summary>
public class DataRequestTexture : DataRequest {
    public readonly string url;

    public Action<string> OnError;
    public Action<Texture2D> OnCallback;

    public override string Url => url;
    public override WebRequestType RequestType => WebRequestType.Texture;

    /// <summary> Web Get请求 Texture </summary>
    public DataRequestTexture(string url, Action<Texture2D> OnCallback = null) {
        this.url = url;
        this.OnCallback = OnCallback;
    }

    public override void RequestResultHandle(bool isDone, DownloadHandler downloadHandler) {
        if (!isDone) { OnError?.Invoke(downloadHandler.text); return; }
        DownloadHandlerTexture dht = downloadHandler as DownloadHandlerTexture;
        OnCallback?.Invoke(dht.texture);
    }
}