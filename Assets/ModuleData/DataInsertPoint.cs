using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInsertPoint {
    /// <summary> 位置 (需要和点同一个坐标系) </summary>
    public Vector3 position;
    /// <summary> 执行操作的板片 </summary>
    public DataPlate plate;
    /// <summary> A点 </summary>
    public DataPoint aPoint;
    /// <summary> B点 </summary>
    public DataPoint bPoint;
    /// <summary> 关联的线段 </summary>
    public DataSide side;
}
