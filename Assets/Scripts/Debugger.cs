using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour {

    private HitboxVisualizer[] hitboxVisualizers;
    private bool toggleHitBoxVisualizers = true;

    public void Start() {
        hitboxVisualizers = FindObjectsOfType<HitboxVisualizer>();
        EnableHitboxes();
    }

    public void Update() {
    }


    public void OnGUI() {
        GUI.Box(new Rect(10, 10, 120, 70), "Debug Menu");

        toggleHitBoxVisualizers = GUI.Toggle(new Rect(20, 40, 100, 20), toggleHitBoxVisualizers, "Enable Hitbox");
        if (toggleHitBoxVisualizers) {
            ToggleHitboxes();
        }
    }

    public void ToggleHitboxes() {
        foreach (HitboxVisualizer hitboxVisualizer in hitboxVisualizers) {
            hitboxVisualizer.gameObject.SetActive(!hitboxVisualizer.gameObject.activeSelf);
        }
    }

    public void EnableHitboxes() {
        foreach (HitboxVisualizer hitboxVisualizer in hitboxVisualizers) {
            hitboxVisualizer.gameObject.SetActive(true);
        }
    }
}
