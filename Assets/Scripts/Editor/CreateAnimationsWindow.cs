using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

/**
 * Reference for creating windows
 */
public class CreateAnimationsWindow : EditorWindow {

    [MenuItem("Window/Create Animations/ShowWindow")]
    public static void ShowWindow() {
        GetWindow(typeof(CreateAnimationsWindow));
    }

    const string prefs_prefix = "Helper_CreateAnimations_";
    string frameRateStr = "1";

    // https://docs.unity3d.com/ScriptReference/EditorPrefs.html
    // On Windows, EditorPrefs are stored in the registry under the HKCU\Software\Unity Technologies\UnityEditor N.x key where N.x is the major version number.
    void OnEnable() {
        frameRateStr = EditorPrefs.GetString(prefs_prefix + "FrameRate");
    }

    void OnDisable() {
        EditorPrefs.SetString(prefs_prefix + "FrameRate", frameRateStr);
    }

    public void OnGUI() {
        GUILayout.Label("Frame Rate");
        frameRateStr = GUILayout.TextField(frameRateStr);

        var go = GUILayout.Button("Go");
        if (go) {
            int frameRate;
            if (!int.TryParse(frameRateStr, out frameRate)) {
                frameRate = 1;
            }
            // Undo.RegisterCompleteObjectUndo(c, "Create Animations");
            CreateAnimations.CreatePlayerIdle();
            CreateAnimations.CreatePlayerSwordAttack1();
        }
    }

}

