#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
#endif

public class FsmFrameInfo {

    public bool hasChanged;
    public int curr;
    public int prev;

    private int prevFrame;

    public FsmFrameInfo(int initialState) {
        hasChanged = false;
        curr = initialState;
        prev = initialState;
        prevFrame = initialState;
    }

    public void DoUpdate(int currentState) {
        if (prevFrame != curr) {
            prevFrame = curr;
        }
        curr = currentState;
        hasChanged = false;
        if (prevFrame != curr) {
            hasChanged = true;
            prev = prevFrame;
        }
    }

#if UNITY_EDITOR
    private DebugAnimatorStates debugAnimatorStates;

    public void DebugInit(Animator fsm, int layer = 0) {
        debugAnimatorStates = new DebugAnimatorStates(fsm, layer);
    }

    public override string ToString() {
        return hasChanged + " " + " " + debugAnimatorStates.Get(prev) + " " + debugAnimatorStates.Get(prevFrame) + " " + debugAnimatorStates.Get(curr);
    }

    private class DebugAnimatorStates {

        private Dictionary<int, string> states = new Dictionary<int, string>();

        public DebugAnimatorStates(Animator fsm, int layer = 0) {
            AnimatorController ac = fsm.runtimeAnimatorController as AnimatorController;
            AnimatorControllerLayer acl = ac.layers[layer];
            ChildAnimatorState[] sts = acl.stateMachine.states;
            foreach (ChildAnimatorState st in sts) {
                Add(acl.name + "." + st.state.name);
            }
        }

        public string Get(int fullPathHash) {
            string name;
            if (states.TryGetValue(fullPathHash, out name))
                return name;
            return "Unknown#" + fullPathHash;
        }

        private void Add(string stateName) {
            int hash = Animator.StringToHash(stateName);
            states.Add(hash, stateName);
        }
    }
#endif

}
