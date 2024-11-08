using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumeratorAgent : ModuleAgent {
    protected override void Awake() {
        ModuleCore.ModuleAgent = this;
    }
}
