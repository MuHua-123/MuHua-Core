using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

/// <summary>
/// 细分多边形算法
/// </summary>
public class AlgorithmSubdivisionPolygon : ModuleAlgorithm<DataPlate> {
    private static readonly float Subdivide = 0.1f;
    private UnitAlgorithm<Polygon> bezierPolygonSide = new BezierPolygonSide();

    private UnitAlgorithm<DataPlate> Boundary = new PolygonalBoundary();
    private UnitAlgorithm<DataPlate> Vertex = new VertexSubdivision();
    //private UnitAlgorithm<DataPlate> sideVertex = new SideSubdivision();
    private UnitAlgorithm<DataPlate> Triangle = new DrawTriangle();

    protected override void Awake() => ModuleCore.AlgorithmSubdivisionPolygon = this;

    public override void Compute(DataPlate plate) {
        Polygon polygon = To(plate);
        //第一次计算边长
        bezierPolygonSide.Compute(polygon);
        //第二次计算细分边长
        bezierPolygonSide.Compute(polygon);

        //Chronoscope("细分多边形计算耗时：", () => {
        //    //粗糙细分，生成边界
        //    Boundary.Compute(plate);
        //    //计算顶点
        //    Vertex.Compute(plate);
        //    //计算边点
        //    SideVertex.Compute(plate);
        //    //计算三角面
        //    Triangle.Compute(plate);
        //});
    }

    #region 数据转换
    public Polygon To(DataPlate plate) {
        Polygon polygon = new Polygon();
        polygon.sides = new PolygonSide[plate.plateSides.Count];
        for (int i = 0; i < plate.plateSides.Count; i++) {
            polygon.sides[i] = To(plate.plateSides[i]);
        }
        return polygon;
    }
    public PolygonSide To(DataPlateSide plateSide) {
        PolygonSide polygonSide = new PolygonSide();
        polygonSide.quotient = 20;
        polygonSide.bezier = plateSide.bezier;
        polygonSide.aPoint = plateSide.aPoint.position;
        polygonSide.bPoint = plateSide.bPoint.position;
        polygonSide.aBezier = plateSide.aBezier;
        polygonSide.bBezier = plateSide.bBezier;
        return polygonSide;
    }
    #endregion

    #region 数据结构
    public struct Polygon {
        /// <summary> 多边形边 </summary>
        public PolygonSide[] sides;
        /// <summary> 多边形边界 </summary>
        public PolygonBorder border;
    }
    public struct PolygonBorder {
        /// <summary> minX </summary>
        public float minX;
        /// <summary> maxX </summary>
        public float maxX;
        /// <summary> minY </summary>
        public float minY;
        /// <summary> maxY </summary>
        public float maxY;
        /// <summary> 多边形边界点 </summary>
        public Vector3[] positions;

        /// <summary> 边界宽 </summary>
        public float Wide => maxX - minX;
        /// <summary> 边界高 </summary>
        public float High => maxY - minY;
        /// <summary> 网格宽 </summary>
        public int GridWide => Mathf.FloorToInt(Wide / Subdivide) + 1;
        /// <summary> 网格高 </summary>
        public int GridHigh => Mathf.FloorToInt(High / Subdivide) + 1;
        /// <summary> 最小点 </summary>
        public Vector3 MinPoint => new Vector3(minX, minY, 0);
        /// <summary> 最大点 </summary>
        public Vector3 MaxPoint => new Vector3(maxX, maxY, 0);
    }
    public struct PolygonSide {
        /// <summary> 细分数 </summary>
        public int quotient;
        /// <summary> 长度 </summary>
        public float length;
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

        /// <summary> 点 </summary>
        public Vector3[] positions;
        /// <summary> 线 </summary>
        public PolygonLine[] lines;
        /// <summary> 边点 </summary>
        public PolygonSideVertex[] vertexs;
    }
    public struct PolygonSideVertex {
        /// <summary> 到边起点的距离 </summary>
        public float origin;
        /// <summary> 位置 </summary>
        public Vector3 position;
    }
    public struct PolygonLine {
        /// <summary> 线段起点a </summary>
        public Vector3 a;
        /// <summary> 线段终点b </summary>
        public Vector3 b;
        /// <summary> 到边起点的距离 </summary>
        public float origin;
    }
    #endregion

    #region 计时器
    /// <summary> 是否启用计时器 </summary>
    public static readonly bool isEnableTimer = true;
    /// <summary> 计时器 </summary>
    public static void Chronoscope(string content, Action action) {
        if (!isEnableTimer) { action?.Invoke(); return; }
        float time = Time.realtimeSinceStartup;
        action?.Invoke();
        float consumed = Time.realtimeSinceStartup - time;
        Debug.Log($"{content}{consumed * 1000}");
    }
    #endregion

