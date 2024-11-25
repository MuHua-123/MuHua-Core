using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 缝合边算法模块
/// </summary>
public class AlgorithmSutureSide : ModuleAlgorithm<DataSutureSide> {
    public class VertexPosition : IComparable<VertexPosition> {
        public Vector3 designPosition;
        public Vector3 bakingPosition;
        public float distance;
        public int CompareTo(VertexPosition other) {
            return other.distance >= distance ? 1 : -1;
        }
    }

    protected override void Awake() => ModuleCore.AlgorithmSutureSide = this;

    public override void Compute(DataSutureSide data) {
        //List<VertexPosition> vertexPositions = VertexPositions(data);
        data.designPositions = VertexToDesignPositions(data).ToArray();
        data.bakingPositions = VertexToBakingPositions(data).ToArray();
    }

    private List<VertexPosition> VertexPositions(DataSutureSide sutureSide) {
        List<VertexPosition> vertexPositions = new List<VertexPosition>();
        for (int i = 0; i < sutureSide.Vertices.Length; i++) {
            Vector3 design = sutureSide.Vertices[i].design;
            Quaternion quaternion = Quaternion.Euler(sutureSide.PlateBakingEulerAngles);
            Vector3 baking = quaternion * sutureSide.Vertices[i].design;
            VertexPosition vertexPosition = new VertexPosition();
            vertexPosition.designPosition = design + sutureSide.PlateDesignPosition;
            vertexPosition.bakingPosition = baking + sutureSide.PlateBakingPosition;
            vertexPosition.distance = Vector3.Distance(design, sutureSide.side.aPoint.position);
            vertexPositions.Add(vertexPosition);
        }
        //按距离从小到大排序
        vertexPositions.Sort();
        //是否颠倒
        if (sutureSide.isReversal) { vertexPositions.Reverse(); }
        return vertexPositions;
    }
    private List<Vector3> VertexToDesignPositions(DataSutureSide data) {
        //转换列表
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < data.Vertices.Length; i++) {
            Vector3 position = data.Vertices[i].design + data.PlateDesignPosition;
            positions.Add(position);
        }
        if (data.isReversal) { positions.Reverse(); }
        return positions;
    }
    private List<Vector3> VertexToBakingPositions(DataSutureSide data) {
        //转换列表
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < data.Vertices.Length; i++) {
            Quaternion quaternion = Quaternion.Euler(data.PlateBakingEulerAngles);
            Vector3 baking = quaternion * data.Vertices[i].design;
            Vector3 position = baking + data.PlateBakingPosition;
            positions.Add(position);
        }
        if (data.isReversal) { positions.Reverse(); }
        return positions;
    }
}
