using StateManaging;
using UnityEngine;

public class TimeElapsedCondition : ICondition
{
    private float _duration;
    private float _timer;

    public TimeElapsedCondition(float duration)
    {
        _duration = duration;
        _timer = duration;
    }

    public bool Check()
    {
        _timer -= Time.deltaTime;

        if (_timer < 0f)
        {
            _timer = _duration;
            return true;
        }
        else
        {
            return false;
        }
    }
}
