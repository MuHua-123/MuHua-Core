using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	/// <summary>
	/// 文件名
	/// </summary>
	public class FileName {
		/// <summary> 名字 </summary>
		public string name;
		/// <summary> 文件夹 </summary>
		public string directory;
		/// <summary> 扩展名 </summary>
		public string extensions = "json";

		/// <summary> 路径 </summary>
		public string PATH => $"{directory}/{name}.{extensions}";

		public static FileName Create(string name) {
			return new FileName() { name = name, directory = SaveTool.PATH };
		}
		public static FileName Create(string name, string directory) {
			return new FileName() { name = name, directory = directory };
		}
		public static FileName Create(string name, string directory, string extensions) {
			return new FileName() { name = name, directory = directory, extensions = extensions };
		}
	}
}