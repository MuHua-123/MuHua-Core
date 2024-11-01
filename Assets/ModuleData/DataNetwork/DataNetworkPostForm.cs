using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataNetworkPostForm : DataNetwork {
    public readonly WWWForm form;
    public DataNetworkPostForm(string url, WWWForm form) : base(url) => this.form = form;

    public override IEnumerator IWebRequest() {
        using (UnityWebRequest web = UnityWebRequest.Post(url, form)) {
            yield return web.SendWebRequest();
            RequestResultHandle(web); 
        }
    }
}
