using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 查找边
/// </summary>
public class FindSide : ModuleFind<DataSide> {
    public readonly float FindRange = 0.01f; 
    /// <summary> 板片资产 </summary>
    public ModuleAssets<DataPlate> AssetsPlate => ModuleCore.AssetsPlate;

    protected override void Awake() => ModuleCore.FindSide = this;

    public override bool Find(Vector3 position, out DataSide side) {
        List<DataPlate> plates = AssetsPlate.Datas;
        for (int i = 0; i < plates.Count; i++) {
            Vector3 localPosition = position - plates[i].designPosition;
            side = Find(plates[i], localPosition);
            if (side != null) { return true; }
        }
        side = null; return false;
    }

    /// <summary> 查询匹配的边 </summary>
    private DataSide Find(DataPlate plate, Vector3 localPosition) {
        for (int i = 0; i < plate.sides.Count; i++) {
            DataSide side = Find(plate.sides[i], localPosition);
            if (side != null) { return side; }
        }
        return null;
    }
    /// <summary> 查询匹配的边 </summary>
    private DataSide Find(DataSide side, Vector3 localPosition) {
        for (int i = 0; i < side.lines.Length; i++) {
            Vector3 a = side.lines[i].a;
            Vector3 b = side.lines[i].b;
            float distance = ProjectDistance(a, b, localPosition);
            //Debug.Log($"{a} , {b} , {localPosition}");
            if (distance < FindRange) { return side; }
        }
        return null;
    }

    /// <summary>
    /// 向量投影法
    /// 计算点c到线段ab最近的点
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns>如果不在线段上返回 float.MaxValue</returns>
    public static float ProjectDistance(Vector3 a, Vector3 b, Vector3 c) {
        Vector3 ab = b - a;
        Vector3 ac = c - a;
        Vector3 p = Vector3.Project(ac, ab);
        if (ab.normalized != p.normalized) { return float.MaxValue; }
        if (ab.magnitude < p.magnitude) { return float.MaxValue; }
        return Vector3.Distance(c, p + a);
    }
}
