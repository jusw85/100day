using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake() {
        moverController = GetComponent<MoverController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHp = maxHp;
    }

    private void Start() {
        followTarget = PlayerController.Instance.gameObject;
    }

    private void Update() {
        moverController.MoveDirection = Vector2.zero;

        if (followTarget != null) {
            var followVector = (followTarget.transform.position - transform.position);
            moverController.MoveDirection = followVector;
        }
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
                damageable.Damage();
            }
        }
    }

    public void Damage() {
        currentHp--;
        AudioManager.Instance.PlaySfx(hitSounds[Random.Range(0, hitSounds.Length)]);
        if (currentHp <= 0) {
            AudioManager.Instance.PlaySfx(deathSounds[Random.Range(0, deathSounds.Length)]);
            Destroy();
        }
    }

    private void LateUpdate() {
        spriteRenderer.flipX = moverController.MoveDirection.x <= 0;
    }

    public override void OnObjectReuse() {
        currentHp = maxHp;
        followTarget = PlayerController.Instance.gameObject;
    }

}
