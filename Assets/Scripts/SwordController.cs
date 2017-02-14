using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour {

    private void Start() {

    }

    private void Update() {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        string tag = other.gameObject.tag;
        if (tag == "enemy") {
            PoolObject poolObject = other.gameObject.GetComponent<PoolObject>();
            if (poolObject != null) {
                poolObject.Destroy();
            } else {
                Destroy(other.gameObject);
            }
        }
    }
}
