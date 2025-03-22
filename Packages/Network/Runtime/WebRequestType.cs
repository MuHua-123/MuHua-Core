using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	/// <summary>
	/// Web请求类型
	/// </summary>
	public enum WebRequestType {
		/// <summary> GET </summary>
		GET = 0,
		/// <summary> POST 表单 </summary>
		PostForm = 1,
		/// <summary> POST Json </summary>
		PostJson = 2,
		/// <summary> GET 获取图片 </summary>
		Texture = 3
	}
}