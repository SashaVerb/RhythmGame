using UnityEngine;

[CreateAssetMenu(fileName = nameof(ScoreConfig), menuName = SOMenuPath.Configs + nameof(ScoreConfig))]
public class ScoreConfig : ScriptableObject
{
    public int scoreForPerfect;
    public int scoreForGood;
}
