using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class CreateAnimations : EditorWindow {

    [MenuItem("Window/Create Animations/CreateAll")]
    public static void CreateAll() {
        //CreateMovement();
        CreateAttack();
    }

    [MenuItem("Window/Create Animations/ShowWindow")]
    public static void ShowWindow() {
        GetWindow(typeof(CreateAnimations));
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
            CreateMovement();
            CreateAttack();
        }
    }

    public const AnimationUtility.TangentMode CONSTANT = AnimationUtility.TangentMode.Constant;
    public const AnimationUtility.TangentMode LINEAR = AnimationUtility.TangentMode.Linear;

    public static void CreateMovement() {
        int frameRate = 12;
        string spritePath = "Assets/Sprites/spr_player_move.png";
        string clipPath = "Assets/_Test/Test.anim";
        string controllerPath = "Assets/Animations/Cube.controller";
        string stateName = "Test";

        AnimationClip clip = CreateAnimationsUtility.CreateAnimationClip(frameRate);
        CreateAnimationsUtility.AddSprites(clip, spritePath, 24, 32, frameRate);
        CreateAnimationsUtility.SaveAnimationClip(clip, clipPath);
        CreateAnimationsUtility.SetClipToAnimatorControllerState(clip, controllerPath, 0, stateName);
        //CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clip, controllerPath, 0, "Movement 0", 1);
        //CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clip, controllerPath, 0, "Movement 0", 8, 1);
        //CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clip, controllerPath, 0, "Movement 0", 8, 3, 1);
    }

    public static void CreateAttack() {
        int frameRate = 16;
        string spritePath = "Assets/Sprites/spr_player_attack.png";
        string clipPath = "Assets/_Test/Test3.anim";
        string controllerPath = "Assets/Animations/Cube.controller";
        string stateName = "Test3";

        AnimationClip clip = CreateAnimationsUtility.CreateAnimationClip(frameRate);
        CreateAnimationsUtility.AddSprites(clip, spritePath, 0, 8, frameRate);
        CreateAttackCurves(clip);
        CreateAnimationsUtility.SaveAnimationClip(clip, clipPath);
        CreateAnimationsUtility.SetClipToAnimatorControllerState(clip, controllerPath, 0, stateName);
    }

    public static void CreateAttackCurves(AnimationClip clip) {
        EditorCurveBinding binding;
        AnimationCurve curve;

        binding = CreateAnimationsUtility.CreateEditorCurveBinding(typeof(BoxCollider2D), "PlayerBullet", "m_Enabled");
        curve = new AnimationCurve();
        CreateAnimationsUtility.AddAnimationKey(clip.frameRate, curve, 0, 0f, CONSTANT, CONSTANT);
        CreateAnimationsUtility.AddAnimationKey(clip.frameRate, curve, 3, 1f, CONSTANT, CONSTANT);
        CreateAnimationsUtility.AddAnimationKey(clip.frameRate, curve, 6, 0f, CONSTANT, CONSTANT);
        AnimationUtility.SetEditorCurve(clip, binding, curve);

        binding = CreateAnimationsUtility.CreateEditorCurveBinding(typeof(Transform), "PlayerBullet", "m_LocalPosition.y");
        curve = new AnimationCurve();
        CreateAnimationsUtility.AddAnimationKey(clip.frameRate, curve, 0, 0f, CONSTANT, CONSTANT);
        CreateAnimationsUtility.AddAnimationKey(clip.frameRate, curve, 3, -1f, CONSTANT, CONSTANT);
        CreateAnimationsUtility.AddAnimationKey(clip.frameRate, curve, 6, 0f, CONSTANT, CONSTANT);
        AnimationUtility.SetEditorCurve(clip, binding, curve);
        
        binding = CreateAnimationsUtility.CreateEditorCurveBinding(typeof(Transform), "PlayerBullet", "m_LocalScale.x");
        curve = new AnimationCurve();
        CreateAnimationsUtility.AddAnimationKey(clip.frameRate, curve, 0, 1f, CONSTANT, CONSTANT);
        CreateAnimationsUtility.AddAnimationKey(clip.frameRate, curve, 3, 3f, CONSTANT, CONSTANT);
        CreateAnimationsUtility.AddAnimationKey(clip.frameRate, curve, 6, 1f, CONSTANT, CONSTANT);
        AnimationUtility.SetEditorCurve(clip, binding, curve);

        binding = CreateAnimationsUtility.CreateEditorCurveBinding(typeof(Transform), "PlayerBullet", "m_LocalScale.y");
        curve = new AnimationCurve();
        CreateAnimationsUtility.AddAnimationKey(clip.frameRate, curve, 0, 1f, CONSTANT, CONSTANT);
        CreateAnimationsUtility.AddAnimationKey(clip.frameRate, curve, 3, 4f, CONSTANT, CONSTANT);
        CreateAnimationsUtility.AddAnimationKey(clip.frameRate, curve, 6, 1f, CONSTANT, CONSTANT);
        AnimationUtility.SetEditorCurve(clip, binding, curve);
    }
}

