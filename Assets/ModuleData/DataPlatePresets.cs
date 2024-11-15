using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PresetsPlate", menuName = "数据模块/预设模板")]
public class DataPlatePresets : ScriptableObject {
    public List<Vector3> designPoints; 
}
