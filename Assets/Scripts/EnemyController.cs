using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoverController))]
public class EnemyController : MonoBehaviour {

    public GameObject followTarget;

    private MoverController moverController;

    private void Awake() {
        moverController = GetComponent<MoverController>();
    }

    private void Start() {
        followTarget = PlayerController.Instance.gameObject;
    }

    private void Update() {
        moverController.MoveDirection = Vector2.zero;

        if (followTarget != null) {
            var followVector = (followTarget.transform.position - transform.position);
            moverController.MoveDirection = followVector;
        }
    }

}
