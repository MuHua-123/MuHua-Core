using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PresetsPlate", menuName = "数据模块/预设模板")]
public class DataPresetsPlate : ScriptableObject {
    public List<Vector2> designPoints;
    public DataPlate ToPlate() {
        DataPlate data = new DataPlate();
        data.designPoints = new List<DataDesignPoint>();
        int maxIndex = designPoints.Count;
        for (int i = 0; i < designPoints.Count; i++) {
            Vector2 position = designPoints[i];
            int left = DataPlateTool.NormalIndex(i + 1, maxIndex);
            int right = DataPlateTool.NormalIndex(i - 1, maxIndex);
            Vector2 leftBezier = (designPoints[left] - position) * 0.5f;
            Vector2 rightBezier = (designPoints[right] - position) * 0.5f;
            DataDesignPoint designPoint = CreateDataDesignPoint(i, position, data);
            designPoint.leftBezier = leftBezier;
            designPoint.rightBezier = rightBezier;
            data.designPoints.Add(designPoint);
        }
        return data;
    }
    private DataDesignPoint CreateDataDesignPoint(int index, Vector2 position, DataPlate data) {
        DataDesignPoint designPoint = new DataDesignPoint(data);
        designPoint.index = index;
        designPoint.postiton = position;
        return designPoint;
    }
}
