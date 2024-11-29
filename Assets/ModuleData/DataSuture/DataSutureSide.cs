using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 缝合边
/// </summary>
public class DataSutureSide {
    /// <summary> 关联的边 </summary>
    public readonly DataPlateSide plateSide;
    /// <summary> 关联的缝合数据 </summary>
    public readonly DataSuture suture;
    /// <summary> 缝合边 </summary>
    public DataSutureSide(DataPlateSide side, DataSuture suture) {
        this.plateSide = side;
        this.suture = suture;
    }

    #region 核心数据
    /// <summary> 是否反转 </summary>
    public bool isReversal;
    #endregion

    #region 次要数据
    /// <summary> 设计缓存数据 </summary>
    public DataSutureSideDesign dataDesign = new DataSutureSideDesign();
    /// <summary> 烘焙缓存数据 </summary>
    public DataSutureSideBaking dataBaking = new DataSutureSideBaking();
    #endregion

    #region 可视化数据
    /// <summary> 可视化对象 </summary>
    public ModulePrefab<DataSutureSide> design;
    /// <summary> 可视化对象 </summary>
    public ModulePrefab<DataSutureSide> baking;
    #endregion
}
/// <summary> 设计缓存数据 </summary>
public class DataSutureSideDesign {
    /// <summary> 位置列表 </summary>
    public Vector3[] positions;
    /// <summary> A点位置 </summary>
    public Vector3 PointA => positions[0];
    /// <summary> B点位置 </summary>
    public Vector3 PointB => positions[positions.Length - 1];
}
/// <summary> 烘焙缓存数据 </summary>
public class DataSutureSideBaking {
    /// <summary> 位置列表 </summary>
    public Vector3[] positions;
    /// <summary> 全部顶点列表 </summary>
    public DataSutureSideVertex[] allVertexs;
    /// <summary> 关联的顶点列表 </summary>
    public DataSutureSideVertex[] vertexs;
    /// <summary> A点位置 </summary>
    public Vector3 PointA => positions[0];
    /// <summary> B点位置 </summary>
    public Vector3 PointB => positions[positions.Length - 1];
}
public class DataSutureSideVertex {
    /// <summary> 边起点到a点的距离 </summary>
    public float origin;
    /// <summary> a点 </summary>
    public DataPlateVertex a;
    /// <summary> a点的下一个点 </summary>
    public DataPlateVertex b;
    /// <summary> 缝合边顶点 </summary>
    public DataSutureSideVertex(float origin, DataPlateVertex a, DataPlateVertex b) {
        this.origin = origin;
        this.a = a;
        this.b = b;
    }
    /// <summary> 最大距离 origin + a - b </summary>
    public float MaxDistance => origin + Distance;
    /// <summary> 分段距离 a-b </summary>
    public float Distance => Vector3.Distance(a.position, b.position);
}