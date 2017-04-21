using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerAudio))]
public class PlayerController : MonoBehaviour, IDamageable {

    private Player player;
    private PlayerAnimator playerAnimator;
    private PlayerAudio playerAudio;
    private Animator fsm;

    private FsmFrameInfo state;
    private PlayerFrameInfo frameInfo;

    private void Awake() {
        fsm = GetComponent<Animator>();
        player = GetComponent<Player>();
        playerAnimator = GetComponent<PlayerAnimator>();
        playerAudio = GetComponent<PlayerAudio>();

        state = new FsmFrameInfo(AnimStates.ENTRY);
        frameInfo = new PlayerFrameInfo();

        GameManager manager = Toolbox.GetOrAddComponent<GameManager>();
        manager.RegisterPlayer(this);
#if UNITY_EDITOR
        state.DebugInit(fsm);
#endif
    }

    private void Start() {
        // refactor this camera follow, e.g. in case camera doesnt follow player during cutscenes
        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
        if (cameraFollow != null) cameraFollow.target = gameObject;
    }

    public void DoUpdate(ControlManager c) {
        AnimatorStateInfo animInfo = fsm.GetCurrentAnimatorStateInfo(0);
        state.DoUpdate(animInfo.fullPathHash);

        player.DoUpdate(state, c, ref frameInfo);
        playerAnimator.DoUpdate(player, ref frameInfo);
        playerAudio.DoUpdate(player, ref frameInfo);

        if (state.curr == AnimStates.IDLE) {
            if (c.isRollPressed && player.CanRoll) {
                fsm.SetTrigger(AnimParams.TRIGGER_ROLL);
            } else if (c.isAttackReleased) {
                if (frameInfo.toChargeAttack) {
                    fsm.SetTrigger(AnimParams.TRIGGER_CHARGEATTACK);
                } else if (frameInfo.toAttack) {
                    fsm.SetTrigger(AnimParams.TRIGGER_ATTACK);
                }
            } else if (c.isAttackPressed) {
                fsm.SetTrigger(AnimParams.TRIGGER_ATTACK);
            } else if (c.isMoved) {
                fsm.SetBool(AnimParams.ISMOVING, true);
            }

        } else if (state.curr == AnimStates.WALK) {
            if (c.isRollPressed && player.CanRoll) {
                fsm.SetTrigger(AnimParams.TRIGGER_ROLL);
            } else if (c.isAttackReleased) {
                if (frameInfo.toChargeAttack) {
                    fsm.SetTrigger(AnimParams.TRIGGER_CHARGEATTACK);
                } else if (frameInfo.toAttack) {
                    fsm.SetTrigger(AnimParams.TRIGGER_ATTACK);
                }
            } else if (c.isAttackPressed) {
                fsm.SetTrigger(AnimParams.TRIGGER_ATTACK);
            } else if (!c.isMoved) {
                fsm.SetBool(AnimParams.ISMOVING, false);
            }

        } else if (state.curr == AnimStates.ATTACK1) {
            if (c.isRollPressed && player.CanRoll) {
                fsm.SetTrigger(AnimParams.TRIGGER_ROLL);
            } else if (c.isAttackPressed && animInfo.normalizedTime >= (3f / 8)) {
                fsm.SetTrigger(AnimParams.TRIGGER_ATTACK);
            }

        } else if (state.curr == AnimStates.ATTACK2) {
            if (c.isRollPressed && player.CanRoll) {
                fsm.SetTrigger(AnimParams.TRIGGER_ROLL);
            } else if (c.isAttackPressed && (animInfo.normalizedTime >= (2f / 8))) {
                fsm.SetTrigger(AnimParams.TRIGGER_ATTACK);
            }

        } else if (state.curr == AnimStates.ATTACK3) {
            if (c.isRollPressed && player.CanRoll) {
                fsm.ResetTrigger(AnimParams.TRIGGER_ATTACK);
                fsm.SetTrigger(AnimParams.TRIGGER_ROLL);
            } else if (c.isAttackPressed && (animInfo.normalizedTime >= (6f / 8))) {
                fsm.ResetTrigger(AnimParams.TRIGGER_ROLL);
                fsm.SetTrigger(AnimParams.TRIGGER_ATTACK);
            }

        } else if (state.curr == AnimStates.CHARGEATTACK) {
            if (c.isRollPressed && player.CanRoll) {
                fsm.ResetTrigger(AnimParams.TRIGGER_ATTACK);
                fsm.SetTrigger(AnimParams.TRIGGER_ROLL);
            } else if (c.isAttackPressed && (animInfo.normalizedTime >= (6f / 8))) {
                fsm.ResetTrigger(AnimParams.TRIGGER_ROLL);
                fsm.SetTrigger(AnimParams.TRIGGER_ATTACK);
            }

        } else if (state.curr == AnimStates.ROLL) {
            if (state.hasChanged) {
                fsm.SetBool(AnimParams.ISMOVING, false);
            }

            if (c.isAttackReleased) {
                if (player.IsFullyCharged) {
                    fsm.ResetTrigger(AnimParams.TRIGGER_ATTACK);
                    fsm.SetTrigger(AnimParams.TRIGGER_CHARGEATTACK);
                } else if (player.IsCharging) {
                    fsm.SetTrigger(AnimParams.TRIGGER_ATTACK);
                }
            } else if (c.isAttackPressed && !fsm.GetBool(AnimParams.TRIGGER_CHARGEATTACK)) {
                fsm.SetTrigger(AnimParams.TRIGGER_ATTACK);
            }

        }

        frameInfo.Reset();
    }

    public void Damage(DamageInfo damageInfo) {
        frameInfo.damageInfo = damageInfo;
    }

}

public class AnimStates {
    public static int ENTRY = Animator.StringToHash("Base.Entry");
    public static int IDLE = Animator.StringToHash("Base.Idle");
    public static int WALK = Animator.StringToHash("Base.Walk");
    public static int ATTACK1 = Animator.StringToHash("Base.Attack1");
    public static int ATTACK2 = Animator.StringToHash("Base.Attack2");
    public static int ATTACK3 = Animator.StringToHash("Base.Attack3");
    public static int CHARGEATTACK = Animator.StringToHash("Base.ChargeAttack");
    public static int ROLL = Animator.StringToHash("Base.Roll");
}

public class AnimParams {
    public static int ISMOVING = Animator.StringToHash("isMoving");
    public static int TRIGGER_ATTACK = Animator.StringToHash("triggerAttack");
    public static int TRIGGER_CHARGEATTACK = Animator.StringToHash("triggerChargeAttack");
    public static int TRIGGER_ROLL = Animator.StringToHash("triggerRoll");

    public static int FACEDIRX = Animator.StringToHash("faceDirX");
    public static int FACEDIRY = Animator.StringToHash("faceDirY");
}

public class PlayerFrameInfo {
    public bool isDamaged;

    public bool isCharging;
    public bool isFullyCharged;
    public bool hasStoppedCharging;

    public bool toAttack;
    public bool toChargeAttack;

    public bool isAttacking;
    public bool isChargeAttacking;

    public DamageInfo damageInfo;

    public void Reset() {
        isDamaged = false;
        isCharging = false;
        isFullyCharged = false;
        hasStoppedCharging = false;
        toAttack = false;
        toChargeAttack = false;
        isAttacking = false;
        isChargeAttacking = false;
        damageInfo = null;
    }
}
