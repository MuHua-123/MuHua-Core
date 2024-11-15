using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInsertPoint {

    /// <summary> 有效的</summary>
    public bool IsValid => plate != null && aPoint != null && bPoint != null;

    #region 输入
    /// <summary> 位置 (需要和点同一个坐标系) </summary>
    public Vector3 position;
    /// <summary> 板片数据 </summary>
    public List<DataPlate> datas;
    #endregion

    #region 输出
    /// <summary> 执行操作的板片 </summary>
    public DataPlate plate;
    /// <summary> A点 </summary>
    public DataPoint aPoint;
    /// <summary> B点 </summary>
    public DataPoint bPoint;
    #endregion

}
