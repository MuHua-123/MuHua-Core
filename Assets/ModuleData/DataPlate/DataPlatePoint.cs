using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlatePoint {
    /// <summary> 绑定的板片 </summary>
    public readonly DataPlate plate;
    /// <summary> 初始化 </summary>
    public DataPlatePoint(DataPlate plate) => this.plate = plate;

    #region 核心数据
    /// <summary> 设计点位置(本地坐标系) </summary>
    public Vector3 position;
    #endregion

    #region 可视化数据
    /// <summary> 可视化对象 </summary>
    public ModulePrefab<DataPlatePoint> visual;
    #endregion
}
