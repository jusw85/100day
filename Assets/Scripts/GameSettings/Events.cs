using UnityEngine;

public class Events {
    public static readonly int PLAYER_HPCHANGE = EventManager.StringToHash("player_hpchange");
    public static readonly int GAME_OVER = EventManager.StringToHash("game_over");
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
    public float maxHp;
    public PlayerHpChangeEvent(float prevHp, float currentHp, float maxHp) {
        this.prevHp = prevHp;
        this.currentHp = currentHp;
        this.maxHp = maxHp;
    }
}

//[System.Serializable]
//public struct NamedHpChangeEvent {
//    public string name;
//    public PlayerHpChangeEvent ev;
//}
