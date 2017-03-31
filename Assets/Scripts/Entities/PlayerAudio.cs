using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {

    private EventManager eventManager;
    public AudioClip swordSound;
    public AudioClip[] owSounds;
    public AudioClip[] deathSounds;

    private void Awake() {
        eventManager = Toolbox.GetOrAddComponent<EventManager>();
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
        if (frameInfo.damageInfo != null) {
            PlaySfxEvent ev = new PlaySfxEvent(owSounds[Random.Range(0, owSounds.Length)]);
            eventManager.Publish(Events.PLAY_SFX, ev);
        }
        if (player.currentHp <= 0) {
            PlaySfxEvent ev = new PlaySfxEvent(deathSounds[Random.Range(0, deathSounds.Length)]);
            eventManager.Publish(Events.PLAY_SFX, ev);
        }
    }
}
