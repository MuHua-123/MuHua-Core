using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets.ResourceLocators;
using MuHua;

/// <summary>
/// 模组系统
/// </summary>
public class ModuleSystem : Module<ModuleSystem> {
	/// <summary> 模组数据 </summary>
	public List<ModuleData> modules = new List<ModuleData>();
	/// <summary> 模组更改事件 </summary>
	public static event Action OnChange;

	#region 模组加载
	/// <summary> 加载默认模组列表 </summary>
	public static void LoadModules() {
		I.modules.Clear();
		string modulePath = ModulePath();
		EnsureDirectoryExists(modulePath);
		foreach (string directory in Directory.GetDirectories(modulePath)) { LoadModules(directory); }
		OnChange?.Invoke();
	}
	public static void LoadModules(string directory) {
		ModuleData module = ReadModule(directory);
		if (module != null) { I.modules.Add(module); }
	}

	/// <summary> 读取模组文件夹 </summary>
	private static ModuleData ReadModule(string directory) {
		// 查询所有 catalog_*.json 文件
		string[] files = Directory.GetFiles(directory, "catalog_*.json", SearchOption.TopDirectoryOnly);
		if (files.Length == 0) return null;
		// 获取模组名称
		string name = Path.GetFileName(directory);
		// 取第一个匹配的文件，统一路径分隔符为正斜杠
		string catalog = files[0].Replace("\\", "/");
		// 获取 * 的内容为版本信息
		string version = Path.GetFileNameWithoutExtension(catalog).Replace("catalog_", string.Empty);
		return new ModuleData { name = name, catalogPath = catalog, version = version };
	}
	/// <summary> 确保目录存在 </summary>
	private static void EnsureDirectoryExists(string path) {
		if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
	}

	/// <summary> 模组路径 </summary>
	private static string ModulePath() {
#if UNITY_EDITOR
		string exclude = "/Assets/StreamingAssets";
		string streaming = Application.streamingAssetsPath;
		string root = streaming.Remove(streaming.Length - exclude.Length);
		return $"{root}/Library/com.unity.addressables/aa/Windows/{BuildTarget()}";
#else
		return $"{SaveTool.PATH}/aa/{BuildTarget()}";
#endif
	}
	/// <summary> 平台路径 </summary>
	private static string BuildTarget() {
		// if (Application.platform == RuntimePlatform.WindowsEditor) { return "StandaloneWindows64"; }
		// if (Application.platform == RuntimePlatform.WindowsPlayer) { return "StandaloneWindows64"; }
		return "StandaloneWindows64";
	}
	#endregion

	#region 模组控制
	/// <summary> 加载模组 </summary>
	public async void LoadModule(ModuleData module) {
		string filePath = module.catalogPath;
		var handle = Addressables.LoadContentCatalogAsync(filePath, false);
		await handle.Task;
		if (handle.Status == AsyncOperationStatus.Failed) { Debug.LogError($"无法加载资源目录!({filePath})"); }
		module.locator = handle.Result;
		OnChange?.Invoke();
	}
	/// <summary> 协程：加载模组 </summary>
	public IEnumerator ILoadModule(ModuleData module) {
		string filePath = module.catalogPath;
		var handle = Addressables.LoadContentCatalogAsync(filePath, false);
		yield return handle;
		if (handle.Status == AsyncOperationStatus.Failed) { Debug.LogError($"无法加载资源目录!({filePath})"); }
		module.locator = handle.Result;
		OnChange?.Invoke();
	}
	/// <summary> 卸载模组 </summary>
	public void UnloadModule(ModuleData module) {
		if (module?.locator == null) { return; }
		Addressables.RemoveResourceLocator(module.locator);
		OnChange?.Invoke();
	}
	#endregion

	/// <summary> 加载资源 </summary>
	public static async void Loads<T>(string tag, Action<T> callback) {
		var handle = Addressables.LoadAssetsAsync(tag, callback, true);
		await handle.Task;
		if (handle.Status != AsyncOperationStatus.Failed) { return; }
		Debug.LogWarning($"加载资源失败! (标签: {tag}, 类型: {typeof(T)})");
	}
}
