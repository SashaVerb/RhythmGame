using StateManaging;
using Zenject;
using ITickable = Zenject.ITickable;

public class GameStateMachine : ITickable, IInitializable
{
    StateMachine stateMachine;

    GameStateMachine(IState[] states, ITransition[] transitions)
    {
        stateMachine = new(states, transitions);
    }

    public void Initialize()
    {
        stateMachine.ChangeState<PlaySongState>();
    }

    public void Tick()
    {
        stateMachine.Tick();
    }
}
