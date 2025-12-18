using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RatingModel : IDisposable
{
    public static readonly string PATH = Path.Combine(Application.persistentDataPath, "Rating.json");

    private readonly List<RatingRecord> _rating;

    public List<RatingRecord> Rating => _rating;

    public RatingModel()
    {
        if (!File.Exists(PATH))
        {
            var stream = File.CreateText(PATH);
            stream.Close();
        }

        string info = File.ReadAllText(PATH);
        _rating = JsonConvert.DeserializeObject<List<RatingRecord>>(info);

        if (_rating != null)
        {
            _rating.Sort((RatingRecord a, RatingRecord b) => b.score - a.score);
        }
        else
        {
            _rating = new();
        }
    }

    public void Add(RatingRecord info)
    {
        int i = 0;
        for (; i < _rating.Count; i++)
        {
            if (_rating[i].score < info.score)
            {
                _rating.Insert(i, info);
                break;
            }
        }

        if (i == _rating.Count)
        {
            _rating.Add(info);
        }
    }

    public int GetPossiblePosition(int score)
    {
        for (int i = 0; i < _rating.Count; i++)
        {
            if (_rating[i].score < score)
            {
                return i + 1;
            }
        }

        return _rating.Count + 1;
    }

    public void Save()
    {
        File.WriteAllText(PATH, JsonConvert.SerializeObject(_rating));
    }

    public void Dispose()
    {
        Save();
    }
}
