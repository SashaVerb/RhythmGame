public class ScoreController
{
    ScoreConfig config;
    ScoreView view;

    private int score;
    public int Score
    {
        get => score;
        set
        {
            score = value;
            view.SetScore(score);
        }
    }

    public ScoreController(ScoreConfig config, ScoreView view)
    {
        this.config = config;
        this.view = view;
    }

    public void CalculateHit(HitType hitType)
    {
        switch (hitType)
        {
            case HitType.Perfect:
                Score += config.scoreForPerfect;
                break;
            case HitType.Good:
                Score += config.scoreForGood;
                break;
        }
    }
}
