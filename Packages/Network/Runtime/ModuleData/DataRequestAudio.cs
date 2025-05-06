using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MuHua {
	public class DataRequestAudio : DataRequest {
		public readonly string url;
		public Action<string> OnError; // 错误回调
		public Action<AudioClip> OnCallback; // 成功回调

		public override string Url => url;
		public override EnumNetworkRequestType RequestType => EnumNetworkRequestType.Audio;

		public DataRequestAudio(string url, Action<AudioClip> OnCallback = null, Action<string> OnError = null) {
			this.url = url;
			this.OnCallback = OnCallback;
			this.OnError = OnError;
		}

		public override void RequestResultHandle(bool isDone, UnityWebRequest web) {
			if (!isDone) {
				OnError?.Invoke($"Audio download failed: {web.error}");
				return;
			}
			// 获取音频数据
			DownloadHandlerAudioClip downloadHandler = web.downloadHandler as DownloadHandlerAudioClip;
			if (downloadHandler == null || downloadHandler.audioClip == null) {
				OnError?.Invoke("Failed to load audio clip.");
				return;
			}
			// 获取音频长度并打印
			AudioClip audioClip = downloadHandler.audioClip;
			Debug.Log($"Audio downloaded successfully: {url}, Length: {audioClip.length} seconds");
			// 回调返回音频数据
			OnCallback?.Invoke(audioClip);
		}
	}
}
