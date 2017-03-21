using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerAnimator : MonoBehaviour {

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private PlayerIdleStateMachineBehaviour playerIdleStateMachineBehaviour;

    private static int isMovingId = Animator.StringToHash("isMoving");
    private static int triggerAttackId = Animator.StringToHash("triggerAttack");
    private static int faceDirXId = Animator.StringToHash("faceDirX");
    private static int faceDirYId = Animator.StringToHash("faceDirY");

    [NonSerialized]
    public bool isIdle;

    private void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerIdleStateMachineBehaviour = animator.GetBehaviour<PlayerIdleStateMachineBehaviour>();
        playerIdleStateMachineBehaviour.playerAnimator = this;
    }

    //public void Animate(PlayerState state, Player player) {
    //    animator.SetFloat(faceDirXId, player.faceDir.x);
    //    animator.SetFloat(faceDirYId, player.faceDir.y);
    //    spriteRenderer.flipX = (player.faceDir.x <= 0);
    //    //animator.SetBool(isMovingId, false);
    //    //switch (state) {
    //    //    case PlayerState.Idle:
    //    //        break;
    //    //    case PlayerState.Walk:
    //    //        animator.SetBool(isMovingId, true);
    //    //        break;
    //    //    case PlayerState.Attack1:
    //    //        break;
    //    //}
    //}

    public void Animate(Player player) {
        animator.SetFloat(faceDirXId, player.faceDir.x);
        animator.SetFloat(faceDirYId, player.faceDir.y);
        spriteRenderer.flipX = (player.faceDir.x <= 0);
    }

    public void TriggerAttack() {
        animator.SetTrigger(triggerAttackId);
    }
}
