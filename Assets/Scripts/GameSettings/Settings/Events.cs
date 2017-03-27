using UnityEngine;

public class Events {
    public static readonly int PLAYER_HPCHANGE = EventManager.StringToHash("player_hpchange");
    public static readonly int PLAYER_ATTACK = EventManager.StringToHash("player_attack");
}

public class PlayerAttackEvent : IGameEvent {
    public int attackNumber;
    public PlayerAttackEvent(int attackNumber) {
        this.attackNumber = attackNumber;
    }
}

[System.Serializable]
public class PlayerHpChangeEvent : IGameEvent {
    public int prevHp;
    public int currentHp;
    public PlayerHpChangeEvent(int prevHp, int currentHp) {
        this.prevHp = prevHp;
        this.currentHp = currentHp;
    }
}

[System.Serializable]
public struct NamedHpChangeEvent {
    public string name;
    public PlayerHpChangeEvent ev;
}
