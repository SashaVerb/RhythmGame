using System;
using UnityEngine;

public abstract class Note : MonoBehaviour, I2DSize
{
    [SerializeField] protected Binding[] bindings;
    [SerializeField] protected SpriteRenderer arrowSprite;
    [SerializeField] protected TrackConfig config;

    public Action<HitType> OnHit;
    public Action OnMiss;

    private Directions direction;
    public Directions Direction { 

        get => direction;

        set
        {
            direction = value;

            foreach (var binding in bindings)
            {
                if (binding.type == value)
                {
                    arrowSprite.sprite = binding.sprite;
                    return;
                }
            }

            arrowSprite.sprite = bindings[0].sprite;
        }
    }

    public abstract Vector2 Size { get; }

    public abstract bool TryPress(Vector3 hitLinePosition);
    public virtual bool TryHold(Vector3 hitLinePosition)
    {
        return false;
    }
    public virtual bool TryRelease(Vector3 hitLinePosition)
    {
        return false;
    }

    public enum Directions
    {
        Up,
        Down,
        Right,
        Left
    }

    [System.Serializable]
    protected class Binding
    {
        public Sprite sprite;
        public Directions type;
    }
}
