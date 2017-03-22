using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerAnimator : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    private static int faceDirXId = Animator.StringToHash("faceDirX");
    private static int faceDirYId = Animator.StringToHash("faceDirY");

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Animate(Animator animator, Player player) {
        animator.SetFloat(faceDirXId, player.faceDir.x);
        animator.SetFloat(faceDirYId, player.faceDir.y);
        spriteRenderer.flipX = (player.faceDir.x < 0);
    }

}
