using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 缝合边
/// </summary>
public class DataSutureSide {
    /// <summary> 关联的边 </summary>
    public readonly DataSide side;
    /// <summary> 关联的缝合数据 </summary>
    public readonly DataSuture suture;
    /// <summary> 缝合边 </summary>
    public DataSutureSide(DataSide side, DataSuture suture) {
        this.side = side;
        this.suture = suture;
    }

    #region 核心数据
    /// <summary> 是否反转 </summary>
    public bool isReversal;
    #endregion

    #region 次要数据

    /// <summary> 边最大长度 </summary>
    public float MaxLength => side.length;
    /// <summary> 边缘顶点数据 </summary>
    public DataVertex[] Vertices => side.vertices;
    /// <summary> 设计视图板片位置 </summary>
    public Vector3 PlateDesignPosition => side.plate.designPosition;
    /// <summary> 烘焙视图板片位置 </summary>
    public Vector3 PlateBakingPosition => side.plate.bakingPosition;
    /// <summary> 烘焙视图板片位置 </summary>
    public Vector3 PlateBakingEulerAngles => side.plate.bakingEulerAngles;

    /// <summary> 设计视图顶点位置 </summary>
    public Vector3[] designPositions;
    /// <summary> 设计视图A点位置 </summary>
    public Vector3 DesignPosintA => designPositions[0];
    /// <summary> 设计视图B点位置 </summary>
    public Vector3 DesignPosintB => designPositions[designPositions.Length - 1];

    /// <summary> 烘焙视图顶点位置 </summary>
    public Vector3[] bakingPositions;
    /// <summary> 烘焙视图A点位置 </summary>
    public Vector3 BakingPosintA => bakingPositions[0];
    /// <summary> 烘焙视图B点位置 </summary>
    public Vector3 BakingPosintB => bakingPositions[bakingPositions.Length - 1];

    #endregion

    #region 可视化数据
    /// <summary> 可视化对象 </summary>
    public ModulePrefab<DataSutureSide> design;
    /// <summary> 可视化对象 </summary>
    public ModulePrefab<DataSutureSide> baking;
    #endregion
}
