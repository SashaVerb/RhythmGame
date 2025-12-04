using UnityEngine;

public class LongNote : Note
{
    [SerializeField] Transform start;
    [SerializeField] Transform finish;
    [SerializeField] LongNoteView view;

    private bool isActivated = false;
    private bool isLocked = false;
    public Vector3 Start => start.position;
    public Vector3 End => finish.position;
    public bool IsActivated => isActivated;

    private float height;
    public float Height
    {
        get => height;
        set
        {
            height = value;
            view.SetHeight(value);
            finish.position = Start + new Vector3(0f, value, 0f);
        }
    }

    public override Bounds bounds => view.bounds;

    public override bool TryPress(Vector3 hitLinePosition)
    {
        if(isActivated || isLocked) 
            return false;

        var distance = (Start - hitLinePosition).magnitude;
        if (PointIsInsideNote(hitLinePosition) || config.IsInHitDistance(distance))
        {
            isActivated = true;
            view.StartActivateEffect(hitLinePosition);

            OnHit?.Invoke(config.HitTypeByDistance(distance));
        }
        else
        {
            OnMiss?.Invoke();
        }

        return false;
    }

    public override bool TryHold(Vector3 hitLinePosition)
    {
        if(!isActivated) 
            return false;

        view.UpdateMaskPosition(hitLinePosition);
        if (PointPassedNote(hitLinePosition))
        {
            view.StopEffect();
            Destroy(gameObject);

            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool TryRelease(Vector3 hitLinePosition)
    {
        if (!isActivated)
            return false;

        isActivated = false;
        isLocked = true;

        view.StopEffect();

        //Destroy(gameObject);

        return PointPassedNote(hitLinePosition);
    }

    private bool PointPassedNote(Vector3 point)
    {
        var directionFromEnd = point - End;
        var noteDirection = Start - End;

        return Vector3.Dot(directionFromEnd, noteDirection) < 0;
    }

    private bool PointIsInsideNote(Vector3 point)
    {
        var directionFromEnd = point - End;
        var directionFromStart = point - Start;

        return Vector3.Dot(directionFromEnd, directionFromStart) < 0; ;
    }
}
