using UnityEngine;

public class Events {
    public static readonly int PLAYER_HPCHANGE = EventManager.StringToHash("player_hpchange");
    public static readonly int PLAY_SFX = EventManager.StringToHash("play_sfx");
}

public class PlaySfxEvent : IGameEvent {
    public AudioClip clip;
    public PlaySfxEvent(AudioClip clip) {
        this.clip = clip;
    }
}

//[System.Serializable]
public class PlayerHpChangeEvent : IGameEvent {
    public float prevHp;
    public float currentHp;
    public PlayerHpChangeEvent(float prevHp, float currentHp) {
        this.prevHp = prevHp;
        this.currentHp = currentHp;
    }
}

//[System.Serializable]
//public struct NamedHpChangeEvent {
//    public string name;
//    public PlayerHpChangeEvent ev;
//}
