using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBeam : MonoBehaviour {

    public ParticleSystem sparks;

    private LineRenderer lineRenderer;
    private ParticleSystem sparksInstance;
    private float offset;
    private float offsetSpeed = 3f;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.sortingOrder = -1;
        lineRenderer.enabled = false;

        sparksInstance = Instantiate(sparks, Vector3.zero, Quaternion.Euler(-90, 90, 0));
        sparksInstance.Stop();
    }

    private void LateUpdate() {
        if (lineRenderer.enabled) {
            offset -= offsetSpeed * Time.deltaTime;
            offset = Mathf.Repeat(offset, 1);
            lineRenderer.material.mainTextureOffset = new Vector2(offset, 0);
            sparksInstance.transform.position = lineRenderer.GetPosition(1);
        }
    }

    private void OnDestroy() {
        if (sparksInstance != null) {
            Destroy(sparksInstance.gameObject);
        }
    }

    public void TurnOn(bool isEnabled) {
        lineRenderer.enabled = isEnabled;
        if (isEnabled) {
            sparksInstance.Play();
        } else {
            sparksInstance.Stop();
        }
    }

    public void SetPosition(Vector3 start, Vector3 end) {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}
