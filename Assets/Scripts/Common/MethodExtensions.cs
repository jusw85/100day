using UnityEngine;

public static class MethodExtensions {

    public static T GetOrAddComponent<T>(this GameObject obj) where T : Component {
        T result = obj.GetComponentInChildren<T>();
        if (result == null) {
            result = obj.AddComponent<T>();
        }
        return result;
    }

    public static T GetOrAddComponent<T>(this Component component) where T : Component {
        T result = component.GetComponentInChildren<T>();
        if (result == null) {
            result = component.gameObject.AddComponent<T>();
        }
        return result;
    }

    public static T AddChildComponent<T>(this GameObject obj) where T : Component {
        GameObject child = new GameObject();
        child.transform.SetParent(obj.transform);
        T result = child.AddComponent<T>();
        result.name = typeof(T).ToString();
        return result;
    }

}
