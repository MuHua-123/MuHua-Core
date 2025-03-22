using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MuHua {
	/// <summary>
	/// 协程网络请求
	/// </summary>
	public static class WebRequest {
		/// <summary> 发送请求 </summary>
		public static IEnumerator Execute(DataRequest request) {
			if (request.RequestType == WebRequestType.GET) { yield return Get(request); }
			if (request.RequestType == WebRequestType.PostForm) { yield return PostForm(request); }
			if (request.RequestType == WebRequestType.PostJson) { yield return PostJson(request); }
			if (request.RequestType == WebRequestType.Texture) { yield return Texture(request); }
		}
		public static IEnumerator Get(DataRequest request) {
			string url = request.Url;
			using UnityWebRequest web = UnityWebRequest.Get(url);
			yield return web.SendWebRequest();
			bool isDone = web.isDone && web.result == UnityWebRequest.Result.Success;
			request.RequestResultHandle(isDone, web);
		}
		public static IEnumerator PostForm(DataRequest request) {
			string url = request.Url;
			WWWForm form = request.Form;
			using UnityWebRequest web = UnityWebRequest.Post(url, form);
			yield return web.SendWebRequest();
			bool isDone = web.isDone && web.result == UnityWebRequest.Result.Success;
			request.RequestResultHandle(isDone, web);
		}
		public static IEnumerator PostJson(DataRequest request) {
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
			yield return web.SendWebRequest();
			bool isDone = web.isDone && web.result == UnityWebRequest.Result.Success;
			request.RequestResultHandle(isDone, web);

		}
		public static IEnumerator Texture(DataRequest request) {
			string url = request.Url;
			using UnityWebRequest web = UnityWebRequestTexture.GetTexture(url);
			yield return web.SendWebRequest();
			bool isDone = web.isDone && web.result == UnityWebRequest.Result.Success;
			request.RequestResultHandle(isDone, web);
		}
	}
}
