using StateManaging;

public class IfSongEndCondition : ICondition
{
    private SongPlayer _songPlayer;
    private TrackManager _trackManager;

    public IfSongEndCondition(SongPlayer songPlayer, TrackManager trackManager)
    {
        _songPlayer = songPlayer;
        _trackManager = trackManager;
    }

    public bool Check()
    {
        return !_songPlayer.IsPlaying && !_trackManager.ContainsNotes;
    }
}
