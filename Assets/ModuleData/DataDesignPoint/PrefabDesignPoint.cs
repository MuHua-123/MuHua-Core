using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

public class PrefabDesignPoint : MonoBehaviour, ITemplate<DataDesignPoint> {
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;

    public PrefabBezierPoint bezierPoint1;
    public PrefabBezierPoint bezierPoint2;

    private DataDesignPoint value;
    public int Index => value.index;
    public Vector2 Position => value.postiton;
    public List<Vector2> EdgePoints => value.edgePoints;
    public DataPlate DataPlate => value.dataPlate;
    public int MaxIndex => DataPlate.designPoints.Count;
    public int NextIndex => DataPlateTool.NormalIndex(Index + 1, MaxIndex);
    public void SetValue(DataDesignPoint value) {
        this.value = value;
        bezierPoint1.SetValue(value, (obj) => { value.leftBezier = obj; });
        bezierPoint2.SetValue(value, (obj) => { value.rightBezier = obj; });
        DataPlate.OnChangeDesignPoint += DataPlate_OnChangeDesignPoint;
        DataPlate_OnChangeDesignPoint(Index);
    }
    private void OnDestroy() {
        DataPlate.OnChangeDesignPoint -= DataPlate_OnChangeDesignPoint;
    }
    private void DataPlate_OnChangeDesignPoint(int index) {
        if (index != Index) { return; }
        transform.localPosition = Position;
        //添加全部点
        int maxIndex = EdgePoints.Count + 1;
        lineRenderer.positionCount = maxIndex;
        Vector2[] vectors = new Vector2[maxIndex];
        for (int i = 0; i < EdgePoints.Count; i++) {
            Vector2 position = EdgePoints[i] - Position;
            lineRenderer.SetPosition(i, position);
            vectors[i] = position;
        }
        //添加最后一个点
        int last = maxIndex - 1;
        DataDesignPoint nextDesignPoint = DataPlate.FindDesignPoint(NextIndex);
        Vector2 position2 = nextDesignPoint.postiton - Position;
        lineRenderer.SetPosition(last, position2);
        vectors[last] = position2;
        //更新2D线段碰撞器
        edgeCollider.points = vectors;
        //更新贝塞尔曲线
        bezierPoint1.SetPosition(value.leftBezier);
        bezierPoint2.SetPosition(value.rightBezier);
    }
}
