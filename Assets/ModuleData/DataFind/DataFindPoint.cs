using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataFindPoint {

    /// <summary> 有效的板片 </summary>
    public bool IsValidPlate => plate != null;
    /// <summary> 有效的点 </summary>
    public bool IsValidPoint => point != null;

    #region 输入
    /// <summary> 位置 (需要和点同一个坐标系) </summary>
    public Vector3 position;
    /// <summary> 板片数据 </summary>
    public List<DataPlate> datas;
    #endregion

    #region 输出
    /// <summary> 查询到的板片 </summary>
    public DataPlate plate;
    /// <summary> 查询到的点 </summary>
    public DataPoint point;
    #endregion
}
