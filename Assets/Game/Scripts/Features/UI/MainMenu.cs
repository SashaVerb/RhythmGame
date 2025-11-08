using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainScreen;
    [SerializeField] private GameObject _levelScreen;
    [SerializeField] private GameObject _recordScreen;

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _recordButton;

    [SerializeField] private Button _backFromRecordButton;
    [SerializeField] private Button _backFromLevelButton;

    private void Awake()
    {
        ShowMainScreen();

        _playButton.onClick.AddListener(ShowLevelScreen);
        _recordButton.onClick.AddListener(ShowRecordScreen);

        _backFromRecordButton.onClick.AddListener(ShowMainScreen);
        _backFromLevelButton.onClick.AddListener(ShowMainScreen);
    }

    private void ShowMainScreen()
    {
        _mainScreen.SetActive(true);
        _levelScreen.SetActive(false);
        _recordScreen.SetActive(false);
    }

    private void ShowRecordScreen()
    {
        _mainScreen.SetActive(false);
        _levelScreen.SetActive(false);
        _recordScreen.SetActive(true);
    }

    private void ShowLevelScreen()
    {
        _mainScreen.SetActive(false);
        _levelScreen.SetActive(true);
        _recordScreen.SetActive(false);
    }
}
