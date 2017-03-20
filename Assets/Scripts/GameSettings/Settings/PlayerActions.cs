using InControl;

public class PlayerActions : PlayerActionSet {
    public PlayerAction Attack;
    public PlayerAction Attack2;
    public PlayerAction MoveLeft;
    public PlayerAction MoveRight;
    public PlayerAction MoveUp;
    public PlayerAction MoveDown;
    public PlayerTwoAxisAction Move;

    public PlayerActions() {
        Attack = CreatePlayerAction("Attack");
        Attack2 = CreatePlayerAction("Attack 2");
        MoveLeft = CreatePlayerAction("Move Left");
        MoveRight = CreatePlayerAction("Move Right");
        MoveUp = CreatePlayerAction("Move Up");
        MoveDown = CreatePlayerAction("Move Down");
        Move = CreateTwoAxisPlayerAction(MoveLeft, MoveRight, MoveDown, MoveUp);
    }

    public static PlayerActions CreateWithDefaultBindings() {
        var playerActions = new PlayerActions();

        playerActions.Attack.AddDefaultBinding(Key.J);
        playerActions.Attack.AddDefaultBinding(InputControlType.Action1);
        playerActions.Attack.AddDefaultBinding(Mouse.LeftButton);
        playerActions.Attack.AddDefaultBinding(InputControlType.LeftTrigger);

        playerActions.Attack2.AddDefaultBinding(Key.K);
        playerActions.Attack2.AddDefaultBinding(InputControlType.Action2);
        playerActions.Attack2.AddDefaultBinding(Mouse.RightButton);
        playerActions.Attack2.AddDefaultBinding(InputControlType.RightTrigger);

        playerActions.MoveUp.AddDefaultBinding(Key.UpArrow);
        playerActions.MoveDown.AddDefaultBinding(Key.DownArrow);
        playerActions.MoveLeft.AddDefaultBinding(Key.LeftArrow);
        playerActions.MoveRight.AddDefaultBinding(Key.RightArrow);

        playerActions.MoveUp.AddDefaultBinding(Key.W);
        playerActions.MoveDown.AddDefaultBinding(Key.S);
        playerActions.MoveLeft.AddDefaultBinding(Key.A);
        playerActions.MoveRight.AddDefaultBinding(Key.D);

        playerActions.MoveLeft.AddDefaultBinding(InputControlType.LeftStickLeft);
        playerActions.MoveRight.AddDefaultBinding(InputControlType.LeftStickRight);
        playerActions.MoveUp.AddDefaultBinding(InputControlType.LeftStickUp);
        playerActions.MoveDown.AddDefaultBinding(InputControlType.LeftStickDown);

        playerActions.MoveLeft.AddDefaultBinding(InputControlType.DPadLeft);
        playerActions.MoveRight.AddDefaultBinding(InputControlType.DPadRight);
        playerActions.MoveUp.AddDefaultBinding(InputControlType.DPadUp);
        playerActions.MoveDown.AddDefaultBinding(InputControlType.DPadDown);

        return playerActions;
    }
}
