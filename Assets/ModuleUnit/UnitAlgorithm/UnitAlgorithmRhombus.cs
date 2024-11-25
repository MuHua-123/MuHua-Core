using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 菱形绘制三角形算法
/// </summary>
public class UnitAlgorithmRhombus : UnitAlgorithm<DataPlate> {

    public void Compute(DataPlate data) {
        List<DataTriangle> triangles = new List<DataTriangle>();
        data.vertexGrid.Loop((x, y) => {
            DataVertex vertex = data.vertexGrid.Get(x, y);
            if (!vertex.isValid) { return; }

            TryGet(data.vertexGrid, x, y + 1, ref vertex.above);
            TryGet(data.vertexGrid, x, y - 1, ref vertex.below);
            TryGet(data.vertexGrid, x - 1, y, ref vertex.left);
            TryGet(data.vertexGrid, x + 1, y, ref vertex.right);

            TryGet(data.vertexGrid, x - 1, y + 1, ref vertex.leftAbove);
            TryGet(data.vertexGrid, x - 1, y - 1, ref vertex.leftBelow);
            TryGet(data.vertexGrid, x + 1, y + 1, ref vertex.rightAbove);
            TryGet(data.vertexGrid, x + 1, y - 1, ref vertex.rightBelow);
            //默认绘制左上角
            if (vertex.above != null && vertex.left != null) {
                triangles.Add(CreateDataTriangle(vertex, vertex.left, vertex.above));
            }
            //默认绘制右下角
            if (vertex.below != null && vertex.right != null) {
                triangles.Add(CreateDataTriangle(vertex, vertex.right, vertex.below));
            }
            //如果右上角点不存在，则尝试绘制右上角
            if (vertex.rightAbove == null && vertex.above != null && vertex.right != null) {
                triangles.Add(CreateDataTriangle(vertex, vertex.above, vertex.right));
            }
            //如果左下角点不存在，则尝试绘制左下角
            if (vertex.leftBelow == null && vertex.below != null && vertex.left != null) {
                triangles.Add(CreateDataTriangle(vertex, vertex.below, vertex.left));
            }
        });
        data.triangles = triangles;
    }

    /// <summary> 校验性获取网格上的点 </summary>
    private void TryGet(GridTool<DataVertex> grid, int x, int y, ref DataVertex vertex) {
        vertex = null;
        if (!grid.TryGet(x, y, out DataVertex data)) { return; }
        if (data.isValid) { vertex = data; }
    }
    /// <summary> 创建三角形 </summary>
    private DataTriangle CreateDataTriangle(DataVertex a, DataVertex b, DataVertex c) {
        DataTriangle triangle = new DataTriangle();
        triangle.b = a.design;
        triangle.c = b.design;
        triangle.a = c.design;
        return triangle;
    }
}
