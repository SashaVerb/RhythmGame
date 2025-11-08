using UnityEngine;
using Zenject;

public class CameraInstaller : MonoInstaller
{
    [SerializeField] private new Camera camera;
    public override void InstallBindings()
    {
        Container.BindInstance(camera);
        CameraAreaInstaller.Install(Container);
    }
}