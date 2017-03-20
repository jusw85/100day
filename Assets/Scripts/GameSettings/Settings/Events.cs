public class Events {
    public static readonly int HPCHANGE_ID = EventManager.StringToHash("hpchange");
}

public class HpChangeEvent : IGameEvent {
    public int prevHp;
    public int currentHp;
    public HpChangeEvent(int prevHp, int currentHp) {
        this.prevHp = prevHp;
        this.currentHp = currentHp;
    }
}
