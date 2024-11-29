using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

/// <summary>
/// 三角形合并网格
/// </summary>
public class UnitAlgorithmMergeTriangle : UnitAlgorithm<DataPlateDesign>, UnitAlgorithm<DataPlateBaking> {
    /// <summary> 三角形合并网格 </summary>
    public UnitAlgorithmMergeTriangle() { }

    public void Compute(DataPlateDesign plateDesign) {
        List<DataTriangle> polygons = plateDesign.triangles;
        //三角形合并
        List<Vector3> vertices = vertices = MergeVertices(polygons);
        List<int> triangles = JobFindTriangleIndex(polygons, vertices);
        //展开uv (顶点去掉z坐标就是未缩放的平面UV)
        List<Vector2> uv = new List<Vector2>();
        for (int i = 0; i < vertices.Count; i++) { uv.Add(vertices[i]); }
        //附加数据
        plateDesign.mesh = new Mesh();
        plateDesign.mesh.vertices = vertices.ToArray();
        plateDesign.mesh.uv = uv.ToArray();
        plateDesign.mesh.triangles = triangles.ToArray();
        plateDesign.mesh.RecalculateBounds();
        plateDesign.mesh.RecalculateNormals();
    }
    public void Compute(DataPlateBaking plateBaking) {
        //List<DataTriangle> polygons = plateBaking.triangles;
        ////三角形合并
        //List<Vector3> vertices = vertices = MergeVertices(polygons);
        //List<int> triangles = JobFindTriangleIndex(polygons, vertices);
        ////展开uv (顶点去掉z坐标就是未缩放的平面UV)
        //List<Vector2> uv = new List<Vector2>();
        //for (int i = 0; i < vertices.Count; i++) { uv.Add(vertices[i]); }
        ////附加数据
        //plateBaking.mesh = new Mesh();
        //plateBaking.mesh.vertices = vertices.ToArray();
        //plateBaking.mesh.uv = uv.ToArray();
        //plateBaking.mesh.triangles = triangles.ToArray();
        //plateBaking.mesh.RecalculateBounds();
        //plateBaking.mesh.RecalculateNormals();
    }
    /// <summary> 合并顶点 </summary>
    private List<Vector3> MergeVertices(List<DataTriangle> polygons) {
        List<Vector3> vertices = new List<Vector3>();
        for (int i = 0; i < polygons.Count; i++) {
            vertices.Add(polygons[i].a);
            vertices.Add(polygons[i].b);
            vertices.Add(polygons[i].c);
        }
        return vertices.Distinct().ToList();
    }

    #region Jobs
    /// <summary> 三角形顶点索引查找作业 </summary>
    private List<int> JobFindTriangleIndex(List<DataTriangle> polygons, List<Vector3> vertices) {
        NativeArray<DataTriangle> dataArray = new NativeArray<DataTriangle>(polygons.ToArray(), Allocator.TempJob);
        NativeArray<Vector3> verticeArray = new NativeArray<Vector3>(vertices.ToArray(), Allocator.TempJob);
        NativeArray<Triangle> trianglesArray = new NativeArray<Triangle>(polygons.Count, Allocator.TempJob);

        TriangleIndex triangleIndex = new TriangleIndex();
        triangleIndex.dataArray = dataArray;
        triangleIndex.vertices = verticeArray;
        triangleIndex.triangles = trianglesArray;

        JobHandle dependency = new JobHandle();
        JobHandle handle = triangleIndex.ScheduleParallel(polygons.Count, 32, dependency);
        handle.Complete();

        List<int> triangles = new List<int>();
        for (int i = 0; i < trianglesArray.Length; i++) {
            triangles.Add(trianglesArray[i].a);
            triangles.Add(trianglesArray[i].b);
            triangles.Add(trianglesArray[i].c);
        }

        dataArray.Dispose();
        verticeArray.Dispose();
        trianglesArray.Dispose();
        return triangles;
    }
    /// <summary> 三角形顶点索引查找作业 </summary>
    [BurstCompile]
    public struct TriangleIndex : IJobFor {
        [ReadOnly] public NativeArray<DataTriangle> dataArray;
        [ReadOnly] public NativeArray<Vector3> vertices;
        public NativeArray<Triangle> triangles;

        public void Execute(int index) {
            DataTriangle dataTriangle = dataArray[index];
            Triangle triangle = new Triangle();
            bool a = false, b = false, c = false;
            for (int i = 0; i < vertices.Length; i++) {
                Vector3 vector = vertices[i];
                if (dataTriangle.a == vector) { triangle.a = i; a = true; }
                if (dataTriangle.b == vector) { triangle.b = i; b = true; }
                if (dataTriangle.c == vector) { triangle.c = i; c = true; }
                if (a && b && c) { break; }
            }
            triangles[index] = triangle;
        }
    }
    /// <summary> 三角形顶点索引 </summary>
    public struct Triangle { public int a, b, c; }
    #endregion

    /// <summary> 是否启用计时器 </summary>
    private readonly bool isEnableTimer = true;
    /// <summary> 计时器 </summary>
    private void Chronoscope(string content, Action action) {
        if (!isEnableTimer) { action?.Invoke(); return; }
        float time = Time.realtimeSinceStartup;
        action?.Invoke();
        float consumed = Time.realtimeSinceStartup - time;
        Debug.Log($"{content}{consumed * 1000}");
    }
}