    #region 算法函数
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
    /// <summary>
    /// 边界数据
    /// </summary>
    /// <param name="points">边界点数组</param>
    /// <returns></returns>
    public static DataBorder Border(Vector3[] points) {
        float minX = 0; float minY = 0;
        float maxX = 0; float maxY = 0;
        for (int i = 0; i < points.Length; i++) {
            if (points[i].x < minX) { minX = points[i].x; }
            if (points[i].x > maxX) { maxX = points[i].x; }
            if (points[i].y < minY) { minY = points[i].y; }
            if (points[i].y > maxY) { maxY = points[i].y; }
        }
        return new DataBorder(minX, maxX, minY, maxY, points);
    }
    /// <summary>
    /// 数组索引转换网格xy
    /// </summary>
    /// <param name="index"></param>
    /// <param name="wide"></param>
    /// <param name="high"></param>
    /// <returns></returns>
    public static Vector2Int IndexToXY(int index, int wide) {
        int y = Math.DivRem(index, wide, out int x);
        return new Vector2Int(x, y);
    }
    /// <summary>
    /// 网格xy转换数组索引
    /// </summary>
    /// <param name="v2"></param>
    /// <returns></returns>
    public static int XYToIndex(Vector2Int v2, int wide) {
        return v2.y * wide + v2.x;
    }
    /// <summary>
    /// 校验xy是否超限
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="wide"></param>
    /// <param name="high"></param>
    /// <returns></returns>
    public static bool TryXY(int x, int y, int wide, int high) {
        return x >= 0 && x < wide && y >= 0 && y < high;
    }
    /// <summary>
    /// 转角法查询位置是否在板片内
    /// </summary>
    /// <param name="points">按顺序组成多边形的顶点</param>
    /// <param name="position">位置</param>
    /// <returns></returns>
    public static bool FindPlateInside(NativeArray<Vector3> points, Vector3 position) {
        double angles = 0;
        for (int i = 0; i < points.Length; i++) {
            Vector3 a = points[LoopIndexTool.LoopIndex(i + 0, points.Length)] - position;
            Vector3 b = points[LoopIndexTool.LoopIndex(i + 1, points.Length)] - position;
            float angle = Vector2.SignedAngle(a, b);
            angles += angle;
        }
        int normal = (int)(angles * 1000);
        return normal > 1000;
    }
    /// <summary>
    /// 计算AB与CD两条线段的交点.
    /// </summary>
    /// <param name="a">A点</param>
    /// <param name="b">B点</param>
    /// <param name="c">C点</param>
    /// <param name="d">D点</param>
    /// <param name="intersectPos">AB与CD的交点</param>
    /// <returns>是否相交 true:相交 false:未相交</returns>
    public static bool TryGetIntersectPoint(Vector3 a, Vector3 b, Vector3 c, Vector3 d, out Vector3 intersectPos) {
        intersectPos = Vector3.zero;

        Vector3 ab = b - a;
        Vector3 ca = a - c;
        Vector3 cd = d - c;

        Vector3 v1 = Vector3.Cross(ca, cd);
        // 不共面
        if (Mathf.Abs(Vector3.Dot(v1, ab)) > 1e-6) { return false; }
        // 平行
        if (Vector3.Cross(ab, cd).sqrMagnitude <= 1e-6) { return false; }

        Vector3 ad = d - a;
        Vector3 cb = b - c;
        // 快速排斥
        if (Mathf.Min(a.x, b.x) > Mathf.Max(c.x, d.x) || Mathf.Max(a.x, b.x) < Mathf.Min(c.x, d.x)
           || Mathf.Min(a.y, b.y) > Mathf.Max(c.y, d.y) || Mathf.Max(a.y, b.y) < Mathf.Min(c.y, d.y)
           || Mathf.Min(a.z, b.z) > Mathf.Max(c.z, d.z) || Mathf.Max(a.z, b.z) < Mathf.Min(c.z, d.z)) {
            return false;
        }

        // 跨立试验
        if (Vector3.Dot(Vector3.Cross(-ca, ab), Vector3.Cross(ab, ad)) > 0
            && Vector3.Dot(Vector3.Cross(ca, cd), Vector3.Cross(cd, cb)) > 0) {
            Vector3 v2 = Vector3.Cross(cd, ab);
            float ratio = Vector3.Dot(v1, v2) / v2.sqrMagnitude;
            intersectPos = a + ab * ratio;
            return true;
        }

        return false;
    }
    #endregion

    #region 贝塞尔曲线化多边形的边
    public class BezierPolygonSide : UnitAlgorithm<Polygon> {

        #region 执行
        public void Compute(Polygon polygon) {
            //创建作业任务
            int count = polygon.sides.Length;
            NativeArray<JobBezierPolygonSide> jobs = new NativeArray<JobBezierPolygonSide>(count, Allocator.Temp);
            NativeArray<JobHandle> handles = new NativeArray<JobHandle>(count, Allocator.Temp);
            for (int i = 0; i < count; i++) {
                jobs[i] = To(polygon.sides[i]);
                handles[i] = jobs[i].Schedule();
            }
            //执行作业
            JobHandle.CompleteAll(handles);
            //转换数据
            for (int i = 0; i < count; i++) { To(jobs[i], polygon.sides[i]); }
        }
        #endregion

