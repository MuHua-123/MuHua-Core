using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MuHua {
	/// <summary>
	/// 异步网络请求
	/// </summary>
	public static class NetworkRequestAsync {
		/// <summary> 发送请求 </summary>
		public static void Execute(DataRequest request) {
			if (request.RequestType == EnumNetworkRequestType.GET) { Get(request); }
			if (request.RequestType == EnumNetworkRequestType.PostForm) { PostForm(request); }
			if (request.RequestType == EnumNetworkRequestType.PostJson) { PostJson(request); }
			if (request.RequestType == EnumNetworkRequestType.Texture) { Texture(request); }
			if (request.RequestType == EnumNetworkRequestType.Audio) { Audio(request); }
		}
		public static async void Get(DataRequest request) {
			string url = request.Url;
			using UnityWebRequest web = UnityWebRequest.Get(url);
			await web.SendWebRequest();
			bool isDone = web.isDone && web.result == UnityWebRequest.Result.Success;
			request.RequestResultHandle(isDone, web);
		}
		public static async void PostForm(DataRequest request) {
			string url = request.Url;
			WWWForm form = request.Form;
			using UnityWebRequest web = UnityWebRequest.Post(url, form);
			await web.SendWebRequest();
			bool isDone = web.isDone && web.result == UnityWebRequest.Result.Success;
			request.RequestResultHandle(isDone, web);
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
			request.RequestResultHandle(isDone, web);

		}
		public static async void Texture(DataRequest request) {
			string url = request.Url;
			using UnityWebRequest web = UnityWebRequestTexture.GetTexture(url);
			await web.SendWebRequest();
			bool isDone = web.isDone && web.result == UnityWebRequest.Result.Success;
			request.RequestResultHandle(isDone, web);
		}
		/// <summary> 下载音频 </summary>
		public static async void Audio(DataRequest request) {
			string url = request.Url;
			using UnityWebRequest web = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.UNKNOWN); // 自动检测音频类型
			await web.SendWebRequest();
			bool isDone = web.isDone && web.result == UnityWebRequest.Result.Success;
			request.RequestResultHandle(isDone, web);
		}
		public static TaskAwaiter<object> GetAwaiter(this UnityWebRequestAsyncOperation op) {
			var tcs = new TaskCompletionSource<object>();
			op.completed += (obj) => { tcs.SetResult(null); };
			return tcs.Task.GetAwaiter();
		}
	}
}