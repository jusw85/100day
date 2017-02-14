using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Set script execution order to run after camera follow
// TODO:
// explore decay functions e.g. linear, smoothing (math.smoothstep), damping, perlin noise, etc.
// explore various shake types e.g. shaking camera rotation
public class CameraShake : MonoBehaviour {

    public float shakeIntensity = 0.3f;
    public bool runForever = false;
    public float duration = 0f;

    private void LateUpdate() {
        if (runForever) {
            Shake();
        } else if (duration > 0f) {
            duration -= Time.deltaTime;
            Shake();
        }
    }

    private void Shake() {
        transform.position = transform.position + Random.insideUnitSphere * shakeIntensity;
    }

}
