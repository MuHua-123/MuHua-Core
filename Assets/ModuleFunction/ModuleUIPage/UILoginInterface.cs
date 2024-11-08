using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoginInterface : ModuleUIPage {
    protected override void Awake() {
        ModuleCore.FunctionRegister(this);
    }
}
