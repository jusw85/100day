using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool enableFpsDisplay = true;
    public GameObject level1;

    private void Start() {
        Toolbox.RegisterComponent<PoolManager>();
        Toolbox.RegisterComponent<EventManager>();
        Toolbox.RegisterComponent<ControlManager>();
        if (enableFpsDisplay) {
            gameObject.AddChildComponent<FPSDisplay>();
        }
        Instantiate(level1);
    }
}
