using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool enableFpsDisplay = true;
    public GameObject level1;

    private void Awake() {
        Toolbox.RegisterComponent<PoolManager>();
        Toolbox.RegisterComponent<EventManager>();
        Toolbox.RegisterComponent<ControlManager>();
    }

    private void Start() {
        if (enableFpsDisplay) {
            gameObject.AddChildComponent<FPSDisplay>();
        }
        Instantiate(level1);
    }
}
