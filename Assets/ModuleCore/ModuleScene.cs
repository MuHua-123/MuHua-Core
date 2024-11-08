using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModuleScene : MonoBehaviour {
    protected virtual ModuleCore ModuleCore => ModuleCore.I;
    protected virtual void Awake() {
        //if (ModuleCore.ModuleScene != null) { Destroy(gameObject); return; }
        //ModuleCore.ModuleScene = this;
        //DontDestroyOnLoad(gameObject);
    }
    public virtual void LoadSceneAsync(string scene) {
        StartCoroutine(ILoadSceneAsync(scene));
    }
    public abstract IEnumerator ILoadSceneAsync(string scene);
}