        #region 转换
        /// <summary> 转换作业数据 </summary>
        private JobBezierPolygonSide To(PolygonSide polygonSide) {
            JobBezierPolygonSide jobBezierPolygonSide = new JobBezierPolygonSide();
            jobBezierPolygonSide.quotient = polygonSide.quotient;
            jobBezierPolygonSide.bezier = polygonSide.bezier;
            jobBezierPolygonSide.aPoint = polygonSide.aPoint;
            jobBezierPolygonSide.bPoint = polygonSide.bPoint;
            jobBezierPolygonSide.aBezier = polygonSide.aBezier;
            jobBezierPolygonSide.bBezier = polygonSide.bBezier;

            int quotient = polygonSide.quotient;
            jobBezierPolygonSide.length = new NativeArray<float>(1, Allocator.TempJob);
            jobBezierPolygonSide.positions = new NativeArray<Vector3>(quotient, Allocator.TempJob);
            jobBezierPolygonSide.lines = new NativeArray<PolygonLine>(quotient - 1, Allocator.TempJob);
            return jobBezierPolygonSide;
        }
        /// <summary> 转换托管数据 </summary>
        private void To(JobBezierPolygonSide job, PolygonSide polygonSide) {
            polygonSide.quotient = (int)(Subdivide / job.length[0]);
            polygonSide.length = job.length[0];
            polygonSide.positions = job.positions.ToArray();
            polygonSide.lines = job.lines.ToArray();
            job.length.Dispose();
            job.positions.Dispose();
            job.lines.Dispose();
        }
        #endregion

        #region Jobs
        [BurstCompile]
        public struct JobBezierPolygonSide : IJob {
            /// <summary> 细分数 </summary>
            public int quotient;
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

            /// <summary> 长度 </summary>
            public NativeArray<float> length;
            /// <summary> 点 </summary>
            public NativeArray<Vector3> positions;
            /// <summary> 线 </summary>
            public NativeArray<PolygonLine> lines;

            public void Execute() {
                //细分点
                if (bezier == Bezier.一阶) { ComputeBezierA(); }
                if (bezier == Bezier.二阶) { ComputeBezierB(); }
                if (bezier == Bezier.三阶) { ComputeBezierC(); }
                //线段
                for (int i = 0; i < quotient - 1; i++) {
                    PolygonLine line = new PolygonLine();
                    line.a = positions[i];
                    line.b = positions[i + 1];
                    line.origin = length[0];
                    lines[i] = line;
                    length[0] += Vector3.Distance(line.a, line.b);
                }
            }
            public void ComputeBezierA() {
                Vector3 a = aPoint;
                Vector3 b = bPoint;
                for (int i = 0; i < quotient; i++) {
                    float t = i / (float)(quotient - 1);
                    positions[i] = ComputeBezier(a, b, t);
                }
            }
            public void ComputeBezierB() {
                Vector3 a = aPoint;
                Vector3 b = aBezier;
                Vector3 c = bPoint;
                for (int i = 0; i < quotient; i++) {
                    float t = i / (float)(quotient - 1);
                    positions[i] = ComputeBezier(a, b, c, t);
                }
            }
            public void ComputeBezierC() {
                Vector3 a = aPoint;
                Vector3 b = aBezier;
                Vector3 c = bBezier;
                Vector3 d = bPoint;
                for (int i = 0; i < quotient; i++) {
                    float t = i / (float)(quotient - 1);
                    positions[i] = ComputeBezier(a, b, c, d, t);
                }
            }
        }
        #endregion

    }

    #endregion

    #region 多边形边界
    public class PolygonalBorder : UnitAlgorithm<Polygon> {

        #region 执行
        public void Compute(Polygon polygon) {
            int count = polygon.sides.Length;
            //创建作业任务
            JobPolygonalBorder job = To(polygon);
            //执行作业
            JobHandle dependency = new JobHandle();
            JobHandle handle = job.Schedule(count, dependency);
            handle.Complete();
            //转换数据
            polygon.border = To(job);
            //释放
            job.border.Dispose();
            job.positions.Dispose();
        }
        #endregion

        #region 转换
        private JobPolygonalBorder To(Polygon polygon) {
            List<Vector3> positions = new List<Vector3>();
            for (int i = 0; i < polygon.sides.Length; i++) {
                positions.AddRange(polygon.sides[i].positions);
            }

            JobPolygonalBorder jobPolygonalBorder = new JobPolygonalBorder();
            jobPolygonalBorder.border = new NativeArray<float>(4, Allocator.TempJob);
            jobPolygonalBorder.positions = new NativeArray<Vector3>(positions.ToArray(), Allocator.TempJob);
            return jobPolygonalBorder;
        }
        private PolygonBorder To(JobPolygonalBorder job) {
            PolygonBorder border = new PolygonBorder();
            border.minX = job.border[0];
            border.maxX = job.border[1];
            border.minY = job.border[2];
            border.maxY = job.border[3];
            border.positions = job.positions.ToArray();
            return border;
        }
        #endregion

        #region Jobs
        [BurstCompile]
        public struct JobPolygonalBorder : IJobFor {
            /// <summary> 0 = minX , 1 = MaxX , 2 = minY, 3 = MaxY </summary>
            public NativeArray<float> border;
            /// <summary> 点 </summary>
            public NativeArray<Vector3> positions;

            public void Execute(int index) {
                Vector3 position = positions[index];
                if (position.x < border[0]) { border[0] = position.x; }
                if (position.x > border[1]) { border[1] = position.x; }
                if (position.y < border[2]) { border[2] = position.y; }
                if (position.y > border[3]) { border[3] = position.y; }
            }
        }
        #endregion

    }
    #endregion

    #region 边缘细分顶点
    public class SideSubdivisionVertex : UnitAlgorithm<Polygon> {

