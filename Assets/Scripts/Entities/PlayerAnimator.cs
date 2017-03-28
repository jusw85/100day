using UnityEngine;
using DG.Tweening;

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

        flasherCharge = new Flasher(spriteRenderer, 0.8f, 0.1f);
        flasherCharge.FlashColor = Color.blue;
        flasherFullCharge = new Flasher(spriteRenderer, 0.8f, 0.3f);
        flasherFullCharge.FlashColor = Color.red;
    }

    public void DoUpdate(Player player, ref PlayerFrameInfo frameInfo) {
        animator.SetFloat(AnimParams.FACEDIRX, player.faceDir.x);
        animator.SetFloat(AnimParams.FACEDIRY, player.faceDir.y);
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

public class Flasher {

    public static readonly int MATERIAL_FLASHAMOUNT_ID = Shader.PropertyToID("_FlashAmount");
    public static readonly int MATERIAL_FLASHCOLOR_ID = Shader.PropertyToID("_FlashColor");

    private SpriteRenderer spriteRenderer;
    private Tween flashTween;

    public Color FlashColor { get; set; }

    public float TexFlashAmount {
        get { return spriteRenderer.material.GetFloat(MATERIAL_FLASHAMOUNT_ID); }
        set { spriteRenderer.material.SetFloat(MATERIAL_FLASHAMOUNT_ID, value); }
    }
    public Color TexFlashColor {
        get { return spriteRenderer.material.GetColor(MATERIAL_FLASHCOLOR_ID); }
        set { spriteRenderer.material.SetColor(MATERIAL_FLASHCOLOR_ID, value); }
    }

    public Flasher(SpriteRenderer spriteRenderer, float value, float duration) {
        this.spriteRenderer = spriteRenderer;

        TexFlashAmount = 0f;
        flashTween = DOTween
            .To(() => TexFlashAmount, x => TexFlashAmount = x, value, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetAutoKill(false);
    }

    public void Start() {
        if (!flashTween.IsPlaying()) {
            TexFlashColor = FlashColor;
            TexFlashAmount = 0f;
            flashTween.Restart();
        }
    }

    public void Pause() {
        if (flashTween.IsPlaying()) {
            flashTween.Pause();
        }
    }

    public void Stop() {
        Pause();
        TexFlashAmount = 0f;
    }

}
