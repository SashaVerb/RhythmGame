using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private Binding[] _bindings;
    [Inject] private SongManager _songManager;
    private void Awake()
    {
        foreach (var binding in _bindings)
        {
            binding.button.onClick.AddListener(() =>
            {
                _songManager.CurrentSong = binding.song;
                SceneManager.LoadScene("Game");
            });
        }
    }

    [System.Serializable]
    public class Binding
    {
        public Button button;
        public SongInfo song;
    }
}
