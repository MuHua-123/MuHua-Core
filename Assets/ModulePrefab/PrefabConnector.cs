using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabConnector : ModulePrefab<DataConnector> {

    public Transform aPonit;
    public Transform bPoint;
    public LineRenderer lineRenderer;
    private DataConnector connector;

    public override DataConnector Value => connector;

    public override void UpdateVisual(DataConnector connector) {
        this.connector = connector;
        aPonit.localPosition = connector.aPoint;
        bPoint.localPosition = connector.bPoint;
        lineRenderer.SetPosition(0, connector.aPoint);
        lineRenderer.SetPosition(1, connector.bPoint);
    }

}
