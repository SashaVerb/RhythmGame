using StateManaging;

public class RecordSongState : IState
{
    SongRecorder songRecorder;

    public RecordSongState(SongRecorder songRecorder)
    {
        this.songRecorder = songRecorder;
    }

    public void Enter()
    {
        songRecorder.StartRecording();
    }

    public void Exit()
    {
        songRecorder.StopRecording();
    }

    public void Tick()
    {

    }
}
