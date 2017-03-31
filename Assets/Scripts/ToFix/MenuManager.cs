using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(PauseManager))]
public class MenuManager : MonoBehaviour {

    //    private static MenuManager instance;
    //    public static MenuManager Instance { get { return instance; } }

    //    public GameObject menuCanvas;
    //    public GameObject gameoverCanvas;
    //    public Selectable firstMenuButton;
    //    public Selectable firstGameoverButton;

    //    private PauseManager pauseManager;
    //    private bool isGameOver = false;

    //    private void Awake() {
    //        if (instance != null && instance != this) {
    //            Destroy(gameObject);
    //        } else {
    //            instance = this;
    //        }

    //        pauseManager = GetComponent<PauseManager>();
    //    }

    //    private void Start() {
    //        menuCanvas.SetActive(false);
    //        gameoverCanvas.SetActive(false);
    //    }

    //    private void Update() {
    //        if (Input.GetButtonDown("Menu") && !isGameOver) {
    //            ToggleMenu();
    //        }
    //    }

    //    public void ToggleMenu() {
    //        if (menuCanvas.activeSelf) {
    //            firstMenuButton.Select();
    //        }
    //        menuCanvas.SetActive(!menuCanvas.activeSelf);
    //        if (menuCanvas.activeSelf) {
    //            firstMenuButton.Select();
    //        }
    //        pauseManager.TogglePause();
    //    }

    //    public void GameOver() {
    //        if (menuCanvas.activeSelf) {
    //            ToggleMenu();
    //        }
    //        gameoverCanvas.SetActive(true);
    //        firstGameoverButton.Select();
    //        isGameOver = true;
    //        pauseManager.Pause();
    //    }

    //    public void Restart() {
    //        pauseManager.Unpause();
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    }

    //    public void Quit() {
    //#if UNITY_EDITOR
    //        EditorApplication.isPlaying = false;
    //#else
    //        Application.Quit();
    //#endif
    //    }

}
