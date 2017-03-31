using System.Collections.Generic;
using UnityEngine;

// http://wiki.unity3d.com/index.php/Toolbox
public class Toolbox : Singleton<Toolbox> {

    private Dictionary<System.Type, Component> cache;

    protected Toolbox() {
        cache = new Dictionary<System.Type, Component>();
    }

    public static T GetOrAddComponent<T>() where T : Component {
        Component c;
        if (Instance.cache.TryGetValue(typeof(T), out c)) {
            return (T)c;
        }
        T component = MethodExtensions.GetOrAddComponent<T>(Instance);
        Instance.cache.Add(typeof(T), component);
        return component;
    }

    public static void RegisterPrefabComponent(GameObject prefab) {
        GameObject obj = GameObject.Find(prefab.name);
        if (obj == null) {
            obj = Instantiate(prefab, Instance.transform);
            obj.name = prefab.name;
        } else {
            obj.transform.SetParent(Instance.transform);
        }
        MonoBehaviour[] components = obj.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour c in components) {
            System.Type t = c.GetType();
            if (!Instance.cache.ContainsKey(t)) {
                Instance.cache.Add(t, c);
            }
        }
    }
}
