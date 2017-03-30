using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlasher : MonoBehaviour {

    private EventManager eventManager;

    private Image flashImage;
    private Color flashColour = new Color(1f, 1f, 1f, 0.6f);
    private float flashSpeed = 1.5f;
    private Tween screenFlashTween;

    private void Awake() {
        eventManager = Toolbox.GetOrAddComponent<EventManager>();
        flashImage = GetComponent<Image>();
    }

    private void OnEnable() {
        eventManager.AddSubscriber(Events.PLAYER_HPCHANGE, HpChangeHandler);
    }

    private void OnDisable() {
        eventManager.RemoveSubscriber(Events.PLAYER_HPCHANGE, HpChangeHandler);
    }

    private void HpChangeHandler(IGameEvent e) {
        flashImage.color = flashColour;
        if (screenFlashTween != null) {
            screenFlashTween.Restart();
        } else {
            screenFlashTween = DOTween
                .To(() => flashImage.color, x => flashImage.color = x, Color.clear, flashSpeed)
                .SetAutoKill(false);
            screenFlashTween.Play();
        }
    }

}
