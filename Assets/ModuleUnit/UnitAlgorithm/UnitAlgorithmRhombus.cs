using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 菱形绘制三角形算法
/// </summary>
public class UnitAlgorithmRhombus : UnitAlgorithm<DataPlateBaking> {

    public void Compute(DataPlateBaking plateBaking) {
        List<DataTriangle> triangles = new List<DataTriangle>();
        List<DataPlateVertex> vertexs = new List<DataPlateVertex>();
        //plateBaking.grid.Loop((x, y) => {
        //    DataPlateVertex vertex = plateBaking.grid.Get(x, y);
        //    if (!vertex.isValid) { return; }

        //    TryGet(plateBaking.grid, x, y + 1, ref vertex.above);
        //    TryGet(plateBaking.grid, x, y - 1, ref vertex.below);
        //    TryGet(plateBaking.grid, x - 1, y, ref vertex.left);
        //    TryGet(plateBaking.grid, x + 1, y, ref vertex.right);

        //    TryGet(plateBaking.grid, x - 1, y + 1, ref vertex.leftAbove);
        //    TryGet(plateBaking.grid, x - 1, y - 1, ref vertex.leftBelow);
        //    TryGet(plateBaking.grid, x + 1, y + 1, ref vertex.rightAbove);
        //    TryGet(plateBaking.grid, x + 1, y - 1, ref vertex.rightBelow);
        //    //默认绘制左上角
        //    if (vertex.above != null && vertex.left != null) {
        //        triangles.Add(CreateDataTriangle(vertex, vertex.left, vertex.above));
        //    }
        //    //默认绘制右下角
        //    if (vertex.below != null && vertex.right != null) {
        //        triangles.Add(CreateDataTriangle(vertex, vertex.right, vertex.below));
        //    }
        //    //如果右上角点不存在，则尝试绘制右上角
        //    if (vertex.rightAbove == null && vertex.above != null && vertex.right != null) {
        //        triangles.Add(CreateDataTriangle(vertex, vertex.above, vertex.right));
        //    }
        //    //如果左下角点不存在，则尝试绘制左下角
        //    if (vertex.leftBelow == null && vertex.below != null && vertex.left != null) {
        //        triangles.Add(CreateDataTriangle(vertex, vertex.below, vertex.left));
        //    }
        //    vertexs.Add(vertex);
        //});
        //plateBaking.triangles = triangles;
        //plateBaking.vertexs = vertexs.ToArray();
    }

    /// <summary> 校验性获取网格上的点 </summary>
    private void TryGet(GridTool<DataPlateVertex> grid, int x, int y, ref DataPlateVertex vertex) {
        vertex = null;
        if (!grid.TryGet(x, y, out DataPlateVertex data)) { return; }
        if (data.isValid) { vertex = data; }
    }
    /// <summary> 创建三角形 </summary>
    private DataTriangle CreateDataTriangle(DataPlateVertex a, DataPlateVertex b, DataPlateVertex c) {
        DataTriangle triangle = new DataTriangle();
        triangle.b = a.position;
        triangle.c = b.position;
        triangle.a = c.position;
        return triangle;
    }
}
