using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(EnemyAudio))]
public class EnemyController : PoolObject, IDamageable {

    public float pushbackForce = 40f;
    public GameObject followTarget;

    private MoverController moverController;
    private Animator fsm;

    public GameObject bloodSplatter;
    private PoolManager poolManager;


    private BoxCollider2D hitbox;


    private Enemy enemy;
    private EnemyAnimator enemyAnimator;
    private EnemyAudio enemyAudio;

    private EnemyFrameInfo frameInfo;

    private void Awake() {
        moverController = GetComponent<MoverController>();

        hitbox = transform.Find("Hitbox").GetComponent<BoxCollider2D>();

        poolManager = Toolbox.GetOrAddComponent<PoolManager>();
        poolManager.CreatePool(bloodSplatter, 150);

        damageInfo = new DamageInfo();
        damageInfo.damage = 10;


        fsm = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        enemyAnimator = GetComponent<EnemyAnimator>();
        enemyAudio = GetComponent<EnemyAudio>();

        frameInfo = new EnemyFrameInfo();
    }

    private void Start() {
        followTarget = Player.Instance.gameObject;
    }


    public bool trackTarget = false;

    private static readonly Vector2 FACE_DOWN = new Vector2(0f, -1f);
    private static readonly Vector2 FACE_RIGHT = new Vector2(1f, 0f);
    private static readonly Vector2 FACE_UP = new Vector2(0f, 1f);
    private static readonly Vector2 FACE_LEFT = new Vector2(-1f, 0f);

    [System.NonSerialized]
    public Vector2 faceDir = FACE_DOWN;
    public float moveSpeed = 12f;

    //public void Move(Vector2 moveInput) {
    //    moverController.Speed = moveSpeed;
    //    moverController.Direction = moveInput;
    //    Face(moveInput);
    //}

    public void Face(Vector2 moveDir) {
        Vector2 v = moveDir.normalized;

        if (Mathf.Abs(v.x) >= Mathf.Abs(v.y)) {
            if (v.x < 0) {
                faceDir = FACE_LEFT;
            } else {
                faceDir = FACE_RIGHT;
            }
        } else {
            if (v.y < 0) {
                faceDir = FACE_DOWN;
            } else {
                faceDir = FACE_UP;
            }
        }
    }

    public float proximityStop = 30f;

    //public void DoUpdate() 
        // decide what to do based on state and vars
        // update fsm state
        // 
    //}
    private void Update() {
        //if (stopFrames-- > 0) {
        //    moverController.MoveSpeed = 0;
        //}

        if (trackTarget && stopFrames-- <= 0) {
            moverController.Speed = moveSpeed;
            moverController.Direction = Vector2.zero;

            if (followTarget != null) {
                var followVector = (followTarget.transform.position - transform.position);
                if (followVector.magnitude <= proximityStop) {
                    moverController.Speed = 0f;
                    //Face(moverController.MoveDirection);
                    //moverController.MoveDirection = Vector2.zero;
                    moverController.Direction = followVector;
                } else {
                    moverController.Direction = followVector;
                }

            }
            Face(moverController.Direction);
        } else {
            moverController.Speed = 0;
            moverController.Direction = Vector2.zero;
        }
        moverController.UpdateVelocity();

        if (moverController.Speed > 0) {
            fsm.SetBool(AnimParams.ISMOVING, true);
        } else {
            fsm.SetBool(AnimParams.ISMOVING, false);
        }
        fsm.SetFloat(AnimParams.FACEDIRX, faceDir.x);
        fsm.SetFloat(AnimParams.FACEDIRY, faceDir.y);

        //enemy.DoUpdate(state, c, ref frameInfo);
        enemyAnimator.DoUpdate(enemy, ref frameInfo);
        enemyAudio.DoUpdate(enemy, ref frameInfo);

        frameInfo.Reset();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        string tag = other.gameObject.tag;
        if (tag == "Player") {
            MoverController movable = other.gameObject.GetComponent<MoverController>();
            if (movable != null) {
                movable.ExternalForce = moverController.Direction * pushbackForce;
            }
            IDamageable damageable = (IDamageable)other.gameObject.GetComponent(typeof(IDamageable));
            if (damageable != null) {
                damageable.Damage(damageInfo);
            }
        }
    }

    private DamageInfo damageInfo;

    public void Damage(DamageInfo damageInfo) {
        //Vector3 inRot = damager.transform.eulerAngles;
        //Vector3 outRot = new Vector3(-inRot.z - 90f, 0f, 0f);

        //Quaternion rot = Quaternion.FromToRotation(Vector3.forward, Vector3.right);
        //rot *= Quaternion.Euler(outRot);

        //poolManager.ReuseObject(bloodSplatter, transform.position, rot);
        //if (currentHp <= 0) {
        //    Destroy();
        //}

        frameInfo.damageInfo = damageInfo;

        stopFrames = 16; // should use time-based instead
    }

    public override void OnObjectReuse() {
        enemy.Reset();
        followTarget = Player.Instance.gameObject;
    }


    private int stopFrames = 0;

    public IEnumerator Invuln(int numFrames) {
        hitbox.enabled = false;
        for (int i = 0; i < numFrames; i++) {
            yield return null;
        }
        hitbox.enabled = true;
    }

}

public class EnemyFrameInfo {
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
