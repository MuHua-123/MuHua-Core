using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MuHua {
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

		public override void RequestResultHandle(bool isDone, UnityWebRequest web) {
			DownloadHandler downloadHandler = web.downloadHandler;
			if (!isDone) { OnError?.Invoke(downloadHandler.text); return; }
			OnCallback?.Invoke(downloadHandler.text);
		}
	}
}
