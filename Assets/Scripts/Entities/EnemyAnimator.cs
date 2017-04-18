using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyAnimator : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Flasher flasherCharge;
    private Flasher flasherFullCharge;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        //flasherCharge = new Flasher(spriteRenderer, 0.8f, 0.1f);
        //flasherCharge.FlashColor = Color.blue;
        //flasherFullCharge = new Flasher(spriteRenderer, 0.8f, 0.3f);
        //flasherFullCharge.FlashColor = Color.red;
    }

    public void DoUpdate(Enemy enemy, ref EnemyFrameInfo frameInfo) {
        //animator.SetFloat(AnimParams.FACEDIRX, enemy.FaceDir.x);
        //animator.SetFloat(AnimParams.FACEDIRY, enemy.FaceDir.y);
        spriteRenderer.sortingOrder = Mathf.RoundToInt(enemy.transform.position.y * 100f) * -1;

    }
}
