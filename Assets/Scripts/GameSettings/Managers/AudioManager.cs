using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

    //private static AudioManager instance;
    //public static AudioManager Instance { get { return instance; } }

    private AudioSource bgmAudioSource;
    private AudioSource sfxAudioSource;
    private EventManager eventManager;

    private void Awake() {
        //if (instance != null && instance != this) {
        //    Destroy(gameObject);
        //} else {
        //    instance = this;
        //}

        bgmAudioSource = GetComponents<AudioSource>()[0];
        sfxAudioSource = GetComponents<AudioSource>()[1];
        eventManager = Toolbox.RegisterComponent<EventManager>();
    }

    public void PlaySfx(AudioClip clip) {
        sfxAudioSource.PlayOneShot(clip);
    }

    public void SetBgmVolume(float volume) {
        bgmAudioSource.volume = volume;
    }

    public void SetSfxVolume(float volume) {
        sfxAudioSource.volume = volume;
    }

    public void PlaySfxEvent(IGameEvent e) {
        PlaySfxEvent ev = (PlaySfxEvent)e;
        PlaySfx(ev.clip);
    }

    private void OnEnable() {
        eventManager.AddSubscriber(Events.PLAY_SFX, PlaySfxEvent);
    }

    private void OnDisable() {
        eventManager.RemoveSubscriber(Events.PLAY_SFX, PlaySfxEvent);
    }

}
