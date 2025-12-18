using DG.Tweening;
using UnityEngine;

public class PingPongScaleEffect : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _targetScale;

    private Tween tween;
    private Vector3 targetScale, defaultScale;
    private float comeBackDuration;

    private void Awake()
    {
        targetScale = Vector3.one * _targetScale;
        defaultScale = transform.localScale;

        comeBackDuration = Mathf.Abs(_targetScale - transform.localScale.x) / _speed;
    }

    public void Play()
    {
        KillTweenIfPlaying();

        float duration = Mathf.Abs(_targetScale - transform.localScale.x) / _speed;
        tween = DOTween.Sequence()
            .Append(transform.DOScale(targetScale, duration))
            .Append(transform.DOScale(defaultScale, comeBackDuration));
    }

    private void KillTweenIfPlaying()
    {
        if(tween != null && tween.IsActive())
        {
            tween.Kill();
        }
    }

    private void OnDestroy()
    {
        KillTweenIfPlaying();
    }
}
