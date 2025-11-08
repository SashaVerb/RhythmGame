using StateManaging;

public class IfPlayerPressPauseCondition : ICondition
{
    InputActions inputActions;

    public IfPlayerPressPauseCondition()
    {
        inputActions = new();
        inputActions.Player.Enable();
    }

    public bool Check()
    {
        return inputActions.Player.Pause.WasPressedThisFrame();
    }
}
