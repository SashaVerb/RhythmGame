using UnityEngine;

public class BasicNote : Note
{
    public override Vector2 Size => arrowSprite.bounds.size;

    public override bool TryPress(Vector3 hitLinePosition)
    {
        float distance = (transform.position - hitLinePosition).magnitude;

        if(config.IsInHitDistance(distance))
        {
            OnHit?.Invoke(config.HitTypeByDistance(distance));
            Destroy(gameObject);
            return true;
        }
        else
        {
            OnMiss?.Invoke();
            return false;
        }
    }
}
