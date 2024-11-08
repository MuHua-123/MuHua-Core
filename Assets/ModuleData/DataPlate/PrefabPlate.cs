using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

public class PrefabPlate : MonoBehaviour, ITemplate<DataPlate> {
    public Transform DesignPointParent;
    public Transform DesignPointTemplate;
    public Transform PlateEdgeParent;
    public Transform PlateEdgeTemplate;

    private DataPlate value;
    private Vector3 localPosition;

    public MeshFilter MeshFilter => GetComponent<MeshFilter>();
    public MeshCollider MeshCollider => GetComponent<MeshCollider>();
    public ModuleViewCamera viewCamera => ModuleCore.I.PlateDesignViewCamera;
    public void SetValue(DataPlate value) {
        this.value = value;
        localPosition = viewCamera.CurrentViewSpaceCenter;
        value.OnChange += DataPlate_OnChange;
        value.Compute();
    }
    private void OnDestroy() {
        value.OnChange -= DataPlate_OnChange;
    }
    public void DataPlate_OnChange() {
        CreateDesignPoint();
        //CreatePrefabEdgePoint();
        CreatePolygonMesh();
        //重置坐标
        transform.localPosition = localPosition + value.centerOffset;
        localPosition = transform.localPosition;
    }
    /// <summary> 生成设计点 </summary>
    private void CreateDesignPoint() {
        DesignPointParent.Instantiate(DesignPointTemplate, value.designPoints);
    }
    /// <summary> 生成边缘点 </summary>
    private void CreatePrefabEdgePoint() {
        PlateEdgeParent.DestroySon(PlateEdgeTemplate);
        for (int i = 0; i < value.edgePoints.Count; i++) {
            Transform temp = Instantiate(PlateEdgeTemplate, PlateEdgeParent);
            temp.gameObject.SetActive(true);
            PrefabPlateEdge plateEdge = temp.GetComponent<PrefabPlateEdge>();
            plateEdge.SetValue(i, value);
        }
    }
    /// <summary> 生成网格 </summary>
    private void CreatePolygonMesh() {
        Mesh mesh = new Mesh();
        mesh.vertices = value.vertices.ToArray();
        mesh.uv = value.uv.ToArray();
        mesh.triangles = value.triangles.ToArray();
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        MeshFilter.mesh = mesh;
        MeshCollider.sharedMesh = mesh;
    }
}
