using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 查询边交点
/// </summary>
public class FindSideIntersectPoint : UnitFind<SideIntersectPoint> {
    public readonly float FindRange = 0.01f;
    /// <summary> 板片资产 </summary>
    public ModuleAssets<DataPlate> AssetsPlate => ModuleCore.I.AssetsPlate;

    /// <summary> 查询边交点 </summary>
    public FindSideIntersectPoint() { }

    public bool Find(Vector3 position, out SideIntersectPoint sip) {
        sip = new SideIntersectPoint();
        List<DataPlate> plates = AssetsPlate.Datas;
        for (int i = 0; i < plates.Count; i++) {
            Vector3 localPosition = position - plates[i].dataDesign.position;
            sip.side = Find(plates[i], localPosition, out sip.intersectPoint);
            sip.intersectPoint += plates[i].dataDesign.position;
            if (sip.side != null) { return true; }
        }
        return false;
    }
    /// <summary> 查询匹配的边 </summary>
    private DataPlateSide Find(DataPlate plate, Vector3 localPosition, out Vector3 intersectPoint) {
        intersectPoint = Vector3.zero;
        for (int i = 0; i < plate.plateSides.Count; i++) {
            DataPlateSide side = Find(plate.plateSides[i], localPosition, out intersectPoint);
            if (side != null) { return side; }
        }
        return null;
    }
    /// <summary> 查询匹配的边 </summary>
    private DataPlateSide Find(DataPlateSide side, Vector3 localPosition, out Vector3 intersectPoint) {
        intersectPoint = Vector3.zero;
        for (int i = 0; i < side.dataDesign.lines.Length; i++) {
            Vector3 a = side.dataDesign.lines[i].a;
            Vector3 b = side.dataDesign.lines[i].b;
            float distance = ProjectDistance(a, b, localPosition, out intersectPoint);
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
    public static float ProjectDistance(Vector3 a, Vector3 b, Vector3 c, out Vector3 intersectPoint) {
        Vector3 ab = b - a;
        Vector3 ac = c - a;
        Vector3 p = Vector3.Project(ac, ab);
        intersectPoint = p + a;
        if (ab.normalized != p.normalized) { return float.MaxValue; }
        if (ab.magnitude < p.magnitude) { return float.MaxValue; }
        return Vector3.Distance(c, p + a);
    }
}
/// <summary>
/// 边交点
/// </summary>
public class SideIntersectPoint {
    /// <summary> 匹配的边 </summary>
    public DataPlateSide side;
    /// <summary> 交点 </summary>
    public Vector3 intersectPoint;
}