        #region 执行
        public void Compute(Polygon polygon) {
            //创建作业任务
            int count = polygon.sides.Length;
            NativeArray<JobSideVertex> jobs = new NativeArray<JobSideVertex>(count, Allocator.Temp);
            NativeArray<JobHandle> handles = new NativeArray<JobHandle>(count, Allocator.Temp);
            for (int i = 0; i < count; i++) {
                jobs[i] = To(polygon, polygon.sides[i]);
                handles[i] = jobs[i].Schedule();
            }
            //执行作业
            JobHandle.CompleteAll(handles);
            //转换数据
            for (int i = 0; i < count; i++) { To(jobs[i], polygon.sides[i]); }
        }
        #endregion

        #region 转换
        private JobSideVertex To(Polygon polygon, PolygonSide polygonSide) {
            JobSideVertex jobSideVertex = new JobSideVertex();
            jobSideVertex.gridWide = polygon.border.GridWide;
            jobSideVertex.gridHigh = polygon.border.GridHigh;
            jobSideVertex.interval = Subdivide;
            jobSideVertex.minX = polygon.border.minX;
            jobSideVertex.maxX = polygon.border.maxX;
            jobSideVertex.minY = polygon.border.minY;
            jobSideVertex.maxY = polygon.border.maxY;
            jobSideVertex.minPoint = polygon.border.MinPoint;
            jobSideVertex.lines = new NativeArray<PolygonLine>(polygonSide.lines, Allocator.TempJob);
            jobSideVertex.vertexs = new NativeList<PolygonSideVertex>(Allocator.TempJob);
            return jobSideVertex;
        }
        /// <summary> 转换托管数据 </summary>
        private void To(JobSideVertex job, PolygonSide polygonSide) {
            polygonSide.vertexs = job.vertexs.ToArray();
            job.lines.Dispose();
            job.vertexs.Dispose();
        }
        #endregion

        #region Jobs
        [BurstCompile]
        public struct JobSideVertex : IJob {
            /// <summary> 网格宽 </summary>
            public int gridWide;
            /// <summary> 网格高 </summary>
            public int gridHigh;
            /// <summary> 网格间隔 </summary>
            public float interval;
            /// <summary> minX </summary>
            public float minX;
            /// <summary> maxX </summary>
            public float maxX;
            /// <summary> minY </summary>
            public float minY;
            /// <summary> maxY </summary>
            public float maxY;
            /// <summary> 原点 </summary>
            public Vector3 minPoint;
            /// <summary> 输入边线 </summary>
            public NativeArray<PolygonLine> lines;

            /// <summary> 输出边点 </summary>
            public NativeList<PolygonSideVertex> vertexs;

            public void Execute() {
                for (int x = 0; x < gridWide; x++) {
                    Vector3 a = new Vector3(minX + x * interval, minY - 1);
                    Vector3 b = new Vector3(minX + x * interval, maxY + 1);
                    Subdivision(a, b);
                }
                for (int y = 0; y < gridHigh; y++) {
                    Vector3 a = new Vector3(minX - 1, minY + y * interval);
                    Vector3 b = new Vector3(maxX + 1, minY + y * interval);
                    Subdivision(a, b);
                }
            }
            public void Subdivision(Vector3 a, Vector3 b) {
                for (int i = 0; i < lines.Length; i++) {
                    PolygonLine line = lines[i];
                    if (!TryGetIntersectPoint(a, b, line.a, line.b, out Vector3 intersectPoint)) { continue; }
                    float distance = Vector3.Distance(line.a, intersectPoint) + line.origin;
                    PolygonSideVertex sideVertex = new PolygonSideVertex();
                    sideVertex.origin = distance;
                    sideVertex.position = intersectPoint;
                    vertexs.Add(sideVertex);
                }
            }
        }
        #endregion

    }
    #endregion

    #region 内部细分顶点
    public class InsideSubdivisionVertex : UnitAlgorithm<Polygon> {

        #region 执行
        public void Compute(Polygon data) {
            throw new NotImplementedException();
        }
        #endregion

        #region 转换

        #endregion

        #region Jobs

        #endregion

    }
    #endregion

    #region 多边形边界
    public class PolygonalBoundary : UnitAlgorithm<DataPlate> {

        #region 执行
        public void Compute(DataPlate plate) {
            int count = plate.plateSides.Count;
            List<DataPlateSide> plateSides = plate.plateSides;
            //创建作业任务
            NativeArray<JobPolygonalBoundary> jobs = new NativeArray<JobPolygonalBoundary>(count, Allocator.Temp);
            NativeArray<JobHandle> handles = new NativeArray<JobHandle>(count, Allocator.Temp);
            for (int i = 0; i < count; i++) {
                jobs[i] = DataPlateSideToJobSideSubdivision(plateSides[i], 10);
                handles[i] = jobs[i].Schedule();
            }
            //执行作业
            JobHandle.CompleteAll(handles);
            //转换数据
            List<Vector3> points = new List<Vector3>();
            for (int i = 0; i < count; i++) {
                OutputSideToDataPlateSide(jobs[i].outputSide, plateSides[i]);
                points.AddRange(plateSides[i].dataBaking.positions);
            }
            plate.dataBaking.border = Border(points.Distinct().ToArray());
        }
        #endregion

        #region Jobs
        [BurstCompile]
        public struct JobPolygonalBoundary : IJob {
            /// <summary> 输入 </summary>
            public InputSide inputSide;
            /// <summary> 输出 </summary>
            public OutputSide outputSide;

