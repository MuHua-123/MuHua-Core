using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

/// <summary>
/// 贝塞尔算法 边缘细分 Jobs
/// </summary>
public class UnitAlgorithmJobsSideSubdivision : UnitAlgorithm<DataPlateDesign>, UnitAlgorithm<DataPlateBaking> {

    /// <summary> 贝塞尔算法 边缘细分 Jobs </summary>
    public UnitAlgorithmJobsSideSubdivision() { }

    public void Compute(DataPlateDesign plateDesign) {
        int count = plateDesign.plate.plateSides.Count;
        List<DataPlateSide> plateSides = plateDesign.plate.plateSides;
        NativeArray<JobSubdivision> jobSubdivisions = new NativeArray<JobSubdivision>(count, Allocator.TempJob);
        NativeArray<JobHandle> jobHandles = new NativeArray<JobHandle>(count, Allocator.TempJob);
        //创建作业任务
        for (int i = 0; i < count; i++) {
            jobSubdivisions[i] = ToJob(plateSides[i]);
            jobHandles[i] = jobSubdivisions[i].Schedule();
        }
        //执行作业
        JobHandle.CompleteAll(jobHandles);
        //作业结果转换
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < count; i++) {
            DataPlateSideDesign design = plateSides[i].dataDesign;
            JobToDesign(design, jobSubdivisions[i]);
            points.AddRange(design.positions);
        }
        plateDesign.points = points.Distinct().ToArray();
        //释放作业
        jobSubdivisions.Dispose();
        jobHandles.Dispose();
    }
    public void Compute(DataPlateBaking plateBaking) {
        //int count = plateBaking.plate.plateSides.Count;
        //List<DataPlateSide> plateSides = plateBaking.plate.plateSides;
        //NativeArray<JobSubdivision> jobSubdivisions = new NativeArray<JobSubdivision>(count, Allocator.Temp);
        //NativeArray<JobHandle> jobHandles = new NativeArray<JobHandle>(count, Allocator.Temp);
        ////创建作业任务
        //for (int i = 0; i < count; i++) {
        //    jobSubdivisions[i] = ToJob(plateSides[i]);
        //    jobHandles[i] = jobSubdivisions[i].Schedule();
        //}
        ////执行作业
        //JobHandle.CompleteAll(jobHandles);
        ////作业结果转换
        ////List<Vector3> points = new List<Vector3>();
        //for (int i = 0; i < count; i++) {
        //    DataPlateSideBaking sideBaking = plateSides[i].dataBaking;
        //    JobToBaking(sideBaking, jobSubdivisions[i]);
        //    //points.AddRange(sideBaking.positions);
        //}
        ////plateBaking.points = points.Distinct().ToArray();
    }

    /// <summary> 转换作业系统数据 </summary>
    public JobSubdivision ToJob(DataPlateSide side) {
        JobSubdivision jobSubdivision = new JobSubdivision();
        jobSubdivision.bezier = side.bezier;
        jobSubdivision.aPoint = side.aPoint.position;
        jobSubdivision.bPoint = side.bPoint.position;
        jobSubdivision.aBezier = side.aBezier;
        jobSubdivision.bBezier = side.bBezier;
        //距离
        float distance = Vector2.Distance(jobSubdivision.aPoint, jobSubdivision.bPoint);
        jobSubdivision.distance = distance;
        //求余，得商数
        jobSubdivision.quotient = side.bezier == Bezier.一阶 ? 2 : 10;

        jobSubdivision.length = new NativeArray<float>(1, Allocator.TempJob);
        jobSubdivision.positions = new NativeArray<Vector3>(jobSubdivision.quotient, Allocator.TempJob);
        jobSubdivision.lines = new NativeArray<JobDataLine>(jobSubdivision.quotient - 1, Allocator.TempJob);

        return jobSubdivision;
    }
    /// <summary> 作业系统数据转换设计数据 </summary>
    public void JobToDesign(DataPlateSideDesign design, JobSubdivision job) {
        design.length = job.length[0];
        design.positions = job.positions.ToArray();
        DataPlateLine[] lines = new DataPlateLine[job.lines.Length];
        for (int i = 0; i < job.lines.Length; i++) {
            lines[i] = ToData(job.lines[i]);
        }
        design.lines = lines;
        job.length.Dispose();
        job.positions.Dispose();
        job.lines.Dispose();
    }
    /// <summary> 作业系统数据转换烘焙数据 </summary>
    public void JobToBaking(DataPlateSideBaking baking, JobSubdivision job) {
        baking.length = job.length[0];
        baking.positions = job.positions.ToArray();
        DataPlateLine[] lines = new DataPlateLine[job.lines.Length];
        for (int i = 0; i < job.lines.Length; i++) {
            lines[i] = ToData(job.lines[i]);
        }
        baking.lines = lines;
        job.length.Dispose();
        job.positions.Dispose();
        job.lines.Dispose();
    }
    /// <summary> 转换托管数据 </summary>
    public DataPlateLine ToData(JobDataLine jobDataLine) {
        DataPlateLine line = new DataPlateLine();
        line.a = jobDataLine.a;
        line.b = jobDataLine.b;
        line.origin = jobDataLine.origin;
        return line;
    }

    /// <summary> 作业系统数据 </summary>
    public struct JobDataLine {
        /// <summary> 线段起点a </summary>
        public Vector3 a;
        /// <summary> 线段终点b </summary>
        public Vector3 b;
        /// <summary> 原始距离 </summary>
        public float origin;
    }
    [BurstCompile]
    public struct JobSubdivision : IJob {

        #region 输入
        /// <summary> 贝塞尔类型 </summary>
        public Bezier bezier;
        /// <summary> a点 </summary>
        public Vector3 aPoint;
        /// <summary> b点 </summary>
        public Vector3 bPoint;
        /// <summary> a点贝塞尔点 </summary>
        public Vector3 aBezier;
        /// <summary> b点贝塞尔点 </summary>
        public Vector3 bBezier;
        /// <summary> a-b距离 </summary>
        public float distance;
        /// <summary> 细分数 </summary>
        public int quotient;
        #endregion

        #region 输出
        /// <summary> 总长度 </summary>
        public NativeArray<float> length;
        /// <summary> 点 </summary>
        public NativeArray<Vector3> positions;
        /// <summary> 线 </summary>
        public NativeArray<JobDataLine> lines;
        #endregion

        public void Execute() {
            //细分点
            if (bezier == Bezier.一阶) { ComputeBezierA(); }
            if (bezier == Bezier.二阶) { ComputeBezierB(); }
            if (bezier == Bezier.三阶) { ComputeBezierC(); }
            //线段
            for (int i = 0; i < quotient - 1; i++) {
                JobDataLine line = new JobDataLine();
                line.a = positions[i];
                line.b = positions[i + 1];
                line.origin = length[0];
                lines[i] = line;
                length[0] += Vector3.Distance(line.a, line.b);
            }
        }
        public void ComputeBezierA() {
            positions[0] = aPoint;
            positions[1] = bPoint;
        }
        public void ComputeBezierB() {
            for (int i = 0; i < quotient; i++) {
                float t = i * (distance / quotient) / distance;
                Vector2 position = ComputeBezier(aPoint, aBezier, bPoint, t);
                positions[i] = position;
            }
        }
        public void ComputeBezierC() {
            for (int i = 0; i < quotient; i++) {
                float t = i * (distance / quotient) / distance;
                Vector2 position = ComputeBezier(aPoint, aBezier, bBezier, bPoint, t);
                positions[i] = position;
            }
        }
    }

    /// <summary> 商数 </summary>
    public static int Quotient(float distance, float smooth) {
        int a = (int)(distance * 1000);
        int b = (int)(smooth * 1000);
        return Math.DivRem(a, b, out int remainder);
    }
    /// <summary>
    /// 一阶贝塞尔算法
    /// </summary>
    /// <param name="a">起点</param>
    /// <param name="b">终点</param>
    /// <param name="t">进度</param>
    /// <returns></returns>
    public static Vector3 ComputeBezier(Vector3 a, Vector3 b, float t) {
        return a + (b - a) * t;
    }
    /// <summary>
    /// 二阶贝塞尔算法
    /// </summary>
    /// <param name="a">起点</param>
    /// <param name="b">贝塞尔点</param>
    /// <param name="c">终点</param>
    /// <param name="t">进度</param>
    /// <returns>当前进度的曲线点</returns>
    public static Vector3 ComputeBezier(Vector3 a, Vector3 b, Vector3 c, float t) {
        Vector3 aa = a + (b - a) * t;
        Vector3 bb = b + (c - b) * t;
        return aa + (bb - aa) * t;
    }
    /// <summary>
    /// 三阶贝塞尔算法
    /// </summary>
    /// <param name="a">起点</param>
    /// <param name="b">起点的贝塞尔点</param>
    /// <param name="c">终点的贝塞尔点</param>
    /// <param name="d">终点</param>
    /// <param name="t">进度</param>
    /// <returns>当前进度的曲线点</returns>
    public static Vector3 ComputeBezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t) {
        Vector3 aa = a + (b - a) * t;
        Vector3 bb = b + (c - b) * t;
        Vector3 cc = c + (d - c) * t;

        Vector3 aaa = aa + (bb - aa) * t;
        Vector3 bbb = bb + (cc - bb) * t;
        return aaa + (bbb - aaa) * t;
    }
}
