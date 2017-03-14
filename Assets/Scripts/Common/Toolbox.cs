using UnityEngine;

// http://wiki.unity3d.com/index.php/Toolbox
public class Toolbox : Singleton<Toolbox> {
    protected Toolbox() { }

    public static T RegisterComponent<T>() where T : Component {
        return Instance.GetOrAddComponent<T>();
    }
}
