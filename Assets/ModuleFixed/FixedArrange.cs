using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedArrange : ModuleFixed {
    public GameObject markPoints;
    private void Awake() {
        ModuleCore.OnBakingMobilePlate += ModuleCore_OnBakingMobilePlate;
    }
    private void OnDestroy() {
        ModuleCore.OnBakingMobilePlate -= ModuleCore_OnBakingMobilePlate;
    }
    private void ModuleCore_OnBakingMobilePlate(DataPlate obj) {
        markPoints.SetActive(obj != null);
    }
}
