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
		public bool isCompress;

		public override string Url => url;
		public override EnumNetworkRequestType RequestType => EnumNetworkRequestType.Texture;

		/// <summary> Web Get请求 Texture </summary>
		public DataRequestTexture(string url, Action<Texture2D> OnCallback = null, bool isCompress = true) {
			this.url = url;
			this.OnCallback = OnCallback;
			this.isCompress = isCompress;
		}

		public override void RequestResultHandle(bool isDone, UnityWebRequest web) {
			// 检查请求是否完成且没有错误
			if (!isDone) {
				OnError?.Invoke($"Texture download failed: {web.error}");
				return;
			}
			// 检查下载处理程序是否为 DownloadHandlerTexture
			DownloadHandlerTexture downloadHandler = web.downloadHandler as DownloadHandlerTexture;
			if (downloadHandler == null || downloadHandler.texture == null) {
				OnError?.Invoke("Failed to load Texture.");
				return;
			}
			// 获取纹理，并进行压缩
			Texture2D texture = downloadHandler.texture;
			if (isCompress) {
				bool compress = (texture.width % 2) == 0 && (texture.height % 2) == 0;
				if (compress) { texture.Compress(true); }
				else { Debug.LogWarning($"Unable to compress image: {web.url}"); }
			}
			// 获取纹理大小并打印
			string textureSize = GetTextureMemorySize(texture);
			Debug.Log($"Texture size: {textureSize}");
			OnCallback?.Invoke(texture);
		}

		public static string GetTextureMemorySize(Texture2D texture) {
			if (texture == null) return "0 B";

			// 获取纹理的宽度、高度和格式
			int width = texture.width;
			int height = texture.height;
			TextureFormat format = texture.format;

			// 每像素的字节数
			int bytesPerPixel = 0;
			switch (format) {
				case TextureFormat.Alpha8: bytesPerPixel = 1; break;
				case TextureFormat.RGB24: bytesPerPixel = 3; break;
				case TextureFormat.RGBA32: bytesPerPixel = 4; break;
				case TextureFormat.ARGB32: bytesPerPixel = 4; break;
				case TextureFormat.RGBAHalf: bytesPerPixel = 8; break;
				case TextureFormat.RGBAFloat: bytesPerPixel = 16; break;
				default:
					Debug.LogWarning($"Unsupported texture format: {format}");
					return "Unknown Size";
			}

			// 计算纹理占用的内存大小（字节）
			long memorySizeBytes = width * height * bytesPerPixel;

			// 转换为 KB 或 MB
			if (memorySizeBytes < 1024) {
				return $"{memorySizeBytes} B";
			}
			else if (memorySizeBytes < 1024 * 1024) {
				return $"{(memorySizeBytes / 1024f):F2} KB";
			}
			else {
				return $"{(memorySizeBytes / (1024f * 1024f)):F2} MB";
			}
		}
	}
}