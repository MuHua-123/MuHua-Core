using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModuleAgent : MonoBehaviour {
    protected abstract void Awake();

    protected virtual ModuleCore ModuleCore => ModuleCore.I;
}
