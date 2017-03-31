using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour {

    public GameObject HUDPrefab;
    public GameObject MenuPrefab;
    public GameObject GameOverPrefab;

    private ControlManager c;
    private GameObject menu;
    private GameObject gameOver;

    private EventManager eventManager;
    private AudioManager audioManager;

    public AudioClip bgm;

    private void Awake() {
        c = GetComponentInChildren<ControlManager>();
        eventManager = Toolbox.GetOrAddComponent<EventManager>();
        audioManager = GetComponentInChildren<AudioManager>();
    }

    private void OnEnable() {
        eventManager.AddSubscriber(Events.GAME_OVER, GameOver);
    }

    private void OnDisable() {
        eventManager.RemoveSubscriber(Events.GAME_OVER, GameOver);
    }

    private void Start() {
        audioManager.PlayBgm(bgm);
        CreateUI();
    }

    private void CreateUI() {
        GameObject UI = new GameObject();
        UI.name = "UI Container";
        CreateUIChild(UI, HUDPrefab);
        menu = CreateUIChild(UI, MenuPrefab);
        menu.SetActive(false);
        gameOver = CreateUIChild(UI, GameOverPrefab);
        gameOver.SetActive(false);
    }

    private GameObject CreateUIChild(GameObject parent, GameObject child) {
        var obj = GameObject.Find(child.name);
        if (obj == null) {
            obj = Instantiate(child);
        }
        obj.transform.SetParent(parent.transform);
        return obj;
    }

    private void Update() {
        if (c.isMenuPressed && !isGameOver) {
            ToggleMenu();
        }
        if (!isPaused) {
            pc.DoUpdate(c);
        }
    }

    private bool isPaused;
    public void TogglePause() {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        isPaused = !isPaused;
    }
    public void Pause() {
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void Unpause() {
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ToggleMenu() {
        menu.SetActive(!menu.activeSelf);
        TogglePause();
    }

    private bool isGameOver = false;
    private void GameOver(IGameEvent ev) {
        if (menu.activeSelf) {
            ToggleMenu();
        }
        gameOver.SetActive(true);
        isGameOver = true;
        Pause();
    }

    private PlayerController pc;
    public void RegisterPlayer(PlayerController pc) {
        this.pc = pc;
    }


    public void Quit() {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
