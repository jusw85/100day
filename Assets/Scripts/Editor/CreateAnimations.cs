using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class CreateAnimations : EditorWindow {

    public const AnimationUtility.TangentMode CONSTANT = AnimationUtility.TangentMode.Constant;
    public const AnimationUtility.TangentMode LINEAR = AnimationUtility.TangentMode.Linear;

    [MenuItem("Window/Create Animations/CreateAll")]
    public static void CreateAll() {
        CreateIdle();
        CreateWalk();
        CreateSwordAttack1();
        CreateSwordAttack2();
        CreateSwordAttack3();
    }

    public static void CreateIdle() {
        string baseClipPath = "Assets/Animations/Player/PlayerIdle";
        int frameRate = 1;
        string spritePath = "Assets/Sprites/PlayerBase/spr_player_move.png";
        string controllerPath = "Assets/Animations/Player/PlayerBase.controller";
        string stateName = "Idle";
        int[] idx = { 40, 40, 42, 42, 44, 44, 42, 42 };
        Create4Dir(baseClipPath, frameRate, spritePath, idx, controllerPath, stateName);
    }

    public static void CreateWalk() {
        string baseClipPath = "Assets/Animations/Player/PlayerWalk";
        int frameRate = 12;
        string spritePath = "Assets/Sprites/PlayerBase/spr_player_move.png";
        string controllerPath = "Assets/Animations/Player/PlayerBase.controller";
        string stateName = "Walk";
        int[] idx = { 0, 7, 16, 23, 32, 39, 16, 23 };
        Create4Dir(baseClipPath, frameRate, spritePath, idx, controllerPath, stateName);
    }

    public static void CreateSwordAttack1() {
        string baseClipPath = "Assets/Animations/Player/PlayerSwordAttack1";
        int frameRate = 16;
        string spritePath = "Assets/Sprites/PlayerBase/spr_player_attack.png";
        string controllerPath = "Assets/Animations/Player/PlayerBase.controller";
        string stateName = "Attack1";
        int[] idx = { 0, 7, 48, 55, 96, 103, 48, 55 };
        int[] subBlendTreeIdx = { 0 };
        Create4Dir(baseClipPath, frameRate, spritePath, idx, controllerPath, stateName, subBlendTreeIdx);
    }

    public static void CreateSwordAttack2() {
        string baseClipPath = "Assets/Animations/Player/PlayerSwordAttack2";
        int frameRate = 16;
        string spritePath = "Assets/Sprites/PlayerBase/spr_player_attack.png";
        string controllerPath = "Assets/Animations/Player/PlayerBase.controller";
        string stateName = "Attack2";
        int[] idx = { 16, 23, 64, 71, 112, 119, 64, 71 };
        int[] subBlendTreeIdx = { 0 };
        Create4Dir(baseClipPath, frameRate, spritePath, idx, controllerPath, stateName, subBlendTreeIdx);
    }

    public static void CreateSwordAttack3() {
        string baseClipPath = "Assets/Animations/Player/PlayerSwordAttack3";
        int frameRate = 12;
        string spritePath = "Assets/Sprites/PlayerBase/spr_player_attack.png";
        string controllerPath = "Assets/Animations/Player/PlayerBase.controller";
        string stateName = "Attack3";
        int[] idx = { 8, 15, 56, 63, 104, 111, 56, 63 };
        int[] subBlendTreeIdx = { 0 };
        Create4Dir(baseClipPath, frameRate, spritePath, idx, controllerPath, stateName, subBlendTreeIdx);
    }

    public static void Create4Dir(string baseClipPath, int frameRate, string spritePath, int[] idx, string controllerPath, string stateName, int[] subBlendTreeIdx = null) {
        string[] dirs = { "Down", "Right", "Up", "Left" };
        int[] flipdirs = { 3 };

        int numdirs = dirs.Length;
        if (idx.Length != (numdirs * 2)) {
            Debug.LogError("Incorrect number of indices");
            return;
        }

        if (subBlendTreeIdx == null) {
            subBlendTreeIdx = new int[1];
        } else {
            System.Array.Resize(ref subBlendTreeIdx, subBlendTreeIdx.Length + 1);
        }

        for (int i = 0; i < numdirs; i++) {
            string clipPath = baseClipPath + dirs[i] + ".anim";
            AnimationClip clip = CreateAnimationsUtility.CreateAnimationClip(frameRate);
            int startIdx = idx[i * 2];
            int endIdx = idx[(i * 2) + 1];
            CreateAnimationsUtility.AddSprites(clip, spritePath, startIdx, endIdx, frameRate);
            if (System.Array.IndexOf(flipdirs, i) >= 0) {
                CreateAnimationsUtility.FlipSprite(clip, (endIdx - startIdx) + 1);
            }
            CreateAnimationsUtility.SaveAnimationClip(clip, clipPath);
            subBlendTreeIdx[subBlendTreeIdx.Length - 1] = i;
            CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clip, controllerPath, 0, stateName, subBlendTreeIdx);
        }
    }

    public static void CreateIdleReference() {
        int frameRate = 1;
        int numFrames = 1;
        int lastFrame = numFrames;
        string spritePath = "Assets/Sprites/PlayerBase/spr_player_move.png";
        string controllerPath = "Assets/Animations/Player/PlayerBase.controller";
        string stateName = "Idle";
        string clipPath;
        AnimationClip clip;

        clipPath = "Assets/Animations/Player/PlayerIdleDown.anim";
        clip = CreateAnimationsUtility.CreateAnimationClip(frameRate);
        CreateAnimationsUtility.AddSprites(clip, spritePath, 40, 40, frameRate);
        CreateAnimationsUtility.SaveAnimationClip(clip, clipPath);
        CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clip, controllerPath, 0, stateName, 0);

        clipPath = "Assets/Animations/Player/PlayerIdleRight.anim";
        clip = CreateAnimationsUtility.CreateAnimationClip(frameRate);
        CreateAnimationsUtility.AddSprites(clip, spritePath, 42, 42, frameRate);
        CreateAnimationsUtility.SaveAnimationClip(clip, clipPath);
        CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clip, controllerPath, 0, stateName, 1);

        clipPath = "Assets/Animations/Player/PlayerIdleUp.anim";
        clip = CreateAnimationsUtility.CreateAnimationClip(frameRate);
        CreateAnimationsUtility.AddSprites(clip, spritePath, 44, 44, frameRate);
        CreateAnimationsUtility.SaveAnimationClip(clip, clipPath);
        CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clip, controllerPath, 0, stateName, 2);

        clipPath = "Assets/Animations/Player/PlayerIdleLeft.anim";
        clip = CreateAnimationsUtility.CreateAnimationClip(frameRate);
        CreateAnimationsUtility.AddSprites(clip, spritePath, 42, 42, frameRate);
        CreateAnimationsUtility.FlipSprite(clip, lastFrame);
        CreateAnimationsUtility.SaveAnimationClip(clip, clipPath);
        CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clip, controllerPath, 0, stateName, 3);
    }

    // mirror about axis (non zero)
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
        CreateAnimationsUtility.AddAnimationKey(clip.frameRate, curve, 3, -1.5f, CONSTANT, CONSTANT);
        CreateAnimationsUtility.AddAnimationKey(clip.frameRate, curve, 6, 0f, CONSTANT, CONSTANT);
        AnimationUtility.SetEditorCurve(clip, binding, curve);

        binding = CreateAnimationsUtility.CreateEditorCurveBinding(typeof(Transform), "PlayerBullet", "m_LocalScale.x");
        curve = new AnimationCurve();
        CreateAnimationsUtility.AddAnimationKey(clip.frameRate, curve, 0, 1f, CONSTANT, CONSTANT);
        CreateAnimationsUtility.AddAnimationKey(clip.frameRate, curve, 3, 2.5f, CONSTANT, CONSTANT);
        CreateAnimationsUtility.AddAnimationKey(clip.frameRate, curve, 6, 1f, CONSTANT, CONSTANT);
        AnimationUtility.SetEditorCurve(clip, binding, curve);

        binding = CreateAnimationsUtility.CreateEditorCurveBinding(typeof(Transform), "PlayerBullet", "m_LocalScale.y");
        curve = new AnimationCurve();
        CreateAnimationsUtility.AddAnimationKey(clip.frameRate, curve, 0, 1f, CONSTANT, CONSTANT);
        CreateAnimationsUtility.AddAnimationKey(clip.frameRate, curve, 3, 3f, CONSTANT, CONSTANT);
        CreateAnimationsUtility.AddAnimationKey(clip.frameRate, curve, 6, 1f, CONSTANT, CONSTANT);
        AnimationUtility.SetEditorCurve(clip, binding, curve);
    }
}