            public void Execute() {
                //细分点
                for (int i = 0; i < inputSide.quotient; i++) {
                    float t = i / (float)(inputSide.quotient - 1);
                    if (inputSide.bezier == Bezier.一阶) {
                        outputSide.positions[i] = ComputeBezier(inputSide.aPoint, inputSide.bPoint, t);
                    }
                    if (inputSide.bezier == Bezier.二阶) {
                        outputSide.positions[i] = ComputeBezier(inputSide.aPoint, inputSide.aBezier, inputSide.bPoint, t);
                    }
                    if (inputSide.bezier == Bezier.三阶) {
                        outputSide.positions[i] = ComputeBezier(inputSide.aPoint, inputSide.aBezier, inputSide.bBezier, inputSide.bPoint, t);
                    }
                }

                //线段
                for (int i = 0; i < inputSide.quotient - 1; i++) {
                    OutputLine line = new OutputLine();
                    line.a = outputSide.positions[i];
                    line.b = outputSide.positions[i + 1];
                    line.origin = outputSide.length;
                    outputSide.lines[i] = line;
                    outputSide.length += Vector3.Distance(line.a, line.b);
                }
            }
        }
        #endregion

        #region 转换
        /// <summary> 转换作业数据 </summary>
        private JobPolygonalBoundary DataPlateSideToJobSideSubdivision(DataPlateSide plateSide, int quotient) {
            InputSide inputSide = new InputSide();
            inputSide.bezier = plateSide.bezier;
            inputSide.aPoint = plateSide.aPoint.position;
            inputSide.bPoint = plateSide.bPoint.position;
            inputSide.aBezier = plateSide.aBezier;
            inputSide.bBezier = plateSide.bBezier;
            inputSide.quotient = quotient;

            OutputSide outputSide = new OutputSide();
            outputSide.positions = new NativeArray<Vector3>(quotient, Allocator.TempJob);
            outputSide.lines = new NativeArray<OutputLine>(quotient - 1, Allocator.TempJob);

            JobPolygonalBoundary jobPolygonalBoundary = new JobPolygonalBoundary();
            jobPolygonalBoundary.inputSide = inputSide;
            jobPolygonalBoundary.outputSide = outputSide;
            return jobPolygonalBoundary;
        }
        /// <summary> 转换托管数据 </summary>
        private void OutputSideToDataPlateSide(OutputSide output, DataPlateSide plateSide) {
            DataPlateSideBaking baking = plateSide.dataBaking;
            baking.length = output.length;
            baking.positions = output.positions.ToArray();
            DataPlateLine[] lines = new DataPlateLine[output.lines.Length];
            for (int i = 0; i < output.lines.Length; i++) {
                lines[i] = OutputLineToDataPlateLine(output.lines[i]);
            }
            baking.lines = lines;
            output.positions.Dispose();
            output.lines.Dispose();
        }
        /// <summary> 转换托管数据 </summary>
        public DataPlateLine OutputLineToDataPlateLine(OutputLine output) {
            DataPlateLine line = new DataPlateLine();
            line.a = output.a;
            line.b = output.b;
            line.origin = output.origin;
            return line;
        }
        #endregion

        #region 输入
        public struct InputSide {
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
            /// <summary> 细分数 </summary>
            public int quotient;
        }
        #endregion

        #region 输出
        public struct OutputSide {
            /// <summary> 总长度 </summary>
            public float length;
            /// <summary> 点 </summary>
            public NativeArray<Vector3> positions;
            /// <summary> 线 </summary>
            public NativeArray<OutputLine> lines;
        }
        public struct OutputLine {
            /// <summary> 线段起点a </summary>
            public Vector3 a;
            /// <summary> 线段终点b </summary>
            public Vector3 b;
            /// <summary> 边原点距离 </summary>
            public float origin;
        }
        #endregion

    }
    #endregion

    #region 顶点细分
    public class VertexSubdivision : UnitAlgorithm<DataPlate> {

        #region 执行
        public void Compute(DataPlate plate) {
            DataBorder border = plate.dataBaking.border;
            //创建作业任务
            int maxIndex = border.GridWide * border.GridHigh;
            JobVertex job = new JobVertex();
            job.input = DataPlateToInputInitialize(plate);
            job.outputVertices = new NativeArray<OutputVertex>(maxIndex, Allocator.TempJob);
            //执行作业
            JobHandle handle = job.Schedule(maxIndex, 32);
            handle.Complete();
            //转换数据
            plate.dataBaking.vertexs = OutputVertexToDataPlateVertex(job.outputVertices);
            //释放
            job.input.points.Dispose();
            job.outputVertices.Dispose();
        }
        #endregion

        #region Jobs
        [BurstCompile]
        public struct JobVertex : IJobParallelFor {
            /// <summary> 输入 </summary>
            [ReadOnly] public InputInitialize input;
            /// <summary> 输出 </summary>
            public NativeArray<OutputVertex> outputVertices;

            public void Execute(int index) {
                Vector2Int v2 = IndexToXY(index, input.wide);
                Vector3 position = new Vector3(v2.x, v2.y) * input.interval + input.origin;
                OutputVertex vertex = new OutputVertex();
                vertex.isValid = FindPlateInside(input.points, position);
                vertex.position = position;
                outputVertices[index] = vertex;
            }
        }
        #endregion

