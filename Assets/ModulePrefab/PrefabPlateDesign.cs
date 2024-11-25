using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPlateDesign : ModulePrefab<DataPlate> {

    private DataPlate plate;
    private MeshFilter meshFilter => GetComponent<MeshFilter>();
    private MeshCollider meshCollider => GetComponent<MeshCollider>();

    public override DataPlate Value => plate;

    public override void UpdateVisual(DataPlate plate) {
        this.plate = plate;
        transform.localPosition = plate.designPosition;
        meshFilter.mesh = plate.designMesh;
        meshCollider.sharedMesh = plate.designMesh;
    }

}
