using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool enableFpsDisplay = true;
    public GameObject level1;

    private void Start() {
        Toolbox.RegisterComponent<PoolManager>();
        if (enableFpsDisplay) {
            Toolbox.RegisterComponent<FPSDisplay>();
        }
        Instantiate(level1);
    }
}