        #region 转换
        private InputInitialize DataPlateToInputInitialize(DataPlate plate) {
            DataBorder border = plate.dataBaking.border;
            InputInitialize inputInitialize = new InputInitialize();
            inputInitialize.wide = border.GridWide;
            inputInitialize.interval = border.smooth;
            inputInitialize.origin = border.MinPoint;
            inputInitialize.points = new NativeArray<Vector3>(border.points, Allocator.TempJob);
            return inputInitialize;
        }
        private DataPlateVertex[] OutputVertexToDataPlateVertex(NativeArray<OutputVertex> outputVertices) {
            DataPlateVertex[] array = new DataPlateVertex[outputVertices.Length];
            for (int i = 0; i < outputVertices.Length; i++) {
                DataPlateVertex vertex = new DataPlateVertex();
                vertex.isValid = outputVertices[i].isValid;
                vertex.position = outputVertices[i].position;
                array[i] = vertex;
            }
            return array;
        }
        #endregion

        #region 输入
        public struct InputInitialize {
            /// <summary> 网格宽 </summary>
            public int wide;
            /// <summary> 网格间隔 </summary>
            public float interval;
            /// <summary> 原点 </summary>
            public Vector3 origin;
            /// <summary> 多边形边缘点 </summary>
            public NativeArray<Vector3> points;
        }
        #endregion

        #region 输出
        public struct OutputVertex {
            /// <summary> 是否是有效顶点 </summary>
            public bool isValid;
            /// <summary> 位置 </summary>
            public Vector3 position;
        }
        #endregion

    }
    #endregion

    #region 边缘细分
    public class SideSubdivision : UnitAlgorithm<DataPlate> {

        #region 执行
        public void Compute(DataPlate plate) {
            int count = plate.plateSides.Count;
            List<DataPlateSide> plateSides = plate.plateSides;
            //创建作业任务
            InputBorder border = DataPlateToInputBorder(plate);
            NativeArray<JobSideSubdivision> jobs = new NativeArray<JobSideSubdivision>(count, Allocator.Temp);
            NativeArray<JobHandle> handles = new NativeArray<JobHandle>(count, Allocator.Temp);
            for (int i = 0; i < count; i++) {
                jobs[i] = DataPlateSideToJobSideSubdivision(plateSides[i], border);
                handles[i] = jobs[i].Schedule();
            }
            //执行作业
            JobHandle.CompleteAll(handles);
            //转换数据
            for (int i = 0; i < jobs.Length; i++) {
                MergeSideVertex(jobs[i], plate);
            }
        }
        #endregion

        #region Jobs
        public struct JobSideSubdivision : IJob {
            /// <summary> 输入边界 </summary>
            [ReadOnly] public InputBorder inputBorder;
            /// <summary> 输入边线 </summary>
            [ReadOnly] public NativeArray<InputLine> inputLines;
            /// <summary> 输出边点 </summary>
            public NativeList<OutputSideVertex> outputSideVertexs;

            public void Execute() {
                for (int x = 0; x < inputBorder.wide; x++) {
                    Vector3 a = new Vector3(inputBorder.minX + x * inputBorder.interval, inputBorder.minY - 1);
                    Vector3 b = new Vector3(inputBorder.minX + x * inputBorder.interval, inputBorder.maxY + 1);
                    Subdivision(a, b);
                }
                for (int y = 0; y < inputBorder.high; y++) {
                    Vector3 a = new Vector3(inputBorder.minX - 1, inputBorder.minY + y * inputBorder.interval);
                    Vector3 b = new Vector3(inputBorder.maxX + 1, inputBorder.minY + y * inputBorder.interval);
                    Subdivision(a, b);
                }
            }
            public void Subdivision(Vector3 a, Vector3 b) {
                for (int i = 0; i < inputLines.Length; i++) {
                    InputLine line = inputLines[i];
                    if (!TryGetIntersectPoint(a, b, line.a, line.b, out Vector3 intersectPoint)) { continue; }
                    float distance = Vector3.Distance(line.a, intersectPoint) + line.origin;
                    OutputSideVertex sideVertex = new OutputSideVertex();
                    sideVertex.origin = distance;
                    sideVertex.position = intersectPoint;
                    outputSideVertexs.Add(sideVertex);
                }
            }
        }
        #endregion

