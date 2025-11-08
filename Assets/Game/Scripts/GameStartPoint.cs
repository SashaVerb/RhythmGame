using UnityEngine;
using Zenject;

public class GameStartPoint : MonoBehaviour
{
    private DiContainer _container;

    [Inject]
    private void Init(DiContainer container)
    {
        _container = container;
    }

    private void Awake()
    {
        
    }


}
