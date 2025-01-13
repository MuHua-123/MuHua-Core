using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTest : ModuleSingle<string> {
    // Start is called before the first frame update
    void Start() {
        I.Open("ss");
    }

    // Update is called once per frame
    void Update() {

    }
}
