using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

    public void TogglePause() {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        PlayerController.Instance.isPaused = !PlayerController.Instance.isPaused;
    }

}
