using MonsterLove.StateMachine;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerController : MonoBehaviour {

    private ControlManager controls;
    private Player player;
    private PlayerAnimator playerAnimator;
    private StateMachine<PlayerState> fsm;

    private void Awake() {
        player = GetComponent<Player>();
        playerAnimator = GetComponent<PlayerAnimator>();
        fsm = StateMachine<PlayerState>.Initialize(player, PlayerState.Idle);
    }

    private void Start() {
        controls = Toolbox.RegisterComponent<ControlManager>();

        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
        if (cameraFollow != null) cameraFollow.target = gameObject;
    }

    private void Update() {
        Vector2 moveInput = controls.actions.Move;

        var sqrMagnitude = moveInput.sqrMagnitude;
        var state = fsm.State;

        switch (state) {
            case PlayerState.Idle:
                if (sqrMagnitude > 0) {
                    fsm.ChangeState(PlayerState.Walk);
                    player.Move(moveInput);
                }
                break;
            case PlayerState.Walk:
                if (sqrMagnitude > 0) {
                    player.Move(moveInput);
                } else {
                    fsm.ChangeState(PlayerState.Idle);
                }
                break;
        }

        playerAnimator.Animate(state, player);
    }
}

public enum PlayerState {
    Idle,
    Walk,
}
