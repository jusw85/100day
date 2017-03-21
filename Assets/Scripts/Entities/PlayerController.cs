using MonsterLove.StateMachine;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {

    private ControlManager controls;
    private Player player;
    private PlayerAnimator playerAnimator;
    private Animator fsm;

    //private StateMachine<PlayerState> fsm;

    private Vector2 moveInput;
    private bool isAttackQueued;

    private void Awake() {
        player = GetComponent<Player>();
        playerAnimator = GetComponent<PlayerAnimator>();
        fsm = GetComponent<Animator>();
        //fsm = StateMachine<PlayerState>.Initialize(this, PlayerState.Idle);
    }

    private void Start() {
        controls = Toolbox.RegisterComponent<ControlManager>();

        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
        if (cameraFollow != null) cameraFollow.target = gameObject;
    }

    private static int idleId = Animator.StringToHash("Base.Idle");
    private static int movementId = Animator.StringToHash("Base.Movement");
    private static int attack1Id = Animator.StringToHash("Base.Attack1");
    private static int isMovingId = Animator.StringToHash("isMoving");
    private static int triggerAttackId = Animator.StringToHash("triggerAttack");

    private void Update() {
        moveInput = controls.actions.Move;
        bool isAttackPressed = controls.actions.Attack.WasPressed;
        bool isAttackHeld = controls.actions.Attack.IsPressed;

        var sqrMagnitude = moveInput.sqrMagnitude;
        //var state = fsm.State;

        if (isAttackPressed) {
            isAttackQueued = true;
        }

        int currentState = fsm.GetCurrentAnimatorStateInfo(0).fullPathHash;
        if (currentState == idleId) {
            if (isAttackQueued) {
                player.Move(moveInput);
                isAttackQueued = false;
                fsm.SetTrigger(triggerAttackId);
            } else if (sqrMagnitude > 0) {
                player.Move(moveInput);
                fsm.SetBool(isMovingId, true);
            }
        } else if (currentState == movementId) {
            if (isAttackQueued) {
                player.Move(moveInput);
                isAttackQueued = false;
                fsm.SetTrigger(triggerAttackId);
            } else if (sqrMagnitude > 0) {
                player.Move(moveInput);
            } else if (sqrMagnitude <= 0) {
                fsm.SetBool(isMovingId, false);
            }
        } else if (currentState == attack1Id) {
        }

        //switch (state) {
        //    case PlayerState.Idle:
        //        if (isAttackQueued) {
        //            fsm.ChangeState(PlayerState.Attack1);
        //        } else if (sqrMagnitude > 0) {
        //            player.Move(moveInput);
        //            fsm.ChangeState(PlayerState.Walk);
        //        }
        //        break;
        //    case PlayerState.Walk:
        //        if (isAttackQueued) {
        //            fsm.ChangeState(PlayerState.Attack1);
        //        } else if (sqrMagnitude > 0) {
        //            player.Move(moveInput);
        //        } else if (sqrMagnitude <= 0) {
        //            fsm.ChangeState(PlayerState.Idle);
        //        }
        //        break;
        //    case PlayerState.Attack1:
        //        if (playerAnimator.isIdle) {
        //            fsm.ChangeState(PlayerState.Idle);
        //        }
        //        break;
        //}

        //playerAnimator.Animate(state, player);
        playerAnimator.Animate(player);
    }

    //private void Attack1_Enter() {
    //    player.Move(moveInput);
    //    isAttackQueued = false;
    //    playerAnimator.TriggerAttack();
    //}

}

//public enum PlayerState {
//    Idle,
//    Walk,
//    Attack1,
//}
