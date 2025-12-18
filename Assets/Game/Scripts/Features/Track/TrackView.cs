using UnityEngine;
using Zenject;

public class TrackView : MonoBehaviour
{
    [SerializeField] private Transform _textEffects;
    [SerializeField] ParticleSystem particles;
    [SerializeField] ParticleSystem mistakeParticles;
    [SerializeField] PingPongScaleEffect pingPongScaleEffect;
    [SerializeField] int particleEmit = 10;

    private TextEffect.Factory _textEffectFactory;
    private TextEffect _textEffect;
    [Inject]
    private void Construct(TextEffect.Factory textEffectFactory)
    {
        _textEffectFactory = textEffectFactory;
    }

    public void OnHit(HitType hitType)
    {
        pingPongScaleEffect.Play();
        particles.Emit(particleEmit);

        if (hitType == HitType.Perfect)
        {
            ShowTextEffect("Perfect");
        }
    }

    public void OnMiss()
    {
        pingPongScaleEffect.Play();
        mistakeParticles.Emit(particleEmit);
    }

    private void ShowTextEffect(string text)
    {
        if(_textEffect != null && _textEffect.gameObject != null)
            Destroy(_textEffect.gameObject);

        _textEffect = _textEffectFactory.Create(_textEffects.position, text);
    }
}
