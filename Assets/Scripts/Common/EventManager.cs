using UnityEngine;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

    public delegate void EventDelegate(IGameEvent e);

    private Dictionary<int, EventDelegate> delegateMap = new Dictionary<int, EventDelegate>();

    public static int StringToHash(string channel) {
        return channel.GetHashCode();
    }

    public void AddSubscriber(string channel, EventDelegate del) {
        AddSubscriber(StringToHash(channel), del);
    }

    public void AddSubscriber(int channelId, EventDelegate del) {
        EventDelegate tmpDel;
        if (delegateMap.TryGetValue(channelId, out tmpDel)) {
            tmpDel -= del;
            tmpDel += del;
            delegateMap[channelId] = tmpDel;
        } else {
            delegateMap[channelId] = del;
        }
    }

    public void RemoveSubscriber(string channel, EventDelegate del) {
        RemoveSubscriber(StringToHash(channel), del);
    }

    public void RemoveSubscriber(int channelId, EventDelegate del) {
        EventDelegate tmpDel;
        if (delegateMap.TryGetValue(channelId, out tmpDel)) {
            tmpDel -= del;
            if (tmpDel == null) {
                delegateMap.Remove(channelId);
            } else {
                delegateMap[channelId] = tmpDel;
            }
        }
    }

    public void Publish(string channel, IGameEvent gameEvent) {
        Publish(StringToHash(channel), gameEvent);
    }

    public void Publish(int channelId, IGameEvent gameEvent) {
        EventDelegate tmpDel;
        if (delegateMap.TryGetValue(channelId, out tmpDel)) {
            tmpDel(gameEvent);
        }
    }
}

public interface IGameEvent {
}
