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
        DataPlateDesign design = plate.dataDesign;
        transform.localPosition = design.position;
        meshFilter.mesh = design.mesh;
        meshCollider.sharedMesh = design.mesh;
    }

}
