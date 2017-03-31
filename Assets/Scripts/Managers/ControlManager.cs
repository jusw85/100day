using UnityEngine;
using InControl;

public class ControlManager : MonoBehaviour {

    public Vector2 move;
    public bool isMoved;
    public bool isAttackPressed;
    public bool isAttackReleased;
    public bool isAttackHeld;
    public bool isRollPressed;
    public bool isSpecialPressed;
    public bool isMenuPressed;

    private PlayerActions actions;

    private void Awake() {
        move = Vector2.zero;
        actions = PlayerActions.CreateWithDefaultBindings();
    }

    private void Update() {
        isAttackPressed = actions.Attack.WasPressed;
        isAttackReleased = actions.Attack.WasReleased;
        isAttackHeld = actions.Attack.IsPressed;
        isRollPressed = actions.Roll.WasPressed;
        isSpecialPressed = actions.Special.WasPressed;
        isMenuPressed = actions.Menu.WasPressed;

        move = actions.Move;
        isMoved = move.sqrMagnitude > 0;
    }

    private void LateUpdate() {
        // maybe get mouse to world position here
        // script to run after camera movements
    }

}
