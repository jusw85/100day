using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {

    private EventManager eventManager;
    public AudioClip swordSound;
    public AudioClip[] owSounds;
    public AudioClip[] deathSounds;

    private void Awake() {
        eventManager = Toolbox.RegisterComponent<EventManager>();
    }

    public void DoUpdate(Player player, ref PlayerFrameInfo frameInfo) {
        if (frameInfo.isAttacking) {
            PlaySfxEvent ev = new PlaySfxEvent(swordSound);
            eventManager.Publish(Events.PLAY_SFX, ev);
        }
        if (frameInfo.isChargeAttacking) {
            PlaySfxEvent ev = new PlaySfxEvent(swordSound);
            eventManager.Publish(Events.PLAY_SFX, ev);
        }
        //owSounds[Random.Range(0, owSounds.Length)]
    }
}
