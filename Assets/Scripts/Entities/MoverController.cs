using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoverController : MonoBehaviour {

    public Vector2 ExternalForce { get; set; }
    public bool ResetVelocity { get; set; }

    public float MoveSpeed { get; set; }

    private Vector2 moveDirection;
    public Vector2 MoveDirection {
        get {
            return moveDirection;
        }
        set {
            moveDirection = value;
            velocity = moveDirection.normalized * MoveSpeed;
        }
    }

    private Vector2 velocity;
    private Rigidbody2D rigidBody;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        ExternalForce = Vector2.zero;
        ResetVelocity = true;
    }

    private void FixedUpdate() {
        velocity += ExternalForce;
        rigidBody.velocity = velocity;

        if (ResetVelocity) velocity = Vector2.zero;
        ExternalForce = Vector2.zero;
    }

}
