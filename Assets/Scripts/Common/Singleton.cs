using UnityEngine;

// http://wiki.unity3d.com/index.php/Toolbox
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

    private static T instance;
    private static object lockobj = new object();
    private static bool applicationIsQuitting = false;

    public static T Instance {
        get {
            if (applicationIsQuitting) {
                return null;
            }

            lock (lockobj) {
                if (instance == null) {
                    instance = (T)FindObjectOfType(typeof(T));
                    if (instance == null) {
                        GameObject singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();

                        DontDestroyOnLoad(singleton);
                    }
                }
                return instance;
            }
        }
    }

    public void OnDestroy() {
        applicationIsQuitting = true;
    }

}
