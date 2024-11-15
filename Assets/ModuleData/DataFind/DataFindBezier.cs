using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataFindBezier {

    /// <summary> 有效的点 </summary>
    public bool IsValid => plate != null && point != null;

    #region 输入
    /// <summary> 位置 (需要和点同一个坐标系) </summary>
    public Vector3 position;
    /// <summary> 板片数据 </summary>
    public List<DataPlate> datas;
    #endregion

    #region 输出
    /// <summary> 是前点(-) </summary>
    public bool isFront;
    /// <summary> 查询到的板片 </summary>
    public DataPlate plate;
    /// <summary> 查询到的点 </summary>
    public DataPoint point;
    #endregion
}
