using UnityEngine;

[CreateAssetMenu(fileName = nameof(TrackConfig), menuName = SOMenuPath.StartFolder + nameof(TrackConfig))]
public class TrackConfig : ScriptableObject
{
    [SerializeField] private float perfectDistance;
    [SerializeField] private float goodDistance;
    [SerializeField] private float disappearOffset;

    public float HitDistance => goodDistance;
    public float DisappearOffset => disappearOffset;

    public HitType HitTypeByDistance(float distance)
    {
        if (Mathf.Abs(distance) <= perfectDistance)
            return HitType.Perfect;
        else
            return HitType.Good;
    }
    
    public bool IsInHitDistance(float distance)
    {
        return Mathf.Abs(distance) <= goodDistance;
    }
}
