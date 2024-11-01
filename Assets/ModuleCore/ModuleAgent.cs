using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 代理模块 </summary>
public abstract class ModuleAgent : MonoBehaviour {
    public abstract void AgentNetwork(DataNetwork data);
    public abstract void AgentLoadingNetwork(DataNetwork data);
}
