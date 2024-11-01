using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataNetworkGet : DataNetwork {
    public DataNetworkGet(string url) : base(url) { }

    public override IEnumerator IWebRequest() {
        using (UnityWebRequest web = UnityWebRequest.Get(url)) {
            yield return web.SendWebRequest();
            RequestResultHandle(web); 
        }
    }
}
