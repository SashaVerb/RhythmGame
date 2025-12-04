using UnityEngine;

public class BasicNote : Note
{
    public override Bounds bounds => arrowSprite.bounds;

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
