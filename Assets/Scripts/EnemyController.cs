using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoverController))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyController : MonoBehaviour, IDamageable {

    public int maxHp = 20;
    [System.NonSerialized]
    public int currentHp;
    public GameObject followTarget;

    public AudioClip[] hitsounds;
    public AudioClip[] deathsounds;

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

    public void Damage() {
        currentHp--;
        AudioManager.Instance.PlaySfx(hitsounds[Random.Range(0, hitsounds.Length)]);
        if (currentHp <= 0) {
            AudioManager.Instance.PlaySfx(deathsounds[Random.Range(0, deathsounds.Length)]);
            Destroy(gameObject);
        }
    }

    private void LateUpdate() {
        spriteRenderer.flipX = moverController.MoveDirection.x <= 0;
    }
}
