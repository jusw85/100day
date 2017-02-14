using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class AnimationController : MonoBehaviour {

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetIsFacingRight(bool isFacingRight) {
        spriteRenderer.flipX = !isFacingRight;
    }

    public void doAttack(bool isAttacking) {
        animator.SetBool("isAttacking", isAttacking);
    }

    public bool isIdle() {
        return animator.GetCurrentAnimatorStateInfo(1).IsName("Idle");
    }

}
