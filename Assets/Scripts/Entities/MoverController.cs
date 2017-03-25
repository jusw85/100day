using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoverController : MonoBehaviour {

    [NonSerialized]
    public Vector2 externalForce = Vector2.zero;
    [NonSerialized]
    public bool resetVelocity = true;


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
    }

    private void FixedUpdate() {
        velocity += externalForce;
        rigidBody.velocity = velocity;

        if (resetVelocity) velocity = Vector2.zero;
        externalForce = Vector2.zero;
    }

}
