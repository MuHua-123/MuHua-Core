using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumeratorAgent : ModuleAgent {
    public override void AgentLoadingNetwork(DataNetwork data) {
        StartCoroutine(data.IWebRequest());
    }
    public override void AgentNetwork(DataNetwork data) {
        StartCoroutine(data.IWebRequest());
    }
}
