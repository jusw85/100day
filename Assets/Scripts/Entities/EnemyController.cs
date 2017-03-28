using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(MoverController))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyController : PoolObject, IDamageable {

    public float pushbackForce = 40f;
    public int maxHp = 20;
    [System.NonSerialized]
    public int currentHp;
    public GameObject followTarget;

    public AudioClip[] hitSounds;
    public AudioClip[] deathSounds;

    private MoverController moverController;
    private SpriteRenderer spriteRenderer;
    private AnimationController animationController;
    private Animator fsm;

    public GameObject bloodSplatter;
    private PoolManager poolManager;

    private Color flashColor = Color.red;
    public float FlashAmount {
        get {
            return spriteRenderer.material.GetFloat(Constants.MATERIAL_FLASHAMOUNT_ID);
        }
        set {
            spriteRenderer.material.SetFloat(Constants.MATERIAL_FLASHAMOUNT_ID, value);
        }
    }
    private Tween flashTween;
    private BoxCollider2D hitbox;

    private void Awake() {
        moverController = GetComponent<MoverController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animationController = GetComponent<AnimationController>();
        fsm = GetComponent<Animator>();
        currentHp = maxHp;

        hitbox = transform.Find("Hitbox").GetComponent<BoxCollider2D>();

        spriteRenderer.material.SetColor(Constants.MATERIAL_FLASHCOLOR_ID, flashColor);

        poolManager = Toolbox.RegisterComponent<PoolManager>();
        poolManager.CreatePool(bloodSplatter, 150);
    }

    private void Start() {
        followTarget = Player.Instance.gameObject;

    }

    private Vector2 lastMoveInput;

    public bool trackTarget = false;

    private static readonly Vector2 FACE_DOWN = new Vector2(0f, -1f);
    private static readonly Vector2 FACE_RIGHT = new Vector2(1f, 0f);
    private static readonly Vector2 FACE_UP = new Vector2(0f, 1f);
    private static readonly Vector2 FACE_LEFT = new Vector2(-1f, 0f);

    [System.NonSerialized]
    public Vector2 faceDir = FACE_DOWN;
    public float moveSpeed = 12f;

    public void Move(Vector2 moveInput) {
        moverController.Speed = moveSpeed;
        moverController.Direction = moveInput;
        Face(moveInput);
    }

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

    private static int isMovingId = Animator.StringToHash("isMoving");
    private static int faceDirXId = Animator.StringToHash("faceDirX");
    private static int faceDirYId = Animator.StringToHash("faceDirY");

    public float proximityStop = 30f;
    private void Update() {
        //if (stopFrames-- > 0) {
        //    moverController.MoveSpeed = 0;
        //}

        if (moverController.Speed > 0) {
            fsm.SetBool(isMovingId, true);
        } else {
            fsm.SetBool(isMovingId, false);
        }
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

            //Vector2 moveInput = moverController.MoveDirection;
            //animationController.SetIsMoving(moveInput.magnitude > 0);
            //animationController.SetMoveVector(moveInput);
            //animationController.SetLastMoveVector(lastMoveInput);

            //animationController.SetIsFacingRight(true);
            //if (moveInput.x < 0 ||
            //    (moveInput.magnitude == 0f && lastMoveInput.x < 0)) {
            //    animationController.SetIsFacingRight(false);
            //}
            //if (moveInput.magnitude > 0)
            //    lastMoveInput = moveInput;
        } else {
            moverController.Speed = 0;
            moverController.Direction = Vector2.zero;
        }
        moverController.UpdateVelocity();
        fsm.SetFloat(faceDirXId, faceDir.x);
        fsm.SetFloat(faceDirYId, faceDir.y);
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
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
                damageable.Damage(gameObject);
            }
        }
    }

    public void Damage(GameObject damager) {
        //currentHp--;
        //AudioManager.Instance.PlaySfx(hitSounds[Random.Range(0, hitSounds.Length)]);

        //Vector3 inRot = damager.transform.eulerAngles;
        //Vector3 outRot = new Vector3(-inRot.z - 90f, 0f, 0f);

        //Quaternion rot = Quaternion.FromToRotation(Vector3.forward, Vector3.right);
        //rot *= Quaternion.Euler(outRot);

        //poolManager.ReuseObject(bloodSplatter, transform.position, rot);
        //if (currentHp <= 0) {
        //    AudioManager.Instance.PlaySfx(deathSounds[Random.Range(0, deathSounds.Length)]);
        //    Destroy();
        //}
        stopFrames = 16; // should use time-based instead
        Flash();
    }

    private int stopFrames = 0;

    public IEnumerator Invuln(int numFrames) {
        hitbox.enabled = false;
        for (int i = 0; i < numFrames; i++) {
            yield return null;
        }
        hitbox.enabled = true;
    }

    public void Flash() {
        if (flashTween != null) {
            flashTween.Restart();
        } else {
            FlashAmount = 0f;
            flashTween = DOTween
                .To(() => FlashAmount, x => FlashAmount = x, 1.0f, 0.05f)
                .SetLoops(2, LoopType.Yoyo)
                .SetAutoKill(false);
            flashTween.Play();
        }
    }

    private void LateUpdate() {
        //spriteRenderer.flipX = moverController.MoveDirection.x <= 0;
    }

    public override void OnObjectReuse() {
        currentHp = maxHp;
        followTarget = Player.Instance.gameObject;
    }

}
