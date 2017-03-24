public class Events {
    public static readonly int HPCHANGE_ID = EventManager.StringToHash("hpchange");
}

[System.Serializable]
public class HpChangeEvent : IGameEvent {
    public int prevHp;
    public int currentHp;
    public HpChangeEvent(int prevHp, int currentHp) {
        this.prevHp = prevHp;
        this.currentHp = currentHp;
    }
}

[System.Serializable]
public struct NamedHpChangeEvent {
    public string name;
    public HpChangeEvent ev;
}
