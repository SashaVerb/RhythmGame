using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "TextEffectsInstaller", menuName = "Installers/TextEffectsInstaller")]
public class TextEffectsInstaller : ScriptableObjectInstaller<TextEffectsInstaller>
{
    [SerializeField] private TextEffect _prefab;
    [SerializeField] private string _containerName;

    public override void InstallBindings()
    {
        Container.BindInstance(_prefab);
        Container.Bind<TextEffect.Factory>().AsSingle();
    }
}