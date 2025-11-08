using UnityEngine;
using Zenject;

public class GameLogicInstaller : MonoInstaller
{
    [SerializeField] Track[] tracks;

    [SerializeField] ScoreView scoreView;
    [SerializeField] ScoreConfig scoreConfig;
    [SerializeField] ComboView comboView;

    [SerializeField] Transform notesParent;
    [SerializeField] Transform effectsParent;

    [SerializeField] SongPlayer songPlayer;

    [SerializeField] Canvas canvas;

    public override void InstallBindings()
    {
        TrackManager trackManager = new(tracks);

        Container.BindInterfacesAndSelfTo<TrackManager>().FromInstance(trackManager);
        Container.BindInstance(trackManager.OnHit);
        Container.BindInstance(trackManager.OnMiss);
        Container.BindInstances(tracks);

        Container.Bind<ScoreController>().AsSingle();
        Container.BindInstance(scoreView);
        Container.BindInstance(scoreConfig);

        Container.Bind<ComboController>().AsSingle();
        Container.BindInstance(comboView);

        Container.Bind<AccuracyController>().AsSingle();

        Container.Bind<NoteFactory>().AsSingle();

        Container.BindInstance(notesParent).WhenInjectedInto<NoteFactory>();
        Container.BindInstance(effectsParent).WhenInjectedInto<TextEffect.Factory>();
        
        Container.BindInstance(songPlayer);

        Container.BindInstance(canvas);

        GameStateMachineInstaller.Install(Container);
    }
}
