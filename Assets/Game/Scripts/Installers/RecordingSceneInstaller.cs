using UnityEngine;
using Zenject;

public class RecordingSceneInstaller : MonoInstaller
{
    [SerializeField] SongRecorder songRecorder;

    public override void InstallBindings()
    {
        Container.BindInstance(songRecorder);

        RecordStateMachineInstaller.Install(Container);
    }
}
