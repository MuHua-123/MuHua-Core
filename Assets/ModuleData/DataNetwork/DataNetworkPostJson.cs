using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataNetworkPostJson : DataNetwork {
    public readonly string json;
    public DataNetworkPostJson(string url, string json) : base(url) => this.json = json;

    public override IEnumerator IWebRequest() {
        byte[] postBytes = System.Text.Encoding.Default.GetBytes(json);
#if UNITY_2022
        using (UnityWebRequest web = UnityWebRequest.PostWwwForm(url, "POST")) {
#else
        using (UnityWebRequest web = UnityWebRequest.Post(url, "POST")) {
#endif
            web.uploadHandler.Dispose();
            web.uploadHandler = new UploadHandlerRaw(postBytes);
            web.SetRequestHeader("Content-Type", "application/json");
            yield return web.SendWebRequest();
            RequestResultHandle(web);
        }
    }
}
