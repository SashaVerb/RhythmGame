using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class SongPlayer : MonoBehaviour, IPausable
{
    [SerializeField] private float visibleInterval;
    [SerializeField] private AudioSource audioSource;

    public bool IsPlaying { get; private set; }

    private SongInfo song;
    private TrackManager tracks;
    private NoteFactory noteFactory;

    private float currentInterval = 0f, neededInterval = 0f;

    [Inject]
    private void Construct(TrackManager tracks, NoteFactory noteFactory)
    {
        this.tracks = tracks;
        this.noteFactory = noteFactory;
    }

    public void SetSong(SongInfo song)
    {
        this.song = song;
        tracks.Speed = song.speed;
        neededInterval = visibleInterval / song.speed;
    }

    public void PlaySong()
    {
        IsPlaying = true;
        PlayAudio((song.start / song.bpm) * 60f).Forget();
        PlaySongAsync().Forget();
    }

    private async UniTask PlaySongAsync()
    {
        foreach (var note in song.notes)
        {
            var second = (note.bit / song.bpm) * 60f;
            while (currentInterval > neededInterval + second)
            {
                currentInterval -= Time.deltaTime;
                await UniTask.Yield();
            }

            var createdNote = noteFactory.Create(note.type, note.direction);

            if(note.type == NoteFactory.Type.Long)
            {
                var longNote = createdNote as LongNote;
                longNote.Height = (note.duration / song.bpm) * 60f * song.speed;
            }

            tracks.AddNote(createdNote, currentInterval + second);
            currentInterval += second;
        }

        IsPlaying = false;
    }

    private async UniTask PlayAudio(float delay)
    {
        await UniTask.WaitForSeconds(delay);
        audioSource.PlayOneShot(song.audioClip);
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    public void Resume()
    {
        audioSource.UnPause();
    }
}
