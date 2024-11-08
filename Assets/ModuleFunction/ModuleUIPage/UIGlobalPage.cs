using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGlobalPage : ModuleUIPage {
    protected override void Awake() {
        ModuleCore.FunctionRegister(this);
    }
}
