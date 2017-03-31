using InControl;

public class PlayerActions : PlayerActionSet {
    public PlayerAction Attack;
    public PlayerAction Special;
    public PlayerAction Roll;
    public PlayerAction Action;
    public PlayerAction MoveLeft;
    public PlayerAction MoveRight;
    public PlayerAction MoveUp;
    public PlayerAction MoveDown;
    public PlayerTwoAxisAction Move;
    public PlayerAction Menu;

    public PlayerActions() {
        Attack = CreatePlayerAction("Attack");
        Special = CreatePlayerAction("Special");
        Roll = CreatePlayerAction("Roll");
        Action = CreatePlayerAction("Action");
        MoveLeft = CreatePlayerAction("Move Left");
        MoveRight = CreatePlayerAction("Move Right");
        MoveUp = CreatePlayerAction("Move Up");
        MoveDown = CreatePlayerAction("Move Down");
        Move = CreateTwoAxisPlayerAction(MoveLeft, MoveRight, MoveDown, MoveUp);
        Menu = CreatePlayerAction("Menu");
    }

    //http://www.gallantgames.com/pages/incontrol-standardized-controls
    // Action1 = A, X
    // Action2 = B, Circle
    // Action3 = X, Square
    // Action4 = Y, Triangle
    public static PlayerActions CreateWithDefaultBindings() {
        var playerActions = new PlayerActions();

        playerActions.Attack.AddDefaultBinding(Key.J);
        playerActions.Attack.AddDefaultBinding(InputControlType.Action3);
        playerActions.Attack.AddDefaultBinding(Mouse.LeftButton);
        playerActions.Attack.AddDefaultBinding(InputControlType.RightTrigger);

        playerActions.Special.AddDefaultBinding(Key.U);
        playerActions.Special.AddDefaultBinding(InputControlType.Action2);
        playerActions.Special.AddDefaultBinding(Mouse.MiddleButton);

        playerActions.Roll.AddDefaultBinding(Key.K);
        playerActions.Roll.AddDefaultBinding(InputControlType.Action1);
        playerActions.Roll.AddDefaultBinding(Mouse.RightButton);
        playerActions.Roll.AddDefaultBinding(InputControlType.LeftTrigger);

        playerActions.Action.AddDefaultBinding(Key.E);
        playerActions.Action.AddDefaultBinding(InputControlType.Action4);

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

        playerActions.Menu.AddDefaultBinding(InputControlType.Start);
        playerActions.Menu.AddDefaultBinding(Key.Escape);

        return playerActions;
    }
}
