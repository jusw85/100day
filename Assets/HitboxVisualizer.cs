using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HitboxVisualizer : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        boxCollider2D = transform.parent.GetComponent<BoxCollider2D>();
    }

    private void LateUpdate() {
        spriteRenderer.enabled = boxCollider2D.enabled;
    }
    
}
