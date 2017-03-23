using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class CreateAnimationsUtility {

    public static void FlipSprite(AnimationClip clip, int lastFrame) {
        EditorCurveBinding binding = CreateEditorCurveBinding(typeof(SpriteRenderer), "", "m_FlipX");
        AnimationCurve curve = new AnimationCurve();
        AddAnimationKey(clip.frameRate, curve, 0, 1f, AnimationUtility.TangentMode.Constant, AnimationUtility.TangentMode.Constant);
        AddAnimationKey(clip.frameRate, curve, lastFrame, 0f, AnimationUtility.TangentMode.Constant, AnimationUtility.TangentMode.Constant);
        AnimationUtility.SetEditorCurve(clip, binding, curve);
    }

    public static EditorCurveBinding CreateEditorCurveBinding(System.Type type, string path, string propertyName) {
        EditorCurveBinding binding = new EditorCurveBinding();
        binding.type = type;
        binding.path = path;
        binding.propertyName = propertyName;
        return binding;
    }

    public static void AddAnimationKey(float frameRate, AnimationCurve curve, int frame, float value, AnimationUtility.TangentMode left, AnimationUtility.TangentMode right) {
        AnimationKeyTime a = FrameToAnimationKey(frame, frameRate);
        int idx = curve.AddKey(new Keyframe(a.Time, value));
        AnimationUtility.SetKeyLeftTangentMode(curve, idx, left);
        AnimationUtility.SetKeyRightTangentMode(curve, idx, right);
    }

    public static AnimationClip CreateAnimationClip(int frameRate) {
        AnimationClip clip = new AnimationClip();
        clip.frameRate = frameRate;
        return clip;
    }

    public static void SaveAnimationClip(AnimationClip clip, string path) {
        AssetDatabase.CreateAsset(clip, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static void AddSprites(AnimationClip clip, string path, int startIdx, int endIdx, float frameRate) {
        Sprite[] sprites = AssetToSpriteArray(path);

        EditorCurveBinding spriteBinding = new EditorCurveBinding();
        spriteBinding.type = typeof(SpriteRenderer);
        spriteBinding.path = "";
        spriteBinding.propertyName = "m_Sprite";

        int numFrames = (endIdx - startIdx) + 1;
        ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[numFrames];
        for (int i = 0, idx = startIdx; idx <= endIdx; i++, idx++) {
            AnimationKeyTime akt = FrameToAnimationKey(i, frameRate);
            spriteKeyFrames[i] = new ObjectReferenceKeyframe();
            spriteKeyFrames[i].time = akt.Time;
            spriteKeyFrames[i].value = sprites[idx];
        }

        AnimationClipSettings clipSettings = new AnimationClipSettings();
        clipSettings.loopTime = true;
        clipSettings.startTime = 0f;
        clipSettings.keepOriginalPositionY = true;
        AnimationUtility.SetAnimationClipSettings(clip, clipSettings);

        // This function sets clipSettings.stopTime
        AnimationUtility.SetObjectReferenceCurve(clip, spriteBinding, spriteKeyFrames);
    }

    public static void SetClipToAnimatorControllerState(AnimationClip clip, string controllerPath, int layerIdx, string stateName) {
        var animatorState = GetAnimatorState(controllerPath, stateName, layerIdx);
        animatorState.motion = clip;
    }

    public static void SetClipToAnimatorControllerBlendTree(AnimationClip clip, string controllerPath, int layerIdx, string stateName, params int[] subBlendTreeIdx) {
        var animatorState = GetAnimatorState(controllerPath, stateName, layerIdx);
        BlendTree bt = (BlendTree)animatorState.motion;
        ChildMotion[] cm = bt.children;
        int idx;

        for (int i = 0; i < subBlendTreeIdx.Length - 1; i++) {
            idx = subBlendTreeIdx[i];
            bt = (BlendTree)cm[idx].motion;
            cm = bt.children;
        }
        idx = subBlendTreeIdx[subBlendTreeIdx.Length - 1];
        cm[idx].motion = clip;
        bt.children = cm;
    }

    private static AnimatorState GetAnimatorState(string controllerPath, string stateName, int layerIdx) {
        AnimatorController animatorController = AssetDatabase.LoadAssetAtPath<AnimatorController>(controllerPath);
        var rootStateMachine = animatorController.layers[layerIdx].stateMachine;
        foreach (var childState in rootStateMachine.states) {
            var animatorState = childState.state;
            if (animatorState.name.Equals(stateName)) {
                return animatorState;
            }
        }
        return null;
    }

    public static void CreateParentFolders(string paths) {
        string prefix = "";
        string fullpath = "";
        string prevfullpath = "";
        foreach (string path in paths.Split('/')) {
            if (path == null || path.Trim().Length == 0)
                continue;
            prevfullpath = fullpath;
            fullpath += prefix + path;
            prefix = "/";
            if (AssetDatabase.IsValidFolder(fullpath)) {
                continue;
            } else {
                AssetDatabase.CreateFolder(prevfullpath, path);
            }
        }
    }

    private static Sprite[] AssetToSpriteArray(string path) {
        Object[] rawSprites = AssetDatabase.LoadAllAssetsAtPath(path);
        // first asset is a texture2d
        Sprite[] sprites = new Sprite[rawSprites.Length - 1];
        for (int i = 0; i < sprites.Length; i++) {
            sprites[i] = (Sprite)rawSprites[i + 1];
        }
        return sprites;
    }

    // Reference: 
    // Trace clip frame rate
    // https://github.com/MattRix/UnityDecompiled/blob/753fde37d331b2100f93cc5f9eb343f1dcff5eee/UnityEditor/UnityEditorInternal/AnimationWindowState.cs
    // https://github.com/MattRix/UnityDecompiled/blob/master/UnityEditor/UnityEditorInternal/AnimationKeyTime.cs
    private struct AnimationKeyTime {
        public float FrameRate;
        public int Frame;
        public float Time;
    }

    private static AnimationKeyTime TimeToAnimationKey(float time, float frameRate) {
        return new AnimationKeyTime {
            Time = time,
            FrameRate = frameRate,
            Frame = Mathf.RoundToInt(time * frameRate)
        };
    }

    private static AnimationKeyTime FrameToAnimationKey(int frame, float frameRate) {
        return new AnimationKeyTime {
            Time = (float)frame / frameRate,
            FrameRate = frameRate,
            Frame = frame
        };
    }

    //https://docs.unity3d.com/ScriptReference/Animations.AnimatorController.html
    //[MenuItem("MyMenu/Create Controller")]
    private static void CreateControllerReference() {
        CreateParentFolders("Assets/Mecanim/");

        // Creates the controller
        var controller = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath("Assets/Mecanim/StateMachineTransitions.controller");

        // Add parameters
        controller.AddParameter("TransitionNow", AnimatorControllerParameterType.Trigger);
        controller.AddParameter("Reset", AnimatorControllerParameterType.Trigger);
        controller.AddParameter("GotoB1", AnimatorControllerParameterType.Trigger);
        controller.AddParameter("GotoC", AnimatorControllerParameterType.Trigger);

        // Add StateMachines
        var rootStateMachine = controller.layers[0].stateMachine;
        var stateMachineA = rootStateMachine.AddStateMachine("smA");
        var stateMachineB = rootStateMachine.AddStateMachine("smB");
        var stateMachineC = stateMachineB.AddStateMachine("smC");

        // Add States
        var stateA1 = stateMachineA.AddState("stateA1");
        var stateB1 = stateMachineB.AddState("stateB1");
        var stateB2 = stateMachineB.AddState("stateB2");
        stateMachineC.AddState("stateC1");
        var stateC2 = stateMachineC.AddState("stateC2"); // don’t add an entry transition, should entry to state by default

        // Add Transitions
        var exitTransition = stateA1.AddExitTransition();
        exitTransition.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0, "TransitionNow");
        exitTransition.duration = 0;

        var resetTransition = stateMachineA.AddAnyStateTransition(stateA1);
        resetTransition.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0, "Reset");
        resetTransition.duration = 0;

        var transitionB1 = stateMachineB.AddEntryTransition(stateB1);
        transitionB1.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0, "GotoB1");
        stateMachineB.AddEntryTransition(stateB2);
        stateMachineC.defaultState = stateC2;
        var exitTransitionC2 = stateC2.AddExitTransition();
        exitTransitionC2.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0, "TransitionNow");
        exitTransitionC2.duration = 0;

        var stateMachineTransition = rootStateMachine.AddStateMachineTransition(stateMachineA, stateMachineC);
        stateMachineTransition.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0, "GotoC");
        rootStateMachine.AddStateMachineTransition(stateMachineA, stateMachineB);
    }
}

