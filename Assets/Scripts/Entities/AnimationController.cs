using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class AnimationController : MonoBehaviour {

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private static int idleStateId = Animator.StringToHash("Attack.Idle");
    private static int isAttackingId = Animator.StringToHash("isAttacking");
    private static int isMovingId = Animator.StringToHash("isMoving");
    private static int moveXId = Animator.StringToHash("moveX");
    private static int moveYId = Animator.StringToHash("moveY");
    private static int lastmoveXId = Animator.StringToHash("lastMoveX");
    private static int lastmoveYId = Animator.StringToHash("lastMoveY");

    private void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetIsFacingRight(bool isFacingRight) {
        spriteRenderer.flipX = !isFacingRight;
    }

    public void DoAttack(bool isAttacking) {
        animator.SetBool(isAttackingId, isAttacking);
    }

    public void SetIsMoving(bool isMoving) {
        animator.SetBool(isMovingId, isMoving);
    }

    public void SetMoveVector(Vector2 moveInput) {
        animator.SetFloat(moveXId, moveInput.x);
        animator.SetFloat(moveYId, moveInput.y);
    }

    public void SetLastMoveVector(Vector2 lastMoveInput) {
        animator.SetFloat(lastmoveXId, lastMoveInput.x);
        animator.SetFloat(lastmoveYId, lastMoveInput.y);
    }

    public bool IsIdle() {
        return animator.GetCurrentAnimatorStateInfo(1).fullPathHash == idleStateId;
    }

}
