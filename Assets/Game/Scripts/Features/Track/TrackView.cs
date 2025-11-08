using UnityEngine;
using Zenject;

public class TrackView : MonoBehaviour
{
    [SerializeField] private Transform _textEffects;

    private TextEffect.Factory _textEffectFactory;
    private TextEffect _textEffect;
    [Inject]
    private void Construct(TextEffect.Factory textEffectFactory)
    {
        _textEffectFactory = textEffectFactory;
    }

    public void OnHit(HitType hitType)
    {
        ShowTextEffect(hitType.ToString());
    }

    public void OnMiss()
    {
        ShowTextEffect("Miss");
    }

    private void ShowTextEffect(string text)
    {
        if(_textEffect != null && _textEffect.gameObject != null)
            Destroy(_textEffect.gameObject);

        _textEffect = _textEffectFactory.Create(_textEffects.position, text);
    }
}
