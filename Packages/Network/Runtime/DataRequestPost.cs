using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MuHua {
	/// <summary>
	/// Post请求数据
	/// </summary>
	public class DataRequestPost : DataRequest {
		public readonly string url;
		public readonly string json;
		public readonly WWWForm form;
		public readonly AsyncWebRequestType type;

		public Action<string> OnError;
		public Action<string> OnCallback;

		public override string Url => url;
		public override AsyncWebRequestType RequestType => type;
		public override string Json => json;
		public override WWWForm Form => form;

		/// <summary> Web Post请求 提交json数据 </summary>
		public DataRequestPost(string url, string json, Action<string> OnCallback = null) {
			this.url = url;
			this.json = json;
			this.OnCallback = OnCallback;
			type = AsyncWebRequestType.PostJson;
		}
		/// <summary> Web Post请求 提交WWWForm数据 </summary>
		public DataRequestPost(string url, WWWForm form, Action<string> OnCallback = null) {
			this.url = url;
			this.form = form;
			this.OnCallback = OnCallback;
			type = AsyncWebRequestType.PostForm;
		}

		public override void RequestResultHandle(bool isDone, UnityWebRequest web) {
			DownloadHandler downloadHandler = web.downloadHandler;
			if (!isDone) { OnError?.Invoke(downloadHandler.text); return; }
			OnCallback?.Invoke(downloadHandler.text);
		}
	}
}