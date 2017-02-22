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

    public GameObject bloodSplatter;

    private void Awake() {
        moverController = GetComponent<MoverController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHp = maxHp;
    }

    private void Start() {
        followTarget = PlayerController.Instance.gameObject;

        PoolManager.instance.CreatePool(bloodSplatter, 150);
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
                damageable.Damage(gameObject);
            }
        }
    }

    public void Damage(GameObject damager) {
        currentHp--;
        AudioManager.Instance.PlaySfx(hitSounds[Random.Range(0, hitSounds.Length)]);

        Vector3 inRot = damager.transform.eulerAngles;
        Vector3 outRot = new Vector3(-inRot.z - 90f, 0f, 0f);

        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, Vector3.right);
        rot *= Quaternion.Euler(outRot);

        PoolManager.instance.ReuseObject(bloodSplatter, transform.position, rot);
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
