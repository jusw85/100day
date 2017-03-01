using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSDisplay : MonoBehaviour {

#if UNITY_EDITOR
    public float updateInterval = 0.5F;

    private double lastInterval;
    private int frames = 0;
    private float fps;

    private void Start() {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
    }

    private void OnGUI() {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 50);
        style.alignment = TextAnchor.UpperRight;
        style.fontSize = h * 2 / 50;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);

        string text = "" + fps.ToString("f2") + " fps";
        GUI.Label(rect, text, style);
    }

    private void Update() {
        ++frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval) {
            fps = (float)(frames / (timeNow - lastInterval));
            frames = 0;
            lastInterval = timeNow;
        }
    }
#endif

}
