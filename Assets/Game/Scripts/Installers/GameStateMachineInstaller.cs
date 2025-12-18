using StateManaging;
using StateManaging.Zenject;
using Zenject;

public class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
{
    public override void InstallBindings()
    {
        var inputActions = new InputActions();
        inputActions.Player.Enable();

        PressKeyCondition pauseCondition = new PressKeyCondition(inputActions.Player.Pause);
        TimeElapsedCondition timeElapsedCondition = new TimeElapsedCondition(1f);
        
        Container.BindInterfacesAndSelfTo<ZenjectStateMachine>().AsSingle();

        Container.BindInterfacesAndSelfTo<EmptyState>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlaySongState>().AsSingle();
        Container.BindInterfacesAndSelfTo<PauseState>().AsSingle();
        Container.BindInterfacesAndSelfTo<ShowResultsState>().AsSingle();

        Container.BindStateTransition<EmptyState, PlaySongState>(timeElapsedCondition);
        Container.BindStateTransition<PlaySongState, PauseState>(pauseCondition);
        Container.BindStateTransition<PauseState, PlaySongState>(pauseCondition);

        Container.BindStateTransition<PlaySongState, ShowResultsState, IfSongEndCondition>();
    }
}