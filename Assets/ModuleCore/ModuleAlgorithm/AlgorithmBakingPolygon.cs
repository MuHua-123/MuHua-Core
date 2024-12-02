using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 烘焙多边形网格
/// </summary>
public class AlgorithmBakingPolygon : ModuleAlgorithm<DataPlateBaking> {

    protected override void Awake() => ModuleCore.AlgorithmBakingPolygon = this;

    public override void Compute(DataPlateBaking baking) {
        int maxIndex = baking.vertices.Length;
        Vector3[] vertices = new Vector3[maxIndex];
        for (int i = 0; i < maxIndex; i++) {
            Quaternion quaternion = Quaternion.Euler(baking.eulerAngles);
            Vector3 rotate = quaternion * baking.vertices[i].position;
            Vector3 position = rotate + baking.position;
            vertices[i] = position;
        }

        baking.mesh = new Mesh();
        baking.mesh.vertices = vertices;
        baking.mesh.uv = baking.uv;
        baking.mesh.triangles = baking.triangles;
        baking.mesh.RecalculateBounds();
        baking.mesh.RecalculateNormals();
    }
}
