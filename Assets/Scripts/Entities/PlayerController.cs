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
    private static int chargeAttackId = Animator.StringToHash("Base.ChargeAttack");
    private static int rollId = Animator.StringToHash("Base.Roll");

    private static int isMovingId = Animator.StringToHash("isMoving");
    private static int triggerAttackId = Animator.StringToHash("triggerAttack");
    private static int triggerChargeAttackId = Animator.StringToHash("triggerChargeAttack");
    private static int triggerRollId = Animator.StringToHash("triggerRoll");

    private void Update() {
        moveInput = controls.actions.Move;
        var sqrMagnitude = moveInput.sqrMagnitude;

        bool isAttackPressed = controls.actions.Attack.WasPressed;
        bool isAttackReleased = controls.actions.Attack.WasReleased;
        bool isAttackHeld = controls.actions.Attack.IsPressed;
        bool isRollPressed = controls.actions.Roll.WasPressed;
        bool isSpecialPressed = controls.actions.Special.WasPressed;

        AnimatorStateInfo animInfo = fsm.GetCurrentAnimatorStateInfo(0);
        int currentStateHash = animInfo.fullPathHash;
        bool hasStateChanged = false;

        if (prevFrameStateHash != currentStateHash) {
            hasStateChanged = true;
            prevStateHash = prevFrameStateHash;
        }
        //Debug.Log(hasStateChanged + " " + DebugAnimationStates.Get(prevFrameStateHash) + " " + DebugAnimationStates.Get(prevStateHash) + " " + DebugAnimationStates.Get(currentStateHash));

        if (hasStateChanged) {
            if (prevStateHash == rollId) {
                player.StopRoll();
            }
        }

        if (currentStateHash == idleId) {
            if (isAttackHeld) {
                player.AddCharge(Time.deltaTime);
            }
            if (sqrMagnitude > 0) {
                player.Move(moveInput);
            }
            bool wasPlayerFullyCharged = false;
            bool wasPlayerCharging = false;
            if (isAttackReleased) {
                wasPlayerFullyCharged = player.IsFullyCharged;
                wasPlayerCharging = player.IsCharging;
                player.ResetCharge();
            }

            if (isRollPressed && player.canDodge) {
                fsm.SetTrigger(triggerRollId);
            } else if (isAttackReleased) {
                if (wasPlayerFullyCharged) {
                    fsm.SetTrigger(triggerChargeAttackId);
                } else if (wasPlayerCharging) {
                    fsm.SetTrigger(triggerAttackId);
                }
            } else if (isAttackPressed) {
                fsm.SetTrigger(triggerAttackId);
            } else if (sqrMagnitude > 0) {
                fsm.SetBool(isMovingId, true);
            }

        } else if (currentStateHash == walkId) {
            if (isAttackHeld) {
                player.AddCharge(Time.deltaTime);
            }
            if (sqrMagnitude > 0) {
                player.Move(moveInput);
            }
            bool wasPlayerFullyCharged = false;
            bool wasPlayerCharging = false;
            if (isAttackReleased) {
                wasPlayerFullyCharged = player.IsFullyCharged;
                wasPlayerCharging = player.IsCharging;
                player.ResetCharge();
            }

            if (isRollPressed && player.canDodge) {
                fsm.SetTrigger(triggerRollId);
            } else if (isAttackReleased) {
                if (wasPlayerFullyCharged) {
                    fsm.SetTrigger(triggerChargeAttackId);
                } else if (wasPlayerCharging) {
                    fsm.SetTrigger(triggerAttackId);
                }
            } else if (isAttackPressed) {
                fsm.SetTrigger(triggerAttackId);
            } else if (sqrMagnitude <= 0) {
                fsm.SetBool(isMovingId, false);
            }

        } else if (currentStateHash == attack1Id) { // 0.5f, 3-6th frame, 8 frames
            player.ResetCharge();
            if (hasStateChanged) {
                player.Attack(1);
                if (sqrMagnitude > 0) {
                    player.Face(moveInput);
                }
            }

            if (isRollPressed && player.canDodge) {
                fsm.SetTrigger(triggerRollId);
            } else if (isAttackPressed && animInfo.normalizedTime >= (3f / 8)) {
                fsm.SetTrigger(triggerAttackId);
            }

        } else if (currentStateHash == attack2Id) { // 0.5f, 2-4th frame, 8 frames
            player.ResetCharge();
            if (hasStateChanged && sqrMagnitude > 0) {
                player.Face(moveInput);
            }

            if (isRollPressed && player.canDodge) {
                fsm.SetTrigger(triggerRollId);
            } else if (isAttackPressed && (animInfo.normalizedTime >= (2f / 8))) {
                fsm.SetTrigger(triggerAttackId);
            }

        } else if (currentStateHash == attack3Id) { // 0.666f, 3-6th frame, 8 frames
            player.ResetCharge();
            if (hasStateChanged && sqrMagnitude > 0) {
                player.Face(moveInput);
            }

            if (isRollPressed && player.canDodge) {
                fsm.ResetTrigger(triggerAttackId);
                fsm.SetTrigger(triggerRollId);
            } else if (isAttackPressed && (animInfo.normalizedTime >= (6f / 8))) {
                fsm.ResetTrigger(triggerRollId);
                fsm.SetTrigger(triggerAttackId);
            }

        } else if (currentStateHash == chargeAttackId) { // 0.666f, 3-6th frame, 8 frames
            player.ResetCharge();
            if (hasStateChanged && sqrMagnitude > 0) {
                player.Face(moveInput);
            }

            if (isRollPressed && player.canDodge) {
                fsm.ResetTrigger(triggerAttackId);
                fsm.SetTrigger(triggerRollId);
            } else if (isAttackPressed && (animInfo.normalizedTime >= (6f / 8))) {
                fsm.ResetTrigger(triggerRollId);
                fsm.SetTrigger(triggerAttackId);
            }

        } else if (currentStateHash == rollId) {
            if (hasStateChanged) {
                fsm.SetBool(isMovingId, false);
                if (sqrMagnitude > 0) {
                    player.StartRoll(moveInput);
                } else {
                    player.StartRoll(player.faceDir);
                }
            }

            if (isAttackReleased) {
                if (player.IsFullyCharged) {
                    fsm.ResetTrigger(triggerAttackId);
                    fsm.SetTrigger(triggerChargeAttackId);
                } else if (player.IsCharging) {
                    fsm.SetTrigger(triggerAttackId);
                }
            } else if (isAttackPressed && !fsm.GetBool(triggerChargeAttackId)) {
                fsm.SetTrigger(triggerAttackId);
            }

        }

        playerAnimator.Animate(player);
        prevFrameStateHash = currentStateHash;
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
        Add("Base.ChargeAttack");
        Add("Base.Roll");
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
