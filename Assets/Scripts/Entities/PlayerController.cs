using System.Collections.Generic;
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

    private int prevStateHash;
    private int prevFrameStateHash;

    private void Awake() {
        player = GetComponent<Player>();
        playerAnimator = GetComponent<PlayerAnimator>();
        fsm = GetComponent<Animator>();

        prevStateHash = entryId;
        prevFrameStateHash = entryId;
    }

    private void Start() {
        controls = Toolbox.RegisterComponent<ControlManager>();

        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
        if (cameraFollow != null) cameraFollow.target = gameObject;
    }

    private static int entryId = Animator.StringToHash("Base.Entry");
    private static int idleId = Animator.StringToHash("Base.Idle");
    private static int walkId = Animator.StringToHash("Base.Walk");
    private static int attack1Id = Animator.StringToHash("Base.Attack1");
    private static int attack2Id = Animator.StringToHash("Base.Attack2");
    private static int attack3Id = Animator.StringToHash("Base.Attack3");
    private static int isMovingId = Animator.StringToHash("isMoving");
    private static int triggerAttackId = Animator.StringToHash("triggerAttack");

    // 1. migrate to state machine behaviour?
    // 2. responsive controls i.e. direction change in middle of string, moving away in middle of string
    // (only) queue attack at end of animation

    // sortorder based on y
    private void Update() {
        moveInput = controls.actions.Move;
        bool isAttackPressed = controls.actions.Attack.WasPressed;
        bool isAttackHeld = controls.actions.Attack.IsPressed;

        var sqrMagnitude = moveInput.sqrMagnitude;

        if (isAttackPressed) {
            isAttackQueued = true;
        }

        AnimatorStateInfo animInfo = fsm.GetCurrentAnimatorStateInfo(0);
        int currentStateHash = animInfo.fullPathHash;
        bool hasStateChanged = false;

        if (prevFrameStateHash != currentStateHash) {
            hasStateChanged = true;
            prevStateHash = prevFrameStateHash;
        }
        //Debug.Log(hasStateChanged + " " + DebugAnimationStates.Get(prevFrameStateHash) + " " + DebugAnimationStates.Get(prevStateHash) + " " + DebugAnimationStates.Get(currentStateHash));

        if (currentStateHash == idleId) {
            if (isAttackQueued) {
                isAttackQueued = false;
                fsm.SetTrigger(triggerAttackId);
            } else if (sqrMagnitude > 0) {
                player.Move(moveInput);
                fsm.SetBool(isMovingId, true);
            }
        } else if (currentStateHash == walkId) {
            if (isAttackQueued) {
                isAttackQueued = false;
                fsm.SetTrigger(triggerAttackId);
            } else if (sqrMagnitude > 0) {
                player.Move(moveInput);
            } else if (sqrMagnitude <= 0) {
                fsm.SetBool(isMovingId, false);
            }
        } else if (currentStateHash == attack1Id) { // 0.5f, 3-6th frame, 8 frames
            if (hasStateChanged && sqrMagnitude > 0) {
                player.Face(moveInput);
            }
            if (isAttackQueued) {
                isAttackQueued = false;
                if (animInfo.normalizedTime >= (3f/8)) {
                    fsm.SetTrigger(triggerAttackId);
                }
            }
        } else if (currentStateHash == attack2Id) { // 0.5f, 2-4th frame, 8 frames
            if (isAttackQueued) {
                isAttackQueued = false;
                if (animInfo.normalizedTime >= (2f/8)) {
                    fsm.SetTrigger(triggerAttackId);
                }
            }
        } else if (currentStateHash == attack3Id) { // 0.666f, 3-6th frame, 8 frames
            if (isAttackQueued) {
                isAttackQueued = false;
                if (animInfo.normalizedTime >= (6f/8)) {
                    fsm.SetTrigger(triggerAttackId);
                }
            }
        }

        playerAnimator.Animate(fsm, player);

        prevFrameStateHash = currentStateHash;
    }

    public float CalcButtonBuffer(AnimatorStateInfo animInfo, float reactionTime) {
        return 1 - (reactionTime / animInfo.length);
    }

}

#if UNITY_EDITOR
public class DebugAnimationStates {

    private static Dictionary<int, string> states = new Dictionary<int, string>();

    static DebugAnimationStates() {
        Add("Base.Entry");
        Add("Base.Idle");
        Add("Base.Walk");
        Add("Base.Attack1");
        Add("Base.Attack2");
        Add("Base.Attack3");
    }

    public static string Get(int fullPathHash) {
        string name;
        if (states.TryGetValue(fullPathHash, out name))
            return name;

        return "Unknown#" + fullPathHash;
    }

    private static void Add(string stateName) {
        int hash = Animator.StringToHash(stateName);
        states.Add(hash, stateName);
    }
}
#endif
