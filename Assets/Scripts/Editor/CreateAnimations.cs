using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class CreateAnimations : EditorWindow {

    public const AnimationUtility.TangentMode CONSTANT = AnimationUtility.TangentMode.Constant;
    public const AnimationUtility.TangentMode LINEAR = AnimationUtility.TangentMode.Linear;

    [MenuItem("Window/Create Animations/CreateAll")]
    public static void CreateAll() {
        //CreatePlayerIdle();
        //CreatePlayerWalk();
        //CreatePlayerSwordAttack1();
        //CreatePlayerSwordAttack2();
        //CreatePlayerSwordAttack3();
        //CreatePlayerChargeAttack();
        //CreatePlayerRoll();

        //CreateMaeriIdle();
        //CreateMaeriWalk();
        //CreateMaeriSwordAttack1();
        //CreateMaeriSwordAttack2();
        //CreateMaeriSwordAttack3();
        CreateMaeriChargeAttack();

        //CreateEnemyIdle();
        //CreateEnemyWalk();
    }

    public static void CreateEnemyIdle() {
        string baseClipPath = "Assets/Animations/Enemy/EnemyIdle";
        int frameRate = 1;
        string spritePath = "Assets/Sprites/EnemyBase/spr_enemy_move.png";
        string controllerPath = "Assets/Animations/Enemy/EnemyBase.controller";
        string stateName = "Idle";
        int[] idx = { 40, 40, 42, 42, 44, 44, 42, 42 };
        int[] subBlendTreeIdx = { 0 };

        NamedAnimationClip[] clips = Create4Dir(baseClipPath, frameRate, spritePath, idx, controllerPath, stateName);
        for (int i = 0; i < clips.Length; i++) {
            CreateAnimationsUtility.SaveAnimationClip(clips[i].clip, clips[i].clipPath);
            subBlendTreeIdx[subBlendTreeIdx.Length - 1] = i;
            CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clips[i].clip, controllerPath, 0, stateName, subBlendTreeIdx);
        }
    }

    public static void CreateEnemyWalk() {
        string baseClipPath = "Assets/Animations/Enemy/EnemyWalk";
        int frameRate = 12;
        string spritePath = "Assets/Sprites/EnemyBase/spr_enemy_move.png";
        string controllerPath = "Assets/Animations/Enemy/EnemyBase.controller";
        string stateName = "Walk";
        int[] idx = { 0, 7, 16, 23, 32, 39, 16, 23 };
        int[] subBlendTreeIdx = { 0 };

        NamedAnimationClip[] clips = Create4Dir(baseClipPath, frameRate, spritePath, idx, controllerPath, stateName);
        for (int i = 0; i < clips.Length; i++) {
            CreateAnimationsUtility.SaveAnimationClip(clips[i].clip, clips[i].clipPath);
            subBlendTreeIdx[subBlendTreeIdx.Length - 1] = i;
            CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clips[i].clip, controllerPath, 0, stateName, subBlendTreeIdx);
        }
    }

    public static void CreatePlayerIdle() {
        string baseClipPath = "Assets/Animations/Player/PlayerIdle";
        int frameRate = 1;
        string spritePath = "Assets/Sprites/Player/spr_player_move.png";
        string controllerPath = "Assets/Animations/Player/PlayerBase.controller";
        string stateName = "Idle";
        int[] idx = { 40, 40, 42, 42, 44, 44, 42, 42 };
        int[] subBlendTreeIdx = { 0 };

        NamedAnimationClip[] clips = Create4Dir(baseClipPath, frameRate, spritePath, idx, controllerPath, stateName);
        for (int i = 0; i < clips.Length; i++) {
            CreateAnimationsUtility.SaveAnimationClip(clips[i].clip, clips[i].clipPath);
            subBlendTreeIdx[subBlendTreeIdx.Length - 1] = i;
            CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clips[i].clip, controllerPath, 0, stateName, subBlendTreeIdx);
        }
    }

    public static void CreatePlayerRoll() {
        string baseClipPath = "Assets/Animations/Player/PlayerRoll";
        int frameRate = 4;
        string spritePath = "Assets/Sprites/Player/spr_player_move.png";
        string controllerPath = "Assets/Animations/Player/PlayerBase.controller";
        string stateName = "Roll";
        int[] idx = { 40, 40, 42, 42, 44, 44, 42, 42 };
        int[] subBlendTreeIdx = { 0 };

        NamedAnimationClip[] clips = Create4Dir(baseClipPath, frameRate, spritePath, idx, controllerPath, stateName);
        for (int i = 0; i < clips.Length; i++) {
            AnimationClip clip = clips[i].clip;
            string clipPath = clips[i].clipPath;
            int[] keyFrames = { 0, 1 };
            AddCurve(clip, typeof(BoxCollider2D), "Hitbox", "m_Enabled", ParamsToKeyFrames(keyFrames, 0f, 1f));
            CreateAnimationsUtility.SaveAnimationClip(clip, clipPath);
            subBlendTreeIdx[subBlendTreeIdx.Length - 1] = i;
            CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clip, controllerPath, 0, stateName, subBlendTreeIdx);
        }
    }

    public static void CreateMaeriIdle() {
        string baseClipPath = "Assets/Animations/Player/PlayerIdle";
        int frameRate = 1;
        string spritePath = "Assets/Sprites/Player/spr_maeri_move.png";
        string controllerPath = "Assets/Animations/Player/PlayerBase.controller";
        string stateName = "Idle";
        int[] idx = { 18, 18, 19, 19, 20, 20, 21, 21 };
        int[] subBlendTreeIdx = { 0 };

        NamedAnimationClip[] clips = Create4Dir(baseClipPath, frameRate, spritePath, idx, controllerPath, stateName, false);
        for (int i = 0; i < clips.Length; i++) {
            CreateAnimationsUtility.SaveAnimationClip(clips[i].clip, clips[i].clipPath);
            subBlendTreeIdx[subBlendTreeIdx.Length - 1] = i;
            CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clips[i].clip, controllerPath, 0, stateName, subBlendTreeIdx);
        }
    }

    public static void CreateMaeriWalk() {
        string baseClipPath = "Assets/Animations/Player/PlayerWalk";
        int frameRate = 12;
        string spritePath = "Assets/Sprites/Player/spr_maeri_move.png";
        string controllerPath = "Assets/Animations/Player/PlayerBase.controller";
        string stateName = "Walk";
        int[] idx = { 0, 5, 6, 11, 12, 17, 6, 11 };
        int[] subBlendTreeIdx = { 0 };

        NamedAnimationClip[] clips = Create4Dir(baseClipPath, frameRate, spritePath, idx, controllerPath, stateName);
        for (int i = 0; i < clips.Length; i++) {
            CreateAnimationsUtility.SaveAnimationClip(clips[i].clip, clips[i].clipPath);
            subBlendTreeIdx[subBlendTreeIdx.Length - 1] = i;
            CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clips[i].clip, controllerPath, 0, stateName, subBlendTreeIdx);
        }
    }

    public static void CreatePlayerWalk() {
        string baseClipPath = "Assets/Animations/Player/PlayerWalk";
        int frameRate = 12;
        string spritePath = "Assets/Sprites/Player/spr_player_move.png";
        string controllerPath = "Assets/Animations/Player/PlayerBase.controller";
        string stateName = "Walk";
        int[] idx = { 0, 7, 16, 23, 32, 39, 16, 23 };
        int[] subBlendTreeIdx = { 0 };

        NamedAnimationClip[] clips = Create4Dir(baseClipPath, frameRate, spritePath, idx, controllerPath, stateName);
        for (int i = 0; i < clips.Length; i++) {
            CreateAnimationsUtility.SaveAnimationClip(clips[i].clip, clips[i].clipPath);
            subBlendTreeIdx[subBlendTreeIdx.Length - 1] = i;
            CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clips[i].clip, controllerPath, 0, stateName, subBlendTreeIdx);
        }
    }

    public static void CreateMaeriSwordAttack1() {
        string baseClipPath = "Assets/Animations/Player/PlayerSwordAttack1";
        int frameRate = 20;
        string spritePath = "Assets/Sprites/Player/spr_maeri_attack.png";
        string controllerPath = "Assets/Animations/Player/PlayerBase.controller";
        string stateName = "Attack1";
        int[] idx = { 0, 5, 18, 23, 36, 41, 18, 23 };
        int[] subBlendTreeIdx = { 0, 0 };

        NamedAnimationClip[] clips = Create4Dir(baseClipPath, frameRate, spritePath, idx, controllerPath, stateName);
        for (int i = 0; i < clips.Length; i++) {
            AnimationClip clip = clips[i].clip;
            string clipPath = clips[i].clipPath;
            int[] keyFrames = { 0, 1, 3 };
            AddCurve(clip, typeof(BoxCollider2D), "Bullet", "m_Enabled", ParamsToKeyFrames(keyFrames, 0f, 1f, 0f));
            switch (i) {
                case 0:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -1.5f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 2.5f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 3f, 1f));
                    break;
                case 1:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.x", ParamsToKeyFrames(keyFrames, 0f, 2.5f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -2f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 4f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 2.5f, 1f));
                    break;
                case 2:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", MirrorKeyFrames(-0.25f, ParamsToKeyFrames(keyFrames, 0f, -1.5f, 0f)));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 2.5f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 3f, 1f));
                    break;
                case 3:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.x", MirrorKeyFrames(0, ParamsToKeyFrames(keyFrames, 0f, 2.5f, 0f)));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -2f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 4f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 2.5f, 1f));
                    break;
            }
            CreateAnimationsUtility.SaveAnimationClip(clip, clipPath);
            subBlendTreeIdx[subBlendTreeIdx.Length - 1] = i;
            CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clip, controllerPath, 0, stateName, subBlendTreeIdx);
        }
    }

    public static void CreateMaeriSwordAttack2() {
        string baseClipPath = "Assets/Animations/Player/PlayerSwordAttack2";
        int frameRate = 16;
        string spritePath = "Assets/Sprites/Player/spr_maeri_attack.png";
        string controllerPath = "Assets/Animations/Player/PlayerBase.controller";
        string stateName = "Attack2";
        int[] idx = { 6, 11, 24, 29, 42, 47, 24, 29 };
        int[] subBlendTreeIdx = { 0, 0 };

        NamedAnimationClip[] clips = Create4Dir(baseClipPath, frameRate, spritePath, idx, controllerPath, stateName);
        for (int i = 0; i < clips.Length; i++) {
            AnimationClip clip = clips[i].clip;
            string clipPath = clips[i].clipPath;
            int[] keyFrames = { 0, 2, 4 };
            AddCurve(clip, typeof(BoxCollider2D), "Bullet", "m_Enabled", ParamsToKeyFrames(keyFrames, 0f, 1f, 0f));
            switch (i) {
                case 0:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -3f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 3f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 3.5f, 1f));
                    break;
                case 1:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.x", ParamsToKeyFrames(keyFrames, 0f, 2.5f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -2f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 4f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 2.5f, 1f));
                    break;
                case 2:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, 1.5f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 3f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 3.5f, 1f));
                    break;
                case 3:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.x", MirrorKeyFrames(0, ParamsToKeyFrames(keyFrames, 0f, 2.5f, 0f)));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -2f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 4f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 2.5f, 1f));
                    break;
            }
            CreateAnimationsUtility.SaveAnimationClip(clip, clipPath);
            subBlendTreeIdx[subBlendTreeIdx.Length - 1] = i;
            CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clip, controllerPath, 0, stateName, subBlendTreeIdx);
        }
    }

    public static void CreateMaeriSwordAttack3() {
        string baseClipPath = "Assets/Animations/Player/PlayerSwordAttack3";
        int frameRate = 12;
        string spritePath = "Assets/Sprites/Player/spr_maeri_attack.png";
        string controllerPath = "Assets/Animations/Player/PlayerBase.controller";
        string stateName = "Attack3";
        int[] idx = { 12, 17, 30, 35, 48, 53, 30, 35 };
        int[] subBlendTreeIdx = { 0, 0 };

        NamedAnimationClip[] clips = Create4Dir(baseClipPath, frameRate, spritePath, idx, controllerPath, stateName);
        for (int i = 0; i < clips.Length; i++) {
            AnimationClip clip = clips[i].clip;
            string clipPath = clips[i].clipPath;
            int[] keyFrames = { 0, 3, 6 };
            AddCurve(clip, typeof(BoxCollider2D), "Bullet", "m_Enabled", ParamsToKeyFrames(keyFrames, 0f, 1f, 0f));
            switch (i) {
                case 0:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -3f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 3f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 3.5f, 1f));
                    break;
                case 1:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.x", ParamsToKeyFrames(keyFrames, 0f, 2.5f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -2f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 4f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 2.5f, 1f));
                    break;
                case 2:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, 1.5f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 3f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 3.5f, 1f));
                    break;
                case 3:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.x", MirrorKeyFrames(0, ParamsToKeyFrames(keyFrames, 0f, 2.5f, 0f)));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -2f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 4f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 2.5f, 1f));
                    break;
            }
            CreateAnimationsUtility.SaveAnimationClip(clip, clipPath);
            subBlendTreeIdx[subBlendTreeIdx.Length - 1] = i;
            CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clip, controllerPath, 0, stateName, subBlendTreeIdx);
        }
    }

    public static void CreateMaeriChargeAttack() {
        string baseClipPath = "Assets/Animations/Player/PlayerChargeAttack";
        int frameRate = 12;
        string spritePath = "Assets/Sprites/Player/spr_maeri_attack.png";
        string controllerPath = "Assets/Animations/Player/PlayerBase.controller";
        string stateName = "ChargeAttack";
        int[] idx = { 12, 17, 30, 35, 48, 53, 30, 35 };
        int[] subBlendTreeIdx = { 0, 0 };

        NamedAnimationClip[] clips = Create4Dir(baseClipPath, frameRate, spritePath, idx, controllerPath, stateName);
        for (int i = 0; i < clips.Length; i++) {
            AnimationClip clip = clips[i].clip;
            string clipPath = clips[i].clipPath;
            int[] keyFrames = { 0, 3, 6 };
            AddCurve(clip, typeof(BoxCollider2D), "Bullet", "m_Enabled", ParamsToKeyFrames(keyFrames, 0f, 1f, 0f));
            switch (i) {
                case 0:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -3f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 3f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 3.5f, 1f));
                    break;
                case 1:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.x", ParamsToKeyFrames(keyFrames, 0f, 2.5f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -2f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 4f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 2.5f, 1f));
                    break;
                case 2:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, 1.5f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 3f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 3.5f, 1f));
                    break;
                case 3:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.x", MirrorKeyFrames(0, ParamsToKeyFrames(keyFrames, 0f, 2.5f, 0f)));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -2f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 4f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 2.5f, 1f));
                    break;
            }
            CreateAnimationsUtility.SaveAnimationClip(clip, clipPath);
            subBlendTreeIdx[subBlendTreeIdx.Length - 1] = i;
            CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clip, controllerPath, 0, stateName, subBlendTreeIdx);
        }
    }

    public static void CreatePlayerSwordAttack1() {
        string baseClipPath = "Assets/Animations/Player/PlayerSwordAttack1";
        int frameRate = 20;
        string spritePath = "Assets/Sprites/Player/spr_player_attack.png";
        string controllerPath = "Assets/Animations/Player/PlayerBase.controller";
        string stateName = "Attack1";
        int[] idx = { 0, 7, 48, 55, 96, 103, 48, 55 };
        int[] subBlendTreeIdx = { 0, 0 };

        NamedAnimationClip[] clips = Create4Dir(baseClipPath, frameRate, spritePath, idx, controllerPath, stateName);
        for (int i = 0; i < clips.Length; i++) {
            AnimationClip clip = clips[i].clip;
            string clipPath = clips[i].clipPath;
            int[] keyFrames = { 0, 3, 6 };
            AddCurve(clip, typeof(BoxCollider2D), "Bullet", "m_Enabled", ParamsToKeyFrames(keyFrames, 0f, 1f, 0f));
            switch (i) {
                case 0:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -1.5f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 2.5f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 3f, 1f));
                    break;
                case 1:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.x", ParamsToKeyFrames(keyFrames, 0f, 2.5f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -2f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 4f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 2.5f, 1f));
                    break;
                case 2:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", MirrorKeyFrames(-0.25f, ParamsToKeyFrames(keyFrames, 0f, -1.5f, 0f)));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 2.5f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 3f, 1f));
                    break;
                case 3:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.x", MirrorKeyFrames(0, ParamsToKeyFrames(keyFrames, 0f, 2.5f, 0f)));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -2f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 4f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 2.5f, 1f));
                    break;
            }
            CreateAnimationsUtility.SaveAnimationClip(clip, clipPath);
            subBlendTreeIdx[subBlendTreeIdx.Length - 1] = i;
            CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clip, controllerPath, 0, stateName, subBlendTreeIdx);
        }
    }

    public static void CreatePlayerSwordAttack2() {
        string baseClipPath = "Assets/Animations/Player/PlayerSwordAttack2";
        int frameRate = 16;
        string spritePath = "Assets/Sprites/Player/spr_player_attack.png";
        string controllerPath = "Assets/Animations/Player/PlayerBase.controller";
        string stateName = "Attack2";
        int[] idx = { 16, 23, 64, 71, 112, 119, 64, 71 };
        int[] subBlendTreeIdx = { 0, 0 };

        NamedAnimationClip[] clips = Create4Dir(baseClipPath, frameRate, spritePath, idx, controllerPath, stateName);
        for (int i = 0; i < clips.Length; i++) {
            AnimationClip clip = clips[i].clip;
            string clipPath = clips[i].clipPath;
            int[] keyFrames = { 0, 2, 4 };
            AddCurve(clip, typeof(BoxCollider2D), "Bullet", "m_Enabled", ParamsToKeyFrames(keyFrames, 0f, 1f, 0f));
            switch (i) {
                case 0:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -3f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 3f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 3.5f, 1f));
                    break;
                case 1:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.x", ParamsToKeyFrames(keyFrames, 0f, 2.5f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -2f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 4f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 2.5f, 1f));
                    break;
                case 2:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, 1.5f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 3f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 3.5f, 1f));
                    break;
                case 3:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.x", MirrorKeyFrames(0, ParamsToKeyFrames(keyFrames, 0f, 2.5f, 0f)));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -2f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 4f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 2.5f, 1f));
                    break;
            }
            CreateAnimationsUtility.SaveAnimationClip(clip, clipPath);
            subBlendTreeIdx[subBlendTreeIdx.Length - 1] = i;
            CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clip, controllerPath, 0, stateName, subBlendTreeIdx);
        }
    }

    public static void CreatePlayerSwordAttack3() {
        string baseClipPath = "Assets/Animations/Player/PlayerSwordAttack3";
        int frameRate = 12;
        string spritePath = "Assets/Sprites/Player/spr_player_attack.png";
        string controllerPath = "Assets/Animations/Player/PlayerBase.controller";
        string stateName = "Attack3";
        int[] idx = { 8, 15, 56, 63, 104, 111, 56, 63 };
        int[] subBlendTreeIdx = { 0, 0 };

        NamedAnimationClip[] clips = Create4Dir(baseClipPath, frameRate, spritePath, idx, controllerPath, stateName);
        for (int i = 0; i < clips.Length; i++) {
            AnimationClip clip = clips[i].clip;
            string clipPath = clips[i].clipPath;
            int[] keyFrames = { 0, 3, 6 };
            AddCurve(clip, typeof(BoxCollider2D), "Bullet", "m_Enabled", ParamsToKeyFrames(keyFrames, 0f, 1f, 0f));
            switch (i) {
                case 0:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -3f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 3f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 3.5f, 1f));
                    break;
                case 1:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.x", ParamsToKeyFrames(keyFrames, 0f, 2.5f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -2f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 4f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 2.5f, 1f));
                    break;
                case 2:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, 1.5f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 3f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 3.5f, 1f));
                    break;
                case 3:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.x", MirrorKeyFrames(0, ParamsToKeyFrames(keyFrames, 0f, 2.5f, 0f)));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -2f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 4f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 2.5f, 1f));
                    break;
            }
            CreateAnimationsUtility.SaveAnimationClip(clip, clipPath);
            subBlendTreeIdx[subBlendTreeIdx.Length - 1] = i;
            CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clip, controllerPath, 0, stateName, subBlendTreeIdx);
        }
    }

    public static void CreatePlayerChargeAttack() {
        string baseClipPath = "Assets/Animations/Player/PlayerChargeAttack";
        int frameRate = 12;
        string spritePath = "Assets/Sprites/Player/spr_player_attack.png";
        string controllerPath = "Assets/Animations/Player/PlayerBase.controller";
        string stateName = "ChargeAttack";
        int[] idx = { 8, 15, 56, 63, 104, 111, 56, 63 };
        int[] subBlendTreeIdx = { 0, 0 };

        NamedAnimationClip[] clips = Create4Dir(baseClipPath, frameRate, spritePath, idx, controllerPath, stateName);
        for (int i = 0; i < clips.Length; i++) {
            AnimationClip clip = clips[i].clip;
            string clipPath = clips[i].clipPath;
            int[] keyFrames = { 0, 3, 6 };
            AddCurve(clip, typeof(BoxCollider2D), "Bullet", "m_Enabled", ParamsToKeyFrames(keyFrames, 0f, 1f, 0f));
            switch (i) {
                case 0:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -3f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 3f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 3.5f, 1f));
                    break;
                case 1:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.x", ParamsToKeyFrames(keyFrames, 0f, 2.5f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -2f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 4f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 2.5f, 1f));
                    break;
                case 2:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, 1.5f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 3f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 3.5f, 1f));
                    break;
                case 3:
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.x", MirrorKeyFrames(0, ParamsToKeyFrames(keyFrames, 0f, 2.5f, 0f)));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalPosition.y", ParamsToKeyFrames(keyFrames, 0f, -2f, 0f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.x", ParamsToKeyFrames(keyFrames, 1f, 4f, 1f));
                    AddCurve(clip, typeof(Transform), "Bullet", "m_LocalScale.y", ParamsToKeyFrames(keyFrames, 1f, 2.5f, 1f));
                    break;
            }
            CreateAnimationsUtility.SaveAnimationClip(clip, clipPath);
            subBlendTreeIdx[subBlendTreeIdx.Length - 1] = i;
            CreateAnimationsUtility.SetClipToAnimatorControllerBlendTree(clip, controllerPath, 0, stateName, subBlendTreeIdx);
        }
    }

    public struct NamedAnimationClip {
        public AnimationClip clip;
        public string clipPath;
    }

    public static NamedAnimationClip[] Create4Dir(string baseClipPath, int frameRate, string spritePath, int[] idx, string controllerPath, string stateName, bool doFlip = true) {
        string[] dirs = { "Down", "Right", "Up", "Left" };
        int[] flipdirs = { 3 };

        int numdirs = dirs.Length;
        NamedAnimationClip[] result = new NamedAnimationClip[numdirs];

        if (idx.Length != (numdirs * 2)) {
            Debug.LogError("Incorrect number of indices");
            return result;
        }

        for (int i = 0; i < numdirs; i++) {
            string clipPath = baseClipPath + dirs[i] + ".anim";
            AnimationClip clip = CreateAnimationsUtility.CreateAnimationClip(frameRate);
            int startIdx = idx[i * 2];
            int endIdx = idx[(i * 2) + 1];
            CreateAnimationsUtility.AddSprites(clip, spritePath, startIdx, endIdx, frameRate);
            if (doFlip && System.Array.IndexOf(flipdirs, i) >= 0) {
                CreateAnimationsUtility.FlipSprite(clip, (endIdx - startIdx) + 1);
            }
            NamedAnimationClip n = new NamedAnimationClip {
                clip = clip,
                clipPath = clipPath
            };
            result[i] = n;
        }
        return result;
    }

    public static void CreateIdleReference() {
        int frameRate = 1;
        int numFrames = 1;
        int lastFrame = numFrames;
        string spritePath = "Assets/Sprites/Player/spr_player_move.png";
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

    public struct SimpleKeyFrame {
        public int frame;
        public float value;
        public AnimationUtility.TangentMode left;
        public AnimationUtility.TangentMode right;
        public SimpleKeyFrame(int frame, float value) {
            this.frame = frame;
            this.value = value;
            left = CONSTANT;
            right = CONSTANT;
        }
    }

    public static SimpleKeyFrame[] ParamsToKeyFrames(int[] keyFrames, params float[] keyFrameParams) {
        if (keyFrameParams.Length != keyFrames.Length) {
            return null;
        }
        SimpleKeyFrame[] result = new SimpleKeyFrame[keyFrames.Length];
        for (int i = 0; i < keyFrames.Length; i++) {
            int frame = keyFrames[i];
            float value = keyFrameParams[i];
            result[i] = new SimpleKeyFrame(frame, value);
        }
        return result;
    }

    public static SimpleKeyFrame[] ParamsToKeyFrames(params float[] keyFrameParams) {
        if (keyFrameParams.Length % 2 != 0) {
            return null;
        }
        SimpleKeyFrame[] result = new SimpleKeyFrame[keyFrameParams.Length / 2];
        for (int i = 0; i < keyFrameParams.Length; i += 2) {
            int frame = Mathf.RoundToInt(keyFrameParams[i]);
            float value = keyFrameParams[i + 1];
            SimpleKeyFrame k = new SimpleKeyFrame(frame, value);
            result[i / 2] = k;
        }
        return result;
    }

    public static SimpleKeyFrame[] MirrorKeyFrames(float axis, SimpleKeyFrame[] keyFrameParams) {
        for (int i = 1; i < keyFrameParams.Length; i += 2) {
            keyFrameParams[i].value = (2 * axis) - keyFrameParams[i].value;
        }
        return keyFrameParams;
    }

    public static void AddCurve(AnimationClip clip, System.Type type, string path, string propertyName, SimpleKeyFrame[] keyFrames) {
        EditorCurveBinding binding = CreateAnimationsUtility.CreateEditorCurveBinding(type, path, propertyName);
        AnimationCurve curve = new AnimationCurve();
        foreach (SimpleKeyFrame keyFrame in keyFrames) {
            CreateAnimationsUtility.AddAnimationKey(clip.frameRate, curve, keyFrame.frame, keyFrame.value, keyFrame.left, keyFrame.right);
        }
        AnimationUtility.SetEditorCurve(clip, binding, curve);
    }
}
