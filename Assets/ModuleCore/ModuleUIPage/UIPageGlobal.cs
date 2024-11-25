using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPageGlobal : ModuleUIPage {
    protected override void Awake() => ModuleCore.GlobalPage = this;
}
