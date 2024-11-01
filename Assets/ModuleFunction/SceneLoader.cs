using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MuHua;

public class SceneLoader : ModuleScene {
    public Slider progressBar;
    public override IEnumerator ILoadSceneAsync(string scene) {
        int disableProgress = 0;
        int toProgress = 0;
        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;
        transform.SonActive(true);
        while (ao.progress < 0.9f) {
            toProgress = (int)(ao.progress * 100);
            while (disableProgress < toProgress) {
                ++disableProgress;
                progressBar.value = disableProgress / 100.0f;//0.01开始
                yield return new WaitForEndOfFrame();
            }
        }
        toProgress = 100;
        while (disableProgress < toProgress) {
            ++disableProgress;
            progressBar.value = disableProgress / 100.0f;
            yield return new WaitForEndOfFrame();
        }
        ao.allowSceneActivation = true;
        while (!ao.isDone) {
            yield return new WaitForEndOfFrame();
        }
        transform.SonActive(false);
    }
}
