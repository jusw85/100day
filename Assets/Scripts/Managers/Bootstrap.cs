using UnityEngine;

public class Bootstrap : MonoBehaviour {

    public GameObject gameManager;

    private void Awake() {
        Toolbox.RegisterPrefabComponent(gameManager);
        Destroy(gameObject);
    }

}
