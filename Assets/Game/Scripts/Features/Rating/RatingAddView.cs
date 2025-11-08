using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RatingAddView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreLabel;
    [SerializeField] private TextMeshProUGUI _comboLabel;
    [SerializeField] private TextMeshProUGUI _accuracyLabel;

    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _button;

    public event Action<string> OnPlayerSendName;

    private void Awake()
    {
        _button.onClick.AddListener(TryGetPlayerName);
    }

    public void TryGetPlayerName()
    {
        Debug.Log("Click");
        if (_inputField.text.Length > 0)
        {
            OnPlayerSendName?.Invoke(_inputField.text);
        }
    }

    public void DisableInput()
    {
        _inputField.interactable = false;
        _button.interactable = false;
    }

    public void SetScore(int score)
    {
        _scoreLabel.text = score.ToString();
    }

    public void SetCombo(int combo)
    {
        _comboLabel.text = combo.ToString();
    }

    public void SetAccuracy(float accuracy)
    {
        _accuracyLabel.text = (accuracy * 100f).ToString("F2") + "%";
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
