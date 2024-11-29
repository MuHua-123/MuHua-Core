using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 烘焙的缝合线
/// </summary>
public class UnitAlgorithmSutureBaking : UnitAlgorithm<DataSutureSide> {
    /// <summary> 烘焙的缝合线 </summary>
    public UnitAlgorithmSutureBaking() { }

    public void Compute(DataSutureSide sutureSide) {
        AllVertexs(sutureSide);
        //缝合范围内的顶点
        SetVertex(sutureSide);
        //顶点转换位置
        SetPositions(sutureSide);
    }
    /// <summary> 顶点距离数据 </summary>
    private List<DataSutureSideVertex> GetVertex(DataPlateVertex[] vertexsArray, bool isReversal) {
        float length = 0;
        List<DataSutureSideVertex> vertexs = new List<DataSutureSideVertex>();
        List<DataPlateVertex> sideVertexs = new List<DataPlateVertex>(vertexsArray);
        if (isReversal) { sideVertexs.Reverse(); }
        for (int i = 0; i < sideVertexs.Count - 1; i++) {
            DataSutureSideVertex vertex = new DataSutureSideVertex(length, sideVertexs[i], sideVertexs[i + 1]);
            vertexs.Add(vertex); length += vertex.Distance;
        }
        return vertexs;
    }
    /// <summary> 全部顶点 </summary>
    private void AllVertexs(DataSutureSide sutureSide) {
        DataPlateVertex[] vertexsArray = sutureSide.plateSide.dataBaking.vertexs;
        List<DataSutureSideVertex> allVertexs = GetVertex(vertexsArray, false);
        sutureSide.dataBaking.allVertexs = allVertexs.ToArray();
    }
    /// <summary> 缝合范围内的顶点 </summary>
    private void SetVertex(DataSutureSide sutureSide) {
        //全部顶点
        List<DataSutureSideVertex> allVertexs = GetVertex(sutureSide.plateSide.dataBaking.vertexs, sutureSide.isReversal);
        //缝合范围内的顶点
        float maxLength = sutureSide.suture.length;
        List<DataSutureSideVertex> vertexs = new List<DataSutureSideVertex>();
        for (int i = 0; i < allVertexs.Count; i++) {
            if (allVertexs[i].origin <= maxLength) { vertexs.Add(allVertexs[i]); }
        }
        sutureSide.dataBaking.vertexs = vertexs.ToArray();
    }
    /// <summary> 顶点转换位置 </summary>
    private void SetPositions(DataSutureSide sutureSide) {
        Vector3 platePosition = sutureSide.plateSide.plate.dataBaking.position;
        Vector3 plateEulerAngles = sutureSide.plateSide.plate.dataBaking.eulerAngles;
        List<Vector3> positions = new List<Vector3>();
        DataSutureSideVertex[] vertexs = sutureSide.dataBaking.vertexs;
        for (int i = 0; i < vertexs.Length; i++) {
            Quaternion quaternion = Quaternion.Euler(plateEulerAngles);
            Vector3 baking = quaternion * vertexs[i].a.position;
            Vector3 position = baking + platePosition;
            positions.Add(position);
        }
        sutureSide.dataBaking.positions = positions.ToArray();
    }
}

