using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerAnimator : MonoBehaviour {

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private static int isMovingId = Animator.StringToHash("isMoving");
    private static int faceDirXId = Animator.StringToHash("faceDirX");
    private static int faceDirYId = Animator.StringToHash("faceDirY");

    private void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Animate(PlayerState state, Player player) {
        animator.SetFloat(faceDirXId, player.faceDir.x);
        animator.SetFloat(faceDirYId, player.faceDir.y);
        spriteRenderer.flipX = (player.faceDir.x <= 0);
        switch (state) {
            case PlayerState.Idle:
                animator.SetBool(isMovingId, false);
                break;
            case PlayerState.Walk:
                animator.SetBool(isMovingId, true);
                break;
        }
    }
}
