using UnityEngine;
using InControl;

public class ControlManager : MonoBehaviour {

    public PlayerActions actions;

    private InControlManager incontrolManager;

    private void Awake() {
        incontrolManager = gameObject.AddComponent<InControlManager>();
        incontrolManager.dontDestroyOnLoad = true;

        actions = PlayerActions.CreateWithDefaultBindings();
    }

    private void LateUpdate() {
        // maybe get mouse position here
        // script to run after camera follow
    }

}
