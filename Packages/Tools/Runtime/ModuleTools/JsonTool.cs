using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MuHua {
    /// <summary>json解析与保存</summary>
    public static class JsonTool {
        /// <summary> 内部包装类 </summary>
        private class Pack<T> { public T data; }
        /// <summary> 把对象转换为Json字符串 </summary>
        /// <param name="obj">对象</param>
        public static string ToJson<T>(T obj) {
            if (obj == null) return "null"; 
            if (obj.GetType().GetInterface("IList") != null) {
                Pack<T> pack = new Pack<T>();
                pack.data = obj;
                string json = JsonUtility.ToJson(pack);
                return json.Substring(8, json.Length - 9);
            }
            return JsonUtility.ToJson(obj);
        }
        /// <summary> 解析Json </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="json">Json字符串</param>
        public static T FromJson<T>(string json) {
            if (json == "null" && typeof(T).IsClass) return default(T);
            if (typeof(T).GetInterface("IList") != null) {
                json = "{\"data\":{data}}".Replace("{data}", json);
                Pack<T> Pack = JsonUtility.FromJson<Pack<T>>(json);
                return Pack.data;
            }
            return JsonUtility.FromJson<T>(json);
        }
    }
}