using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = nameof(GlobalSettingInstaller), menuName = SOMenuPath.Installers + nameof(GlobalSettingInstaller))]
public class GlobalSettingInstaller : ScriptableObjectInstaller<GlobalSettingInstaller>
{
    [SerializeField] NotePrefabBinding notePrefabBinding;
    [SerializeField] SongInfo song;

    public override void InstallBindings()
    {
        Container.BindInstance(notePrefabBinding);
        Container.BindInstance(song);
        Container.Bind<SongManager>().AsSingle();
    }
}