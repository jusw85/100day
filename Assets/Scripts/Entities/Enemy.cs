using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MoverController))]
public class Enemy : MonoBehaviour {

    public float maxHp = 100;
    [System.NonSerialized]
    public float currentHp;

    private MoverController mover;
    private EventManager eventManager;

    private void Awake() {
        mover = GetComponent<MoverController>();

        Reset();
        eventManager = Toolbox.GetOrAddComponent<EventManager>();
    }

    private static readonly Vector2 FACE_DOWN = new Vector2(0f, -1f);
    private static readonly Vector2 FACE_RIGHT = new Vector2(1f, 0f);
    private static readonly Vector2 FACE_UP = new Vector2(0f, 1f);
    private static readonly Vector2 FACE_LEFT = new Vector2(-1f, 0f);

    public Vector2 FaceDir { get; set; }
    public float walkSpeed = 12f;

    public void Walk(Vector2 moveDirection) {
        mover.Speed = walkSpeed;
        mover.Direction = moveDirection;
        mover.UpdateVelocity();
        Face(moveDirection);
    }

    public void Face(Vector2 moveDir) {
        Vector2 v = moveDir.normalized;

        if (Mathf.Abs(v.x) >= Mathf.Abs(v.y)) {
            if (v.x < 0) {
                FaceDir = FACE_LEFT;
            } else {
                FaceDir = FACE_RIGHT;
            }
        } else {
            if (v.y < 0) {
                FaceDir = FACE_DOWN;
            } else {
                FaceDir = FACE_UP;
            }
        }
    }

    public void Reset() {
        currentHp = maxHp;
        FaceDir = FACE_DOWN;
    }

    public void Damage(DamageInfo damageInfo) {
        float prevHp = currentHp;
        currentHp -= damageInfo.damage;
        //PlayerHpChangeEvent ev = new PlayerHpChangeEvent(prevHp, currentHp, maxHp);
        //eventManager.Publish(Events.PLAYER_HPCHANGE, ev);

        //if (currentHp <= 0) {
        //    eventManager.Publish(Events.GAME_OVER, null);
        //}
    }

    private EnemyFrameInfo frameInfo;
    public void DoUpdate(FsmFrameInfo state, ControlManager c, ref EnemyFrameInfo frameInfo) {
        this.frameInfo = frameInfo;
        if (frameInfo.damageInfo != null) {
            Damage(frameInfo.damageInfo);
        }

        if (state.hasChanged) {
            if (state.curr == AnimStates.ATTACK1) {
                frameInfo.isAttacking = true;
                if (c.isMoved) {
                    Face(c.move);
                }
            } else if (state.curr == AnimStates.ATTACK2) {
                frameInfo.isAttacking = true;
                if (c.isMoved) {
                    Face(c.move);
                }
            } else if (state.curr == AnimStates.ATTACK3) {
                frameInfo.isAttacking = true;
                if (c.isMoved) {
                    Face(c.move);
                }
            } else if (state.curr == AnimStates.CHARGEATTACK) {
                frameInfo.isChargeAttacking = true;
                if (c.isMoved) {
                    Face(c.move);
                }
            }
        }

        if (state.curr == AnimStates.IDLE) {
            if (c.isMoved) {
                Walk(c.move);
            }

        } else if (state.curr == AnimStates.WALK) {
            if (c.isMoved) {
                Walk(c.move);
            }

        }
    }

}
