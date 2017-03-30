using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //public GameObject level1;
    private ControlManager c;

    private void Awake() {
        //Toolbox.RegisterComponent<PoolManager>();
        //Toolbox.RegisterComponent<EventManager>();
        //Toolbox.RegisterComponent<ControlManager>();
        c = GetComponentInChildren<ControlManager>();
    }

    private void Start() {
        //Instantiate(level1);
    }

    private void Update() {
        pc.DoUpdate(c);
    }

    private PlayerController pc;
    public void RegisterPlayer(PlayerController pc) {
        this.pc = pc;
    }

}
