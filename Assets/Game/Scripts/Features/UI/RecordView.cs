using TMPro;
using UnityEngine;

public class RecordView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameLabel;
    [SerializeField] private TextMeshProUGUI _scoreLabel;

    public void SetInfo(string name, int score)
    {
        _nameLabel.text = name;
        _scoreLabel.text = score.ToString();
    }
}
