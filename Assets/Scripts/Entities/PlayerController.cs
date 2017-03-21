using MonsterLove.StateMachine;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerController : MonoBehaviour {

    private ControlManager controls;
    private Player player;
    private PlayerAnimator playerAnimator;
    private StateMachine<PlayerState> fsm;

    private Vector2 moveInput;
    private bool isAttackQueued;

    private void Awake() {
        player = GetComponent<Player>();
        playerAnimator = GetComponent<PlayerAnimator>();
        fsm = StateMachine<PlayerState>.Initialize(this, PlayerState.Idle);
    }

    private void Start() {
        controls = Toolbox.RegisterComponent<ControlManager>();

        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
        if (cameraFollow != null) cameraFollow.target = gameObject;
    }

    private void Update() {
        moveInput = controls.actions.Move;
        bool isAttackPressed = controls.actions.Attack.WasPressed;
        bool isAttackHeld = controls.actions.Attack.IsPressed;

        var sqrMagnitude = moveInput.sqrMagnitude;
        var state = fsm.State;

        if (isAttackPressed) {
            isAttackQueued = true;
        }

        //Debug.Log(state + " " + isAttackQueued);

        switch (state) {
            case PlayerState.Idle:
                if (isAttackQueued) {
                    fsm.ChangeState(PlayerState.Attack);
                } else if (sqrMagnitude > 0) {
                    player.Move(moveInput);
                    fsm.ChangeState(PlayerState.Walk);
                }
                break;
            case PlayerState.Walk:
                if (isAttackQueued) {
                    fsm.ChangeState(PlayerState.Attack);
                } else if (sqrMagnitude > 0) {
                    player.Move(moveInput);
                } else if (sqrMagnitude <= 0) {
                    fsm.ChangeState(PlayerState.Idle);
                }
                break;
            case PlayerState.Attack:
                if (playerAnimator.isIdle) {
                    fsm.ChangeState(PlayerState.Idle);
                }
                break;
        }

        playerAnimator.Animate(state, player);
    }

    private void Attack_Enter() {
        player.Move(moveInput);
        isAttackQueued = false;
        playerAnimator.TriggerAttack();
    }

}

public enum PlayerState {
    Idle,
    Walk,
    Attack,
}
