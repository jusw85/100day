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

    private void Awake() {
        moverController = GetComponent<MoverController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animationController = GetComponent<AnimationController>();
        currentHp = maxHp;

        spriteRenderer.material.SetColor(Constants.MATERIAL_FLASHCOLOR_ID, flashColor);
    }

    private void Start() {
        followTarget = Player.Instance.gameObject;

        poolManager = Toolbox.RegisterComponent<PoolManager>();
        poolManager.CreatePool(bloodSplatter, 150);
    }

    private Vector2 lastMoveInput;

    private void Update() {
        //moverController.MoveDirection = Vector2.zero;

        //if (followTarget != null) {
        //    var followVector = (followTarget.transform.position - transform.position);
        //    moverController.MoveDirection = followVector;
        //}

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
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        string tag = other.gameObject.tag;
        if (tag == "Player") {
            MoverController movable = other.gameObject.GetComponent<MoverController>();
            if (movable != null) {
                movable.externalForce = moverController.MoveDirection * pushbackForce;
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
        Flash();
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
