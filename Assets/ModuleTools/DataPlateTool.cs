using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataPlateTool {
    /// <summary> 计算板片数据 </summary>
    public static void Compute(this DataPlate data) {
        //ModuleCore.I.EdgeSort.Compute(this);
        ModuleCore.I.GenerateEdge.Compute(data);
        ModuleCore.I.Polygon.Compute(data);
        data.OnChange?.Invoke();
    }
    /// <summary> 头尾循环标准化索引 </summary>
    public static int NormalIndex(int index, int maxIndex) {
        if (maxIndex == 0) { Debug.LogError("错误索引：maxIndex = 0"); return 0; }
        if (index < 0) { return NormalIndex(index + maxIndex, maxIndex); }
        if (index >= maxIndex) { return NormalIndex(index - maxIndex, maxIndex); }
        return index;
    }

    /// <summary> 根据索引查询边缘点 </summary>
    public static Vector3 FindEdgePoint(this DataPlate data, int index) {
        index = NormalIndex(index, data.edgePoints.Count);
        return data.edgePoints[index];
    }
    /// <summary> 改变边缘点 </summary>
    public static void ChangeEdgePoint(this DataPlate data, int index, Vector3 position) {
        index = NormalIndex(index, data.edgePoints.Count);
        data.edgePoints[index] = position;
        data.OnChangeEdgePoint?.Invoke(index);
    }
    /// <summary> 插入边缘点 </summary>
    public static void InsertEdgePoint(this DataPlate data, int index, Vector3 position) {
        index = NormalIndex(index, data.edgePoints.Count);
        data.edgePoints.Insert(index + 1, position);
        data.Compute();
    }

    /// <summary> 根据索引查询设计点 </summary>
    public static DataDesignPoint FindDesignPoint(this DataPlate data, int index) {
        index = NormalIndex(index, data.designPoints.Count);
        return data.designPoints[index];
    }
    /// <summary> 改变设计点 </summary>
    public static void ChangeDesignPoint(this DataPlate data, int index, Vector3 position) {
        index = NormalIndex(index, data.edgePoints.Count);
        data.designPoints[index].postiton = position;
        data.OnChangeDesignPoint?.Invoke(index);
    }
    /// <summary> 插入边缘点 </summary>
    public static void InsertDesignPoint(this DataPlate data, int index, Vector2 position) {
        int maxIndex = data.designPoints.Count;
        index = NormalIndex(index, data.designPoints.Count);
        int left = NormalIndex(index + 1, maxIndex);
        int right = NormalIndex(index, maxIndex);
        Vector2 leftBezier = (data.designPoints[left].postiton - position) * 0.5f;
        Vector2 rightBezier = (data.designPoints[right].postiton - position) * 0.5f;

        for (int i = index + 1; i < maxIndex; i++) {
            data.designPoints[i].index++;
        }

        DataDesignPoint designPoint = new DataDesignPoint(data);
        designPoint.index = index + 1;
        designPoint.postiton = position;
        designPoint.leftBezier = leftBezier;
        designPoint.rightBezier = rightBezier;
        data.designPoints.Insert(index + 1, designPoint);
        data.Compute();
    }
}
