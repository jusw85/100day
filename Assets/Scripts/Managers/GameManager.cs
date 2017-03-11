using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool enableFpsDisplay = true;
    public GameObject fpsDisplay;
    public GameObject poolManager;
    public GameObject level1;

    private void Start() {
        if (enableFpsDisplay) {
            Instantiate(fpsDisplay);
        }
        var obj = Instantiate(poolManager);
        obj.transform.SetParent(transform);
        Instantiate(level1);
    }

    // Update is called once per frame
    void Update() {

    }
}