        #region 转换
        private InputBorder DataPlateToInputBorder(DataPlate plate) {
            InputBorder inputBorder = new InputBorder();
            inputBorder.wide = plate.dataBaking.border.GridWide;
            inputBorder.high = plate.dataBaking.border.GridHigh;
            inputBorder.interval = plate.dataBaking.border.smooth;
            inputBorder.minX = plate.dataBaking.border.minX;
            inputBorder.maxX = plate.dataBaking.border.maxX;
            inputBorder.minY = plate.dataBaking.border.minY;
            inputBorder.maxY = plate.dataBaking.border.maxY;
            inputBorder.origin = plate.dataBaking.border.MinPoint;
            return inputBorder;
        }
        private JobSideSubdivision DataPlateSideToJobSideSubdivision(DataPlateSide plateSide, InputBorder border) {
            DataPlateLine[] lines = plateSide.dataBaking.lines;
            NativeArray<InputLine> inputLines = new NativeArray<InputLine>(lines.Length, Allocator.TempJob);
            for (int i = 0; i < lines.Length; i++) {
                InputLine line = new InputLine();
                line.a = lines[i].a;
                line.b = lines[i].b;
                line.origin = lines[i].origin;
                inputLines[i] = line;
            }

            JobSideSubdivision jobSideSubdivision = new JobSideSubdivision();
            jobSideSubdivision.inputBorder = border;
            jobSideSubdivision.inputLines = inputLines;
            jobSideSubdivision.outputSideVertexs = new NativeList<OutputSideVertex>(Allocator.TempJob);
            return jobSideSubdivision;
        }
        private void MergeSideVertex(JobSideSubdivision job, DataPlate plate) {
            DataPlateVertex[] plateVertexs = plate.dataBaking.vertexs;
            float interval = job.inputBorder.interval;
            Vector3 offset = job.inputBorder.origin - new Vector3(interval, interval, 0) * 0.1f;
            for (int i = 0; i < job.outputSideVertexs.Length; i++) {
                Vector3 position = job.outputSideVertexs[i].position;
                Vector3 gridPosition = position - offset;
                int vertexX = Mathf.FloorToInt(gridPosition.x / interval);
                int vertexY = Mathf.FloorToInt(gridPosition.y / interval);
                vertexX = Math.Clamp(vertexX, 0, job.inputBorder.wide - 1);
                vertexY = Math.Clamp(vertexY, 0, job.inputBorder.high - 1);
                int index = XYToIndex(new Vector2Int(vertexX, vertexY), job.inputBorder.wide);

                plateVertexs[index].isValid = true;
                plateVertexs[index].position = position;
            }
            job.inputLines.Dispose();
            job.outputSideVertexs.Dispose();
        }
        #endregion

        #region 输入
        public struct InputBorder {
            /// <summary> 网格宽 </summary>
            public int wide;
            /// <summary> 网格高 </summary>
            public int high;
            /// <summary> 网格间隔 </summary>
            public float interval;
            /// <summary> minX </summary>
            public float minX;
            /// <summary> maxX </summary>
            public float maxX;
            /// <summary> minY </summary>
            public float minY;
            /// <summary> maxY </summary>
            public float maxY;
            /// <summary> 原点 </summary>
            public Vector3 origin;
        }
        public struct InputLine {
            /// <summary> 线段起点a </summary>
            public Vector3 a;
            /// <summary> 线段终点b </summary>
            public Vector3 b;
            /// <summary> 原始距离 </summary>
            public float origin;
        }
        #endregion

        #region 输出
        public struct OutputSideVertex {
            /// <summary> 到边起点的距离 </summary>
            public float origin;
            /// <summary> 位置 </summary>
            public Vector3 position;
        }
        #endregion

    }
    #endregion

    #region 绘制三角形
    public class DrawTriangle : UnitAlgorithm<DataPlate> {

        #region 执行
        public void Compute(DataPlate plate) {
            DataPlateVertex[] vertexs = plate.dataBaking.vertexs;
            int maxIndex = vertexs.Length;
            DataBorder border = plate.dataBaking.border;
            //创建作业任务
            JobRhombus job = new JobRhombus();
            job.inputBorder = new InputBorder() { wide = border.GridWide, high = border.GridHigh };
            job.inputVertexs = DataPlateToInputVertex(vertexs);
            job.outputVertexs = new NativeArray<OutputVertex>(maxIndex, Allocator.TempJob);
            //执行作业
            JobHandle handle = job.Schedule(maxIndex, 32);
            handle.Complete();
            //三角形合并
            plate.dataBaking.mesh = new Mesh();
            plate.dataBaking.mesh.vertices = Vertices(job.outputVertexs);
            plate.dataBaking.mesh.uv = UV(job.outputVertexs);
            plate.dataBaking.mesh.triangles = Triangles(job.outputVertexs);
            plate.dataBaking.mesh.RecalculateBounds();
            plate.dataBaking.mesh.RecalculateNormals();
            //释放
            job.inputVertexs.Dispose();
            job.outputVertexs.Dispose();
        }
        #endregion

        #region Jobs
        [BurstCompile]
        public struct JobRhombus : IJobParallelFor {
            /// <summary> 输入边界 </summary>
            public InputBorder inputBorder;
            /// <summary> 输入网格 </summary>
            [ReadOnly] public NativeArray<InputVertex> inputVertexs;
            /// <summary> 输出顶点 </summary>
            public NativeArray<OutputVertex> outputVertexs;

            public struct ValidVertex {
                /// <summary> 数组索引 </summary>
                public int index;
                /// <summary> 是否是有效顶点 </summary>
                public bool isValid;
            }

