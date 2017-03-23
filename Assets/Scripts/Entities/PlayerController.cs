using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {

    private ControlManager controls;
    private Player player;
    private PlayerAnimator playerAnimator;
    private Animator fsm;

    private Vector2 moveInput;
    private bool isAttackQueued;

    private void Awake() {
        player = GetComponent<Player>();
        playerAnimator = GetComponent<PlayerAnimator>();
        fsm = GetComponent<Animator>();
    }

    private void Start() {
        controls = Toolbox.RegisterComponent<ControlManager>();

        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
        if (cameraFollow != null) cameraFollow.target = gameObject;
    }

    private static int idleId = Animator.StringToHash("Base.Idle");
    private static int walkId = Animator.StringToHash("Base.Walk");
    private static int attack1Id = Animator.StringToHash("Base.Attack1");
    private static int attack2Id = Animator.StringToHash("Base.Attack2");
    private static int attack3Id = Animator.StringToHash("Base.Attack3");
    private static int isMovingId = Animator.StringToHash("isMoving");
    private static int triggerAttackId = Animator.StringToHash("triggerAttack");

    // 1. migrate to state machine behaviour?
    // 2. responsive controls i.e. direction change in middle of string, moving away in middle of string

    // mirror easy creation of animation
    // collider inconsistent
    // (only) queue attack at end of transition
    // sortorder based on y
    private void Update() {
        moveInput = controls.actions.Move;
        bool isAttackPressed = controls.actions.Attack.WasPressed;
        bool isAttackHeld = controls.actions.Attack.IsPressed;

        var sqrMagnitude = moveInput.sqrMagnitude;

        if (isAttackPressed) {
            isAttackQueued = true;
        }

        int currentState = fsm.GetCurrentAnimatorStateInfo(0).fullPathHash;
        if (currentState == idleId) {
            if (isAttackQueued) {
                if (sqrMagnitude > 0) player.Face(moveInput);
                isAttackQueued = false;
                fsm.SetTrigger(triggerAttackId);
            } else if (sqrMagnitude > 0) {
                player.Move(moveInput);
                fsm.SetBool(isMovingId, true);
            }
        } else if (currentState == walkId) {
            if (isAttackQueued) {
                if (sqrMagnitude > 0) player.Move(moveInput);
                isAttackQueued = false;
                fsm.SetTrigger(triggerAttackId);
            } else if (sqrMagnitude > 0) {
                player.Move(moveInput);
            } else if (sqrMagnitude <= 0) {
                fsm.SetBool(isMovingId, false);
            }
        } else if (currentState == attack1Id) {
            if (isAttackQueued) {
                isAttackQueued = false;
                fsm.SetTrigger(triggerAttackId);
            }
        } else if (currentState == attack2Id) {
            if (isAttackQueued) {
                isAttackQueued = false;
                fsm.SetTrigger(triggerAttackId);
            }
        } else if (currentState == attack3Id) {
            if (isAttackQueued) {
                isAttackQueued = false;
            }
        }

        playerAnimator.Animate(fsm, player);
    }

}
