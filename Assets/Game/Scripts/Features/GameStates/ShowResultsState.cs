using StateManaging;
using UnityEngine.SceneManagement;

public class ShowResultsState : IState
{
    private readonly RatingAddController _ratingController;
    private readonly ScoreController _scoreController;
    private readonly ComboController _comboController;
    private readonly AccuracyController _accuracyController;

    public ShowResultsState(RatingAddController ratingController, ScoreController scoreController, ComboController comboController, AccuracyController accuracyController)
    {
        _ratingController = ratingController;
        _scoreController = scoreController;
        _comboController = comboController;
        _accuracyController = accuracyController;
    }

    public void Enter()
    {
        RatingRecord record = new(string.Empty, string.Empty, _scoreController.Score);
        _ratingController.Show(record, _comboController.MaxPerfectCombo, _accuracyController.Accuracy);

        _ratingController.OnAddRecord += () =>
        {
            SceneManager.LoadScene("Menu");
        };
    }

    public void Exit()
    {

    }

    public void Tick()
    {

    }
}