            public void Execute(int index) {
                InputVertex input = inputVertexs[index];

                Vector2Int xy = IndexToXY(index, inputBorder.wide);

                ValidVertex above = TryVertex(xy.x, xy.y + 1, inputBorder.wide, inputBorder.high);
                ValidVertex below = TryVertex(xy.x, xy.y - 1, inputBorder.wide, inputBorder.high);
                ValidVertex lefts = TryVertex(xy.x - 1, xy.y, inputBorder.wide, inputBorder.high);
                ValidVertex right = TryVertex(xy.x + 1, xy.y, inputBorder.wide, inputBorder.high);

                ValidVertex leftsAbove = TryVertex(xy.x - 1, xy.y + 1, inputBorder.wide, inputBorder.high);
                ValidVertex leftsBelow = TryVertex(xy.x - 1, xy.y - 1, inputBorder.wide, inputBorder.high);
                ValidVertex rightAbove = TryVertex(xy.x + 1, xy.y + 1, inputBorder.wide, inputBorder.high);
                ValidVertex rightBelow = TryVertex(xy.x + 1, xy.y - 1, inputBorder.wide, inputBorder.high);

                OutputVertex output = new OutputVertex {
                    isValid = input.isValid,
                    position = input.position
                };

                //默认绘制左上角
                output.a = new OutputTriangle {
                    isValid = above.isValid && lefts.isValid,
                    a = input.index,
                    c = lefts.index,
                    b = above.index
                };
                //默认绘制右下角
                output.b = new OutputTriangle {
                    isValid = below.isValid && right.isValid,
                    a = input.index,
                    c = right.index,
                    b = below.index
                };
                //如果右上角点不存在，则尝试绘制右上角
                output.c = new OutputTriangle {
                    isValid = !rightAbove.isValid && above.isValid && right.isValid,
                    a = input.index,
                    c = above.index,
                    b = right.index
                };
                //如果左下角点不存在，则尝试绘制左下角
                output.d = new OutputTriangle {
                    isValid = !leftsBelow.isValid && below.isValid && lefts.isValid,
                    a = input.index,
                    c = below.index,
                    b = lefts.index
                };
                outputVertexs[index] = output;
            }
            public ValidVertex TryVertex(int x, int y, int wide, int high) {
                ValidVertex vertex = new ValidVertex() { index = -1, isValid = false };
                if (!TryXY(x, y, wide, high)) { return vertex; }
                int index = XYToIndex(new Vector2Int(x, y), wide);
                InputVertex input = inputVertexs[index];
                vertex.index = input.index;
                vertex.isValid = input.isValid;
                return vertex;
            }
        }
        #endregion

        #region 转换
        private NativeArray<InputVertex> DataPlateToInputVertex(DataPlateVertex[] vertexs) {
            NativeArray<InputVertex> InputVertexs = new NativeArray<InputVertex>(vertexs.Length, Allocator.TempJob);
            int index = 0;
            for (int i = 0; i < vertexs.Length; i++) {
                InputVertex vertex = new InputVertex();
                vertex.index = vertexs[i].isValid ? index : -1;
                vertex.isValid = vertexs[i].isValid;
                vertex.position = vertexs[i].position;
                InputVertexs[i] = vertex;
                if (vertexs[i].isValid) { index++; }
            }
            return InputVertexs;
        }
        private Vector3[] Vertices(NativeArray<OutputVertex> outputVertexs) {
            List<Vector3> Vertices = new List<Vector3>();
            for (int i = 0; i < outputVertexs.Length; i++) {
                if (!outputVertexs[i].isValid) { continue; }
                Vertices.Add(outputVertexs[i].position);
            }
            return Vertices.ToArray();
        }
        private Vector2[] UV(NativeArray<OutputVertex> outputVertexs) {
            //展开uv (顶点去掉z坐标就是未缩放的平面UV)
            List<Vector2> uv = new List<Vector2>();
            for (int i = 0; i < outputVertexs.Length; i++) {
                if (!outputVertexs[i].isValid) { continue; }
                uv.Add(new Vector2(outputVertexs[i].position.x, outputVertexs[i].position.y));
            }
            return uv.ToArray();
        }
        private int[] Triangles(NativeArray<OutputVertex> outputVertexs) {
            List<int> triangles = new List<int>();
            for (int i = 0; i < outputVertexs.Length; i++) {
                if (!outputVertexs[i].isValid) { continue; }
                triangles.AddRange(Triangles(outputVertexs[i]));
            }
            return triangles.ToArray();
        }
        private List<int> Triangles(OutputVertex vertex) {
            List<int> triangles = new List<int>();
            if (vertex.a.isValid) { triangles.AddRange(Triangles(vertex.a)); }
            if (vertex.b.isValid) { triangles.AddRange(Triangles(vertex.b)); }
            if (vertex.c.isValid) { triangles.AddRange(Triangles(vertex.c)); }
            if (vertex.d.isValid) { triangles.AddRange(Triangles(vertex.d)); }
            return triangles;
        }
        private List<int> Triangles(OutputTriangle triangle) {
            return new List<int>() { triangle.a, triangle.b, triangle.c };
        }
        #endregion

        #region 输入
        public struct InputBorder {
            /// <summary> 网格宽 </summary>
            public int wide;
            /// <summary> 网格高 </summary>
            public int high;
        }
        public struct InputVertex {
            /// <summary> 数组索引 </summary>
            public int index;
            /// <summary> 是否是有效顶点 </summary>
            public bool isValid;
            /// <summary> 设计视图中位置 </summary>
            public Vector3 position;
        }
        #endregion

        #region 输出
        public struct OutputVertex {
            /// <summary> 是否是有效 </summary>
            public bool isValid;
            /// <summary> 顶点位置 </summary>
            public Vector3 position;
            /// <summary> 左上角 </summary>
            public OutputTriangle a;
            /// <summary> 右下角 </summary>
            public OutputTriangle b;
            /// <summary> 右上角 </summary>
            public OutputTriangle c;
            /// <summary> 左下角 </summary>
            public OutputTriangle d;
        }
        public struct OutputTriangle {
            /// <summary> 是否是有效 </summary>
            public bool isValid;
            /// <summary> a点索引 </summary>
            public int a;
            /// <summary> b点索引 </summary>
            public int b;
            /// <summary> c点索引 </summary>
            public int c;
        }
        #endregion

    }
    #endregion
}
