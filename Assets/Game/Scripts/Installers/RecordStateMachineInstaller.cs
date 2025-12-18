using StateManaging;
using StateManaging.Zenject;
using Zenject;

public class RecordStateMachineInstaller : Installer<RecordStateMachineInstaller>
{
    public override void InstallBindings()
    {
        var inputActions = new InputActions();
        inputActions.Player.Enable();

        PressKeyCondition toggleRecordCondition = new PressKeyCondition(inputActions.Player.Space);

        Container.BindInterfacesAndSelfTo<ZenjectStateMachine>().AsSingle();

        Container.BindInterfacesAndSelfTo<EmptyState>().AsSingle();
        Container.BindInterfacesAndSelfTo<RecordSongState>().AsSingle();

        Container.BindStateTransition<EmptyState, RecordSongState>(toggleRecordCondition);
        Container.BindStateTransition<RecordSongState, EmptyState>(toggleRecordCondition);
    }
}