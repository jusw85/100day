using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyAnimator : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Flasher flasherDamage;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        flasherDamage = new Flasher(spriteRenderer, 1.0f, 0.05f, 2);
        flasherDamage.FlashColor = Color.red;
    }

    public void DoUpdate(Enemy enemy, ref EnemyFrameInfo frameInfo) {
        //animator.SetFloat(AnimParams.FACEDIRX, enemy.FaceDir.x);
        //animator.SetFloat(AnimParams.FACEDIRY, enemy.FaceDir.y);
        spriteRenderer.sortingOrder = Mathf.RoundToInt(enemy.transform.position.y * 100f) * -1;

        if (frameInfo.damageInfo != null) {
            flasherDamage.Restart();
        }
    }
}
