using StateManaging;
using System;

public class IfPlayerPressPauseCondition : ICondition, IDisposable
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

    public void Dispose()
    {
        inputActions.Player.Disable();
    }
}
