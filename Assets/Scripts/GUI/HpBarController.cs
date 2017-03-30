using UnityEngine;
using UnityEngine.UI;

public class HpBarController : MonoBehaviour {

    private Slider slider;
    private EventManager eventManager;

    private void Awake() {
        slider = GetComponent<Slider>();
        eventManager = Toolbox.GetOrAddComponent<EventManager>();
    }

    private void OnEnable() {
        eventManager.AddSubscriber(Events.PLAYER_HPCHANGE, HpChangeHandler);
    }

    private void OnDisable() {
        eventManager.RemoveSubscriber(Events.PLAYER_HPCHANGE, HpChangeHandler);
    }

    public void HpChangeHandler(IGameEvent e) {
        PlayerHpChangeEvent ev = (PlayerHpChangeEvent)e;
        slider.value = Mathf.Clamp((ev.currentHp / ev.maxHp), 0, 1);
    }

}
