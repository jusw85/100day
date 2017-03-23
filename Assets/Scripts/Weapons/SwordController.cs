using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour {

    public float projectileForce = 50f;

    private void OnTriggerEnter2D(Collider2D other) {
        //string tag = other.gameObject.tag;
        //if (tag != "Player") {
        MoverController movable = other.transform.parent.gameObject.GetComponent<MoverController>();
        if (movable != null) {
            Transform otherTransform = other.transform.parent.transform;
            //Vector2 dir = transform.up.normalized;
            Vector3 dir = (otherTransform.position - transform.parent.position).normalized;
            movable.externalForce = dir * projectileForce;
        }
        IDamageable damageable = (IDamageable)other.transform.parent.gameObject.GetComponent(typeof(IDamageable));
        if (damageable != null) {
            damageable.Damage(gameObject);
        }
        //}

        //string tag = other.gameObject.tag;
        //if (tag == "enemy") {
        //    PoolObject poolObject = other.gameObject.GetComponent<PoolObject>();
        //    if (poolObject != null) {
        //        poolObject.Destroy();
        //    } else {
        //        Destroy(other.gameObject);
        //    }
        //}
    }
}
