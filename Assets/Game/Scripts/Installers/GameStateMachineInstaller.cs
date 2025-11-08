using StateManaging.Zenject;
using Zenject;

public class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlaySongState>().AsSingle();
        Container.BindInterfacesAndSelfTo<PauseState>().AsSingle();
        Container.BindInterfacesAndSelfTo<ShowResultsState>().AsSingle();

        Container.BindStateTransition<PlaySongState, PauseState, IfPlayerPressPauseCondition>();
        Container.BindStateTransition<PauseState, PlaySongState, IfPlayerPressPauseCondition>();

        Container.BindStateTransition<PlaySongState, ShowResultsState, IfSongEndCondition>();
    }
}