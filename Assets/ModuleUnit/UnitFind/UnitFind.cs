using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UnitFind<Data> {
    /// <summary> 查询 </summary>
    public bool Find(Vector3 position, out Data data);
}
