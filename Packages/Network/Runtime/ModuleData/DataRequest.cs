using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MuHua {
	/// <summary>
	/// 请求数据
	/// </summary>
	public abstract class DataRequest {
		/// <summary> Web请求地址 </summary>
		public abstract string Url { get; }
		/// <summary> Web请求类型 </summary>
		public abstract EnumNetworkRequestType RequestType { get; }
		/// <summary> 提交json数据 </summary>
		public virtual string Json { get; }
		/// <summary> 提交Form表单数据 </summary>
		public virtual WWWForm Form { get; }

		/// <summary> Web请求结果处理 </summary>
		public abstract void RequestResultHandle(bool isDone, UnityWebRequest web);

		/// <summary> 发送请求 </summary>
		public virtual IEnumerator Send() => NetworkRequest.Execute(this);
		/// <summary> 发送请求（异步） </summary>
		public virtual void SendAsync() => NetworkRequestAsync.Execute(this);
	}
}