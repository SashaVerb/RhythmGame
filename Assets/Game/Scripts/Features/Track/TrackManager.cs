using System;

public class TrackManager : IPausable
{
    Track[] tracks;

    public Action<HitType> OnHit;
    public Action OnMiss;

    public bool ContainsNotes {
        get
        {
            foreach (var track in tracks)
            {
                if(track.HasNotes)
                    return true;
            }
            return false;
        }
    }

    private float speed;
    public float Speed
    {
        get => speed;
        set
        {
            speed = value;
            foreach (var track in tracks)
            {
                track.Speed = value;
            }
        }
    }

    public TrackManager(Track[] tracks)
    {
        this.tracks = tracks;
        foreach (var track in tracks)
        {
            track.OnHit += (hitType) => OnHit?.Invoke(hitType);
            track.OnMiss += () => OnMiss?.Invoke();
        }
    }

    public void AddNote(Note note, float delay)
    {
        foreach (var track in tracks)
        {
            if (track.NoteType == note.Direction)
            {
                track.AddNote(note, delay);
                break;
            }
        }
    }

    public void Pause()
    {
        foreach(var track in tracks)
        {
            track.Pause();
        }
    }

    public void Resume()
    {
        foreach (var track in tracks)
        {
            track.Resume();
        }
    }
}
