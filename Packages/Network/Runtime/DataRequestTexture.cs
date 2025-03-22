using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MuHua {
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

		public override void RequestResultHandle(bool isDone, UnityWebRequest web) {
			DownloadHandler downloadHandler = web.downloadHandler;
			if (!isDone) { OnError?.Invoke(downloadHandler.text); return; }
			DownloadHandlerTexture dht = downloadHandler as DownloadHandlerTexture;
			Texture2D texture = dht.texture;
			bool compress = (texture.width % 2) == 0 && (texture.height % 2) == 0;
			if (compress) { texture.Compress(true); }
			else { Debug.LogWarning($"无法压缩的图片：{web.url}"); }
			OnCallback?.Invoke(texture);
		}
	}
}