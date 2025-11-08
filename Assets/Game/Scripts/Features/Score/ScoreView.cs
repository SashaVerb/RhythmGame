using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI label;
    [SerializeField] string textBeforeScore;
    [SerializeField] int numberCount;

    public void SetScore(int score)
    {
        string result = textBeforeScore;
        var scoreString = score.ToString();
        for(int i = score; i < numberCount; i++)
        {
            result += '0';
        }
        result += scoreString;

        label.text = result;
    }
}
