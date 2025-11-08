using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RecordsMenu : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private RecordView _recordViewPrefab;

    [Inject] private RatingModel _ratingModel;

    private void OnEnable()
    {
        SetInfo(_ratingModel.Rating);
    }

    private void OnDisable()
    {
        Clear();
    }

    public void SetInfo(List<RatingRecord> ratingRecord)
    {
        foreach (var record in ratingRecord)
        {
            var recordView = Instantiate(_recordViewPrefab, _container);
            recordView.SetInfo(record.name, record.score);
        }
    }

    public void Clear()
    {
        for (int i = 0; i < _container.childCount; i++)
        {
            Destroy(_container.GetChild(i));
        }
    }
}
