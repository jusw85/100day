using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

    public void TogglePause() {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        Player.Instance.isPaused = !Player.Instance.isPaused;
    }

    public void Pause() {
        Time.timeScale = 0f;
        Player.Instance.isPaused = true;
    }

    public void Unpause() {
        Time.timeScale = 1f;
        Player.Instance.isPaused = false;
    }

}
