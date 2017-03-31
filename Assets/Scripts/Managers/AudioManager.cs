using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

    private AudioSource bgmAudioSource;
    private AudioSource sfxAudioSource;
    private EventManager eventManager;

    private void Awake() {
        bgmAudioSource = GetComponents<AudioSource>()[0];
        sfxAudioSource = GetComponents<AudioSource>()[1];
        eventManager = Toolbox.GetOrAddComponent<EventManager>();
    }

    public void PlaySfx(AudioClip clip) {
        sfxAudioSource.PlayOneShot(clip);
    }

    public void PlayBgm(AudioClip clip) {
        bgmAudioSource.clip = clip;
        bgmAudioSource.Play();
    }

    public void SetBgmVolume(float volume) {
        bgmAudioSource.volume = volume;
    }

    public void SetSfxVolume(float volume) {
        sfxAudioSource.volume = volume;
    }

    private void PlaySfxEvent(IGameEvent e) {
        PlaySfxEvent ev = (PlaySfxEvent)e;
        PlaySfx(ev.clip);
    }

    private void OnEnable() {
        bgmAudioSource.enabled = true;
        sfxAudioSource.enabled = true;
        eventManager.AddSubscriber(Events.PLAY_SFX, PlaySfxEvent);
    }

    private void OnDisable() {
        bgmAudioSource.enabled = false;
        sfxAudioSource.enabled = false;
        eventManager.RemoveSubscriber(Events.PLAY_SFX, PlaySfxEvent);
    }

}
