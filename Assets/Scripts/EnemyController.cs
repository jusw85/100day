using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoverController))]
public class EnemyController : MonoBehaviour {

    private MoverController moverController;

    private void Awake() {
        moverController = GetComponent<MoverController>();
    }

    private void Update() {
        moverController.MoveDirection = Vector2.zero;
        //moverController.MoveDirection = new Vector2(0, -2f); ;
    }

}
