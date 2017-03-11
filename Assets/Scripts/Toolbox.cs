using UnityEngine;

// http://wiki.unity3d.com/index.php/Toolbox
public class Toolbox : Singleton<Toolbox> {
    protected Toolbox() { }

    public static T RegisterComponent<T>() where T : Component {
        return Instance.GetOrAddComponent<T>();
    }
}

public static class MethodExtensionForMonoBehaviourTransform {
    public static T GetOrAddComponent<T>(this Component obj) where T : Component {
        T result = obj.GetComponent<T>();
        if (result == null) {
            result = obj.gameObject.AddComponent<T>();
        }
        return result;
    }
}
