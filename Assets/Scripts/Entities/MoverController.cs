using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoverController : MonoBehaviour {

    public float moveSpeed = 0f;
    [NonSerialized]
    public Vector2 externalForce = Vector2.zero;

    private Vector2 moveDirection;
    public Vector2 MoveDirection {
        get {
            return moveDirection;
        }
        set {
            moveDirection = value;
            velocity = moveDirection.normalized * moveSpeed;
        }
    }

    private Vector2 velocity;
    private Rigidbody2D rigidBody;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        velocity += externalForce;
        rigidBody.velocity = velocity;
        externalForce = Vector2.zero;
    }

}
