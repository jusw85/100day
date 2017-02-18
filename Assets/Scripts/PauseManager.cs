using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

    public void TogglePause() {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        PlayerController.Instance.isPaused = !PlayerController.Instance.isPaused;
    }

    public void Pause() {
        Time.timeScale = 0f;
        PlayerController.Instance.isPaused = true;
    }

    public void Unpause() {
        Time.timeScale = 1f;
        PlayerController.Instance.isPaused = false;
    }

}
