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
    private static readonly float Subdivide = 0.01f;
    private UnitAlgorithm<Polygon> sideBezier = new SideBezier();
    private UnitAlgorithm<Polygon> computeBorder = new ComputeBorder();
    private UnitAlgorithm<Polygon> vertexSideSubdivision = new VertexSideSubdivision();
    private UnitAlgorithm<Polygon> vertexInsideSubdivision = new VertexInsideSubdivision();
    private UnitAlgorithm<Polygon> vertexMergeSubdivision = new VertexMergeSubdivision();
    private UnitAlgorithm<Polygon> triangleDraw = new TriangleDraw();
    protected override void Awake() => ModuleCore.AlgorithmSubdivisionPolygon = this;

    public override void Compute(DataPlate plate) {
        Polygon polygon = To(plate);

        //Chronoscope("细分多边形计算耗时：", () => {
        //第一次计算边长
        sideBezier.Compute(polygon);
        //第二次计算细分边长
        sideBezier.Compute(polygon);
        //计算边界
        computeBorder.Compute(polygon);
        //计算边缘细分
        vertexSideSubdivision.Compute(polygon);
        //计算内部顶点细分
        vertexInsideSubdivision.Compute(polygon);
        //合并顶点
        vertexMergeSubdivision.Compute(polygon);
        //绘制三角形
        triangleDraw.Compute(polygon);
        //});

        List<DataPlateVertex> vertices = new List<DataPlateVertex>();
        List<Vector2> uv = new List<Vector2>();
        List<int> triangles = new List<int>();

        for (int i = 0; i < polygon.vertices.Length; i++) {
            PolygonVertex vertex = polygon.vertices[i];
            if (!vertex.isValid) { continue; }
            vertices.Add(ToData(vertex));
            uv.Add(new Vector2(vertex.position.x, vertex.position.y));
            triangles.AddRange(To(vertex));
        }

        plate.dataBaking.vertices = vertices.ToArray();
        plate.dataBaking.uv = uv.ToArray();
        plate.dataBaking.triangles = triangles.ToArray();
    }
    private DataPlateVertex ToData(PolygonVertex vertex) {
        DataPlateVertex plateVertex = new DataPlateVertex();
        plateVertex.position = vertex.position;
        return plateVertex;
    }
    private List<int> To(PolygonVertex vertex) {
        List<int> triangles = new List<int>();
        if (vertex.a.isValid) { triangles.AddRange(To(vertex.a)); }
        if (vertex.b.isValid) { triangles.AddRange(To(vertex.b)); }
        if (vertex.c.isValid) { triangles.AddRange(To(vertex.c)); }
        if (vertex.d.isValid) { triangles.AddRange(To(vertex.d)); }
        return triangles;
    }
    private List<int> To(PolygonTriangle triangle) {
        return new List<int>() { triangle.a, triangle.b, triangle.c };
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
    public class Polygon {
        /// <summary> 多边形边 </summary>
        public PolygonSide[] sides;
        /// <summary> 多边形边界 </summary>
        public PolygonBorder border;
        /// <summary> 网格顶点 </summary>
        public PolygonVertex[] vertices;
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
    public struct PolygonVertex {
        /// <summary> 数组索引 </summary>
        public int index;
        /// <summary> 是否是有效顶点 </summary>
        public bool isValid;
        /// <summary> 位置 </summary>
        public Vector3 position;
        /// <summary> 左上角 </summary>
        public PolygonTriangle a;
        /// <summary> 右下角 </summary>
        public PolygonTriangle b;
        /// <summary> 右上角 </summary>
        public PolygonTriangle c;
        /// <summary> 左下角 </summary>
        public PolygonTriangle d;
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
    public struct PolygonTriangle {
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

    #region 边缘贝塞尔曲线化
    public class SideBezier : UnitAlgorithm<Polygon> {

        #region 执行
        public void Compute(Polygon polygon) {
            //创建作业任务
            int count = polygon.sides.Length;
            NativeArray<JobSideBezier> jobs = new NativeArray<JobSideBezier>(count, Allocator.Temp);
            NativeArray<JobHandle> handles = new NativeArray<JobHandle>(count, Allocator.Temp);
            for (int i = 0; i < count; i++) {
                jobs[i] = To(polygon.sides[i]);
                handles[i] = jobs[i].Schedule();
            }
            //执行作业
            JobHandle.CompleteAll(handles);
            //转换数据
            for (int i = 0; i < count; i++) { polygon.sides[i] = To(jobs[i], polygon.sides[i]); }
        }
        #endregion

        #region 转换
        /// <summary> 转换作业数据 </summary>
        private JobSideBezier To(PolygonSide polygonSide) {
            JobSideBezier job = new JobSideBezier();
            job.quotient = polygonSide.quotient;
            job.bezier = polygonSide.bezier;
            job.aPoint = polygonSide.aPoint;
            job.bPoint = polygonSide.bPoint;
            job.aBezier = polygonSide.aBezier;
            job.bBezier = polygonSide.bBezier;

            int quotient = polygonSide.quotient;
            job.length = new NativeArray<float>(1, Allocator.TempJob);
            job.positions = new NativeArray<Vector3>(quotient, Allocator.TempJob);
            job.lines = new NativeArray<PolygonLine>(quotient - 1, Allocator.TempJob);
            return job;
        }
        /// <summary> 转换托管数据 </summary>
        private PolygonSide To(JobSideBezier job, PolygonSide polygonSide) {
            polygonSide.quotient = (int)(job.length[0] / Subdivide);
            polygonSide.length = job.length[0];
            polygonSide.positions = job.positions.ToArray();
            polygonSide.lines = job.lines.ToArray();
            job.length.Dispose();
            job.positions.Dispose();
            job.lines.Dispose();
            return polygonSide;
        }
        #endregion

        #region Jobs
        [BurstCompile]
        public struct JobSideBezier : IJob {
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

    #region 计算边界
    public class ComputeBorder : UnitAlgorithm<Polygon> {

        #region 执行
        public void Compute(Polygon polygon) {
            //创建作业任务
            JobComputeBorder job = To(polygon);
            int count = job.positions.Length;
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
        private JobComputeBorder To(Polygon polygon) {
            List<Vector3> positions = new List<Vector3>();
            for (int i = 0; i < polygon.sides.Length; i++) {
                positions.AddRange(polygon.sides[i].positions);
            }

            JobComputeBorder job = new JobComputeBorder();
            job.border = new NativeArray<float>(4, Allocator.TempJob);
            job.positions = new NativeArray<Vector3>(positions.ToArray(), Allocator.TempJob);
            return job;
        }
        private PolygonBorder To(JobComputeBorder job) {
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
        public struct JobComputeBorder : IJobFor {
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
    public class VertexSideSubdivision : UnitAlgorithm<Polygon> {

        #region 执行
        public void Compute(Polygon polygon) {
            //创建作业任务
            int count = polygon.sides.Length;
            NativeArray<JobVertexSideSubdivision> jobs = new NativeArray<JobVertexSideSubdivision>(count, Allocator.Temp);
            NativeArray<JobHandle> handles = new NativeArray<JobHandle>(count, Allocator.Temp);
            for (int i = 0; i < count; i++) {
                jobs[i] = To(polygon, polygon.sides[i]);
                handles[i] = jobs[i].Schedule();
            }
            //执行作业
            JobHandle.CompleteAll(handles);
            //转换数据
            for (int i = 0; i < count; i++) { polygon.sides[i] = To(jobs[i], polygon.sides[i]); }
        }
        #endregion

        #region 转换
        private JobVertexSideSubdivision To(Polygon polygon, PolygonSide polygonSide) {
            JobVertexSideSubdivision job = new JobVertexSideSubdivision();
            job.gridWide = polygon.border.GridWide;
            job.gridHigh = polygon.border.GridHigh;
            job.interval = Subdivide;
            job.minX = polygon.border.minX;
            job.maxX = polygon.border.maxX;
            job.minY = polygon.border.minY;
            job.maxY = polygon.border.maxY;
            job.minPoint = polygon.border.MinPoint;
            job.lines = new NativeArray<PolygonLine>(polygonSide.lines, Allocator.TempJob);
            job.vertexs = new NativeList<PolygonSideVertex>(Allocator.TempJob);
            return job;
        }
        /// <summary> 转换托管数据 </summary>
        private PolygonSide To(JobVertexSideSubdivision job, PolygonSide polygonSide) {
            polygonSide.vertexs = job.vertexs.ToArray();
            job.lines.Dispose();
            job.vertexs.Dispose();
            return polygonSide;
        }
        #endregion

        #region Jobs
        [BurstCompile]
        public struct JobVertexSideSubdivision : IJob {
            /// <summary> 网格宽 </summary>
            [ReadOnly] public int gridWide;
            /// <summary> 网格高 </summary>
            [ReadOnly] public int gridHigh;
            /// <summary> 网格间隔 </summary>
            [ReadOnly] public float interval;
            /// <summary> minX </summary>
            [ReadOnly] public float minX;
            /// <summary> maxX </summary>
            [ReadOnly] public float maxX;
            /// <summary> minY </summary>
            [ReadOnly] public float minY;
            /// <summary> maxY </summary>
            [ReadOnly] public float maxY;
            /// <summary> 原点 </summary>
            [ReadOnly] public Vector3 minPoint;
            /// <summary> 输入边线 </summary>
            [ReadOnly] public NativeArray<PolygonLine> lines;

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
                for (int i = 1; i < vertexs.Length; i++) {
                    PolygonSideVertex temp = vertexs[i];
                    for (int j = i - 1; j >= 0; j--) {
                        if (vertexs[j].origin > temp.origin) {
                            vertexs[j + 1] = vertexs[j];
                            vertexs[j] = temp;
                        }
                    }
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
    public class VertexInsideSubdivision : UnitAlgorithm<Polygon> {

        #region 执行
        public void Compute(Polygon polygon) {
            //创建作业任务
            int maxIndex = polygon.border.GridWide * polygon.border.GridHigh;
            JobVertexInsideSubdivision job = To(polygon);
            //执行作业
            JobHandle handle = job.Schedule(maxIndex, 32);
            handle.Complete();
            //转换数据
            polygon.vertices = job.vertices.ToArray();
            //释放
            job.positions.Dispose();
            job.vertices.Dispose();
        }
        #endregion

        #region 转换
        private JobVertexInsideSubdivision To(Polygon polygon) {
            int maxIndex = polygon.border.GridWide * polygon.border.GridHigh;
            JobVertexInsideSubdivision job = new JobVertexInsideSubdivision();
            job.gridWide = polygon.border.GridWide;
            job.minPoint = polygon.border.MinPoint;
            job.positions = new NativeArray<Vector3>(polygon.border.positions, Allocator.TempJob);
            job.vertices = new NativeArray<PolygonVertex>(maxIndex, Allocator.TempJob);
            return job;
        }
        #endregion

        #region Jobs
        [BurstCompile]
        public struct JobVertexInsideSubdivision : IJobParallelFor {
            /// <summary> 网格宽 </summary>
            [ReadOnly] public int gridWide;
            /// <summary> 网格起点 </summary>
            [ReadOnly] public Vector3 minPoint;
            /// <summary> 顶点列表 </summary>
            [ReadOnly] public NativeArray<Vector3> positions;
            /// <summary> 输出 </summary>
            public NativeArray<PolygonVertex> vertices;

            public void Execute(int index) {
                Vector2Int v2 = IndexToXY(index, gridWide);
                Vector3 position = new Vector3(v2.x, v2.y) * Subdivide + minPoint;
                PolygonVertex vertex = new PolygonVertex();
                vertex.isValid = FindPlateInside(positions, position);
                vertex.position = position;
                vertices[index] = vertex;
            }
        }
        #endregion

    }
    #endregion

    #region 合并细分顶点
    public class VertexMergeSubdivision : UnitAlgorithm<Polygon> {

        #region 执行
        public void Compute(Polygon polygon) {
            //创建作业任务
            JobVertexMergeSubdivision job = To(polygon);
            int count = job.sideVertices.Length;
            //执行作业
            JobHandle dependency = new JobHandle();
            JobHandle handle = job.Schedule(count, dependency);
            handle.Complete();
            //转换数据
            polygon.vertices = job.vertices.ToArray();
            //释放
            job.sideVertices.Dispose();
            job.vertices.Dispose();
        }
        #endregion

        #region 转换
        private JobVertexMergeSubdivision To(Polygon polygon) {
            List<PolygonSideVertex> sideVertices = new List<PolygonSideVertex>();
            for (int i = 0; i < polygon.sides.Length; i++) {
                sideVertices.AddRange(polygon.sides[i].vertexs);
            }

            JobVertexMergeSubdivision job = new JobVertexMergeSubdivision();
            job.offset = polygon.border.MinPoint - new Vector3(Subdivide, Subdivide, 0) * 0.1f;
            job.interval = Subdivide;
            job.GridWide = polygon.border.GridWide;
            job.GridHigh = polygon.border.GridHigh;
            job.sideVertices = new NativeArray<PolygonSideVertex>(sideVertices.ToArray(), Allocator.TempJob);
            job.vertices = new NativeArray<PolygonVertex>(polygon.vertices, Allocator.TempJob);
            return job;
        }
        #endregion

        #region Jobs
        [BurstCompile]
        public struct JobVertexMergeSubdivision : IJobFor {
            /// <summary> 偏移 </summary>
            [ReadOnly] public Vector3 offset;
            /// <summary> 间隔 </summary>
            [ReadOnly] public float interval;
            /// <summary> 网格宽 </summary>
            [ReadOnly] public int GridWide;
            /// <summary> 网格高 </summary>
            [ReadOnly] public int GridHigh;
            /// <summary> 边点 </summary>
            [ReadOnly] public NativeArray<PolygonSideVertex> sideVertices;
            /// <summary> 网格顶点 </summary>
            [NativeDisableParallelForRestriction]
            public NativeArray<PolygonVertex> vertices;

            public void Execute(int index) {
                Vector3 position = sideVertices[index].position;
                Vector3 gridPosition = position - offset;
                int vertexX = Mathf.FloorToInt(gridPosition.x / interval);
                int vertexY = Mathf.FloorToInt(gridPosition.y / interval);
                vertexX = Math.Clamp(vertexX, 0, GridWide - 1);
                vertexY = Math.Clamp(vertexY, 0, GridHigh - 1);
                int i = XYToIndex(new Vector2Int(vertexX, vertexY), GridWide);

                PolygonVertex vertex = vertices[i];
                vertex.isValid = true;
                vertex.position = position;
                vertices[i] = vertex;
            }
        }
        #endregion

    }
    #endregion

    #region 绘制三角形
    public class TriangleDraw : UnitAlgorithm<Polygon> {

        #region 执行
        public void Compute(Polygon polygon) {
            int maxIndex = polygon.vertices.Length;
            //创建作业任务
            JobTriangleDraw job = To(polygon);
            //执行作业
            JobHandle handle = job.Schedule(maxIndex, 32);
            handle.Complete();
            //转换数据
            polygon.vertices = job.output.ToArray();
            //释放
            job.vertices.Dispose();
            job.output.Dispose();
        }
        #endregion

        #region 转换
        private JobTriangleDraw To(Polygon polygon) {
            int index = 0;
            for (int i = 0; i < polygon.vertices.Length; i++) {
                PolygonVertex vertex = polygon.vertices[i];
                vertex.index = vertex.isValid ? index : -1;
                if (vertex.isValid) { index++; }
                polygon.vertices[i] = vertex;
            }

            JobTriangleDraw job = new JobTriangleDraw();
            job.gridWide = polygon.border.GridWide;
            job.gridHigh = polygon.border.GridHigh;
            job.vertices = new NativeArray<PolygonVertex>(polygon.vertices, Allocator.TempJob);
            job.output = new NativeArray<PolygonVertex>(polygon.vertices, Allocator.TempJob);
            return job;
        }
        #endregion

        #region Jobs
        [BurstCompile]
        public struct JobTriangleDraw : IJobParallelFor {
            /// <summary> 网格宽 </summary>
            [ReadOnly] public int gridWide;
            /// <summary> 网格高 </summary>
            [ReadOnly] public int gridHigh;
            /// <summary> 网格顶点 </summary>
            [ReadOnly] public NativeArray<PolygonVertex> vertices;
            /// <summary> 输出 </summary>
            public NativeArray<PolygonVertex> output;

            public struct ValidVertex {
                /// <summary> 数组索引 </summary>
                public int index;
                /// <summary> 是否是有效顶点 </summary>
                public bool isValid;
            }

            public void Execute(int index) {
                PolygonVertex vertex = vertices[index];

                Vector2Int xy = IndexToXY(index, gridWide);

                ValidVertex above = TryVertex(xy.x, xy.y + 1);
                ValidVertex below = TryVertex(xy.x, xy.y - 1);
                ValidVertex lefts = TryVertex(xy.x - 1, xy.y);
                ValidVertex right = TryVertex(xy.x + 1, xy.y);

                ValidVertex leftsAbove = TryVertex(xy.x - 1, xy.y + 1);
                ValidVertex leftsBelow = TryVertex(xy.x - 1, xy.y - 1);
                ValidVertex rightAbove = TryVertex(xy.x + 1, xy.y + 1);
                ValidVertex rightBelow = TryVertex(xy.x + 1, xy.y - 1);

                //默认绘制左上角
                vertex.a = new PolygonTriangle {
                    isValid = above.isValid && lefts.isValid,
                    a = vertex.index,
                    b = lefts.index,
                    c = above.index
                };
                //默认绘制右下角
                vertex.b = new PolygonTriangle {
                    isValid = below.isValid && right.isValid,
                    a = vertex.index,
                    b = right.index,
                    c = below.index
                };
                //如果右上角点不存在，则尝试绘制右上角
                vertex.c = new PolygonTriangle {
                    isValid = !rightAbove.isValid && above.isValid && right.isValid,
                    a = vertex.index,
                    b = above.index,
                    c = right.index
                };
                //如果左下角点不存在，则尝试绘制左下角
                vertex.d = new PolygonTriangle {
                    isValid = !leftsBelow.isValid && below.isValid && lefts.isValid,
                    a = vertex.index,
                    b = below.index,
                    c = lefts.index
                };
                output[index] = vertex;
            }
            public ValidVertex TryVertex(int x, int y) {
                ValidVertex validVertex = new ValidVertex() { index = -1, isValid = false };
                if (!TryXY(x, y, gridWide, gridHigh)) { return validVertex; }
                int index = XYToIndex(new Vector2Int(x, y), gridWide);
                PolygonVertex vertex = vertices[index];
                validVertex.index = vertex.index;
                validVertex.isValid = vertex.isValid;
                return validVertex;
            }
        }
        #endregion

    }
    #endregion

}

