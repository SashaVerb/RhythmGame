
public class AccuracyController
{
    private float hits, misses;
    public float Accuracy => hits / (hits + misses);

    public void CalculateHit(HitType hitType)
    {
        hits += 1f;
    }

    public void CalculateMiss()
    {
        misses += 1f;
    }
}
