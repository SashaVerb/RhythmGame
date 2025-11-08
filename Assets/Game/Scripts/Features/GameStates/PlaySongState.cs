using StateManaging;

public class PlaySongState : IState
{
    SongPlayer songPlayer;
    SongManager songManager;
    TrackManager trackManager;
    ComboController comboController;
    ScoreController scoreController;
    AccuracyController accuracyController;

    public PlaySongState(SongPlayer songPlayer, SongManager songManager, TrackManager trackManager, ComboController comboController, ScoreController scoreController, AccuracyController accuracyController)
    {
        this.songPlayer = songPlayer;
        this.songManager = songManager;
        this.trackManager = trackManager;
        this.comboController = comboController;
        this.scoreController = scoreController;
        this.accuracyController = accuracyController;
    }

    public void Enter()
    {
        if(!songPlayer.IsPlaying)
        {
            songPlayer.SetSong(songManager.CurrentSong);
            songPlayer.PlaySong();
        }

        trackManager.OnHit += comboController.CalculateHit;
        trackManager.OnMiss += comboController.RestartCombo;

        trackManager.OnHit += accuracyController.CalculateHit;
        trackManager.OnMiss += accuracyController.CalculateMiss;
        
        trackManager.OnHit += scoreController.CalculateHit;
    }

    public void Exit()
    {
        trackManager.OnHit -= comboController.CalculateHit;
        trackManager.OnMiss -= comboController.RestartCombo;

        trackManager.OnHit -= accuracyController.CalculateHit;
        trackManager.OnMiss -= accuracyController.CalculateMiss;

        trackManager.OnHit -= scoreController.CalculateHit;
    }

    public void Tick()
    {
        
    }
}
