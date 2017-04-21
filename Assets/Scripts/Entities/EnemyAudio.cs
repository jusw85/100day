using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour {

    private EventManager eventManager;
    public AudioClip[] owSounds;
    public AudioClip[] deathSounds;

    private void Awake() {
        eventManager = Toolbox.GetOrAddComponent<EventManager>();
    }

    public void DoUpdate(Enemy enemy, ref EnemyFrameInfo frameInfo) {
        if (frameInfo.damageInfo != null) {
            PlaySfxEvent ev = new PlaySfxEvent(owSounds[Random.Range(0, owSounds.Length)]);
            eventManager.Publish(Events.PLAY_SFX, ev);
        }
        if (enemy.currentHp <= 0) {
            PlaySfxEvent ev = new PlaySfxEvent(deathSounds[Random.Range(0, deathSounds.Length)]);
            eventManager.Publish(Events.PLAY_SFX, ev);
        }
    }
}
