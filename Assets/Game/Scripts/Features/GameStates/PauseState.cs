using StateManaging;

public class PauseState : IState
{
    IPausable[] pausableObjects;

    public PauseState(IPausable[] pausableObjects)
    {
        this.pausableObjects = pausableObjects;
    }

    public void Enter()
    {
        foreach (var obj in pausableObjects)
        {
            obj.Pause();
        }
    }

    public void Exit()
    {
        foreach (var obj in pausableObjects)
        {
            obj.Resume();
        }
    }

    public void Tick()
    {
        
    }
}
