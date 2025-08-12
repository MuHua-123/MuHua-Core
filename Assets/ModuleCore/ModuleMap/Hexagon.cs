using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 六边形
/// </summary>
public class Hexagon : MonoBehaviour {

	public Transform prefab;

	private void Awake() {
		float size = 0.5f; // 可以根据实际需求调整边长
		for (int x = 0; x < 10; x++) {
			for (int y = 0; y < 10; y++) { Create(new Vector2Int(x, y), size); }
		}
	}
	private void Create(Vector2Int grid, float size) {
		Transform obj = Instantiate(prefab, transform);
		obj.position = GridToWorld(grid, size);
		obj.name = $"Hex_{grid}";
		obj.gameObject.SetActive(true);
	}

	/// <summary>
	/// 获取六边形网格的世界坐标
	/// </summary>
	/// <param name="grid">网格坐标(x, y)</param>
	/// <param name="size">六边形边长</param>
	/// <returns>世界坐标</returns>
	public static Vector3 GridToWorld(Vector2Int grid, float size) {
		float width = size * 2f;
		float height = Mathf.Sqrt(3f) * size;
		float offsetX = grid.x * (width * 0.75f);
		float offsetY = grid.y * height + (grid.x % 2 == 0 ? 0 : height / 2f);
		return new Vector3(offsetX, 0, offsetY);
	}

	/// <summary>
	/// 使用世界坐标转换成六边形网格的x和y坐标
	/// </summary>
	/// <param name="worldPos">世界坐标</param>
	/// <param name="size">六边形边长</param>
	/// <returns>网格坐标(x, y)</returns>
	public static Vector2Int WorldToGrid(Vector3 worldPosition, float size) {
		float width = size * 2f;
		float height = Mathf.Sqrt(3f) * size;
		// 计算近似的x
		int x = Mathf.RoundToInt(worldPosition.x / (width * 0.75f));
		// 计算近似的y
		float yOffset = (x % 2 == 0) ? 0 : height / 2f;
		int y = Mathf.RoundToInt((worldPosition.z - yOffset) / height);
		return new Vector2Int(x, y);
	}
}
