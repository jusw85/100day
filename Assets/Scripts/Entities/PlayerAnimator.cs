using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerAnimator : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Flasher flasherCharge;
    private Flasher flasherFullCharge;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        flasherCharge = new Flasher(spriteRenderer, 0.8f, 0.1f, -1);
        flasherCharge.FlashColor = Color.blue;
        flasherFullCharge = new Flasher(spriteRenderer, 0.8f, 0.3f, -1);
        flasherFullCharge.FlashColor = Color.red;
    }

    public void DoUpdate(Player player, ref PlayerFrameInfo frameInfo) {
        animator.SetFloat(AnimParams.FACEDIRX, player.FaceDir.x);
        animator.SetFloat(AnimParams.FACEDIRY, player.FaceDir.y);
        spriteRenderer.sortingOrder = Mathf.RoundToInt(player.transform.position.y * 100f) * -1;

        if (frameInfo.isFullyCharged) {
            flasherCharge.Stop();
            flasherFullCharge.Start();
        } else if (frameInfo.isCharging) {
            flasherCharge.Start();
        } else if (frameInfo.hasStoppedCharging) {
            flasherCharge.Stop();
            flasherFullCharge.Stop();
        }
    }
}
