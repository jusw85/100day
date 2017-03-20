using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    private ControlManager controls;
    private PlayerController player;

    private void Awake() {
        player = GetComponent<PlayerController>();
    }

    private void Start() {
        controls = Toolbox.RegisterComponent<ControlManager>();
    }

    private void Update() {
        Vector2 moveInput = controls.actions.Move;
        player.Move(moveInput);
    }
}