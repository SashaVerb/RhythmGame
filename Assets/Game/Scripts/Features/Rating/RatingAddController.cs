using System;

public class RatingAddController
{
    public event Action OnAddRecord;

    private readonly RatingAddView _view;
    private readonly RatingModel _ratingModel;

    private RatingRecord _infoToAdd;

    public RatingAddController(RatingAddView ratingAddView, RatingModel ratingModel)
    {
        _view = ratingAddView;
        _ratingModel = ratingModel;

        ratingAddView.OnPlayerSendName += OnPlayerAddRecord;
    }

    public void Show(RatingRecord ratingInfo, int combo, float accuracy)
    {
        _infoToAdd = ratingInfo;
        _view.Show();
        _view.SetScore(ratingInfo.score);
        _view.SetCombo(combo);
        _view.SetAccuracy(accuracy);
    }

    private void OnPlayerAddRecord(string name)
    {
        _infoToAdd.name = name;

        _ratingModel.Add(_infoToAdd);
        _view.DisableInput();

        OnAddRecord?.Invoke();
    }
}
