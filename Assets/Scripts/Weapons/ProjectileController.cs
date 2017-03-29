using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileController : PoolObject {

    public float projectileForce = 10f;

    [System.NonSerialized]
    public float movementSpeed;
    public float destroyTime;

    private Rigidbody2D rigidBody;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        StartCoroutine(WaitAndDestroy());
    }

    private void FixedUpdate() {
        Vector2 moveVector = transform.up * Time.fixedDeltaTime * movementSpeed;
        rigidBody.MovePosition(rigidBody.position + moveVector);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        MoverController movable = other.transform.parent.gameObject.GetComponent<MoverController>();
        if (movable != null) {
            movable.ExternalForce = transform.up * projectileForce;
        }
        IDamageable damageable = (IDamageable)other.transform.parent.gameObject.GetComponent(typeof(IDamageable));
        if (damageable != null) {
            //damageable.Damage(gameObject);
        }
        Destroy();
    }

    private IEnumerator WaitAndDestroy() {
        yield return new WaitForSeconds(destroyTime);
        Destroy();
    }

    public override void OnObjectReuse() {

    }

}
