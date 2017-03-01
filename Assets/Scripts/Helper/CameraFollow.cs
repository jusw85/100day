using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject target;
    public float offsetX;
    public float offsetY;

    private Vector3 offset;

    private void Awake() {
        UpdateOffset();
    }

    private void LateUpdate() {
        transform.position = target.transform.position + offset;
    }

    private void OnValidate() {
        UpdateOffset();
    }

    private void UpdateOffset() {
        offset = new Vector3(offsetX, offsetY, -10);
    }

}
