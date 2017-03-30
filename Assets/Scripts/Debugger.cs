using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour {

    private BoxCollider2DVisualizer[] boxCollider2DVisualizers;
    private bool toggleHitBoxVisualizers = true;
    public bool enableFpsDisplay = true;

    public void Start() {
        boxCollider2DVisualizers = FindObjectsOfType<BoxCollider2DVisualizer>();
        EnableHitboxes();

        if (enableFpsDisplay) {
            gameObject.AddChildComponent<FPSDisplay>();
        }
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
        foreach (BoxCollider2DVisualizer boxCollider2DVisualizer in boxCollider2DVisualizers) {
            boxCollider2DVisualizer.gameObject.SetActive(!boxCollider2DVisualizer.gameObject.activeSelf);
        }
    }

    public void EnableHitboxes() {
        foreach (BoxCollider2DVisualizer hitboxVisualizer in boxCollider2DVisualizers) {
            hitboxVisualizer.gameObject.SetActive(true);
        }
    }
}
