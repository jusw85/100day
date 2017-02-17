using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(PauseManager))]
public class MenuManager : MonoBehaviour {

    public Canvas menuCanvas;
    public Selectable firstButton;

    private PauseManager pauseManager;

    private void Awake() {
        pauseManager = GetComponent<PauseManager>();
    }

    private void Start() {
        menuCanvas.enabled = false;
    }

    private void Update() {
        if (Input.GetButtonDown("Menu")) {
            ToggleMenu();
        }
    }

    public void ToggleMenu() {
        if (menuCanvas.enabled) {
            firstButton.Select();
        }
        menuCanvas.enabled = !menuCanvas.enabled;
        if (menuCanvas.enabled) {
            firstButton.Select();
        }
        pauseManager.TogglePause();
    }

    public void Quit() {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
