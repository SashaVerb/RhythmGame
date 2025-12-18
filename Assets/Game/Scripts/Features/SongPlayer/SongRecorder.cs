using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SongRecorder : MonoBehaviour, IPausable
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float bpm;
    [SerializeField] private float longNoteThreshold = 0.25f;

    private bool isRecording;
    private float recordStartTime;

    private List<RecordedPress> presses = new();

    private class RecordedPress
    {
        public float startTime;
        public float endTime;
        public Note.Directions direction;
    }

    public void StartRecording()
    {
        presses.Clear();

        isRecording = true;
        recordStartTime = Time.time;

        audioSource.Play();
        RecordLoop().Forget();
    }

    public void StopRecording()
    {
        isRecording = false;
        audioSource.Stop();

        GenerateSongInfo();
    }

    private async UniTask RecordLoop()
    {
        Dictionary<KeyCode, RecordedPress> activeNotes = new();

        while (isRecording)
        {
            foreach (var pair in keyToDirection)
            {
                KeyCode key = pair.Key;

                if (Input.GetKeyDown(key))
                {
                    activeNotes[key] = new RecordedPress()
                    {
                        startTime = Time.time - recordStartTime,
                        direction = pair.Value
                    };
                }

                if (Input.GetKeyUp(key) && activeNotes.ContainsKey(key))
                {
                    var press = activeNotes[key];
                    press.endTime = Time.time - recordStartTime;
                    presses.Add(press);
                    activeNotes.Remove(key);
                }
            }

            await UniTask.Yield();
        }

        foreach (var press in activeNotes.Values)
        {
            press.endTime = Time.time - recordStartTime;
            presses.Add(press);
        }
    }

    public SongInfo GenerateSongInfo(string name = "RecordedSong")
    {
        var songInfo = ScriptableObject.CreateInstance<SongInfo>();
        songInfo.name = name;
        songInfo.audioClip = audioSource.clip;
        songInfo.bpm = bpm;
        songInfo.speed = 5f;
        songInfo.start = 0;

        var notes = new List<SongInfo.NoteInfo>();

        float lastNoteStartTime = 0f;
        foreach (var press in presses)
        {
            float bitStart = (press.startTime - lastNoteStartTime) / 60f * bpm;

            lastNoteStartTime = press.startTime;
            float duration = ((press.endTime - press.startTime) / 60f) * bpm;

            bitStart = MathF.Round(bitStart, 1);
            duration = MathF.Round(duration, 1);

            var note = new SongInfo.NoteInfo()
            {
                bit = bitStart,
                direction = press.direction,
                duration = duration,
                type = duration > longNoteThreshold ? NoteFactory.Type.Long : NoteFactory.Type.Basic
            };

            notes.Add(note);
        }

        songInfo.notes = notes.ToArray();

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.CreateAsset(
                songInfo,
                $"Assets/Game/Configs/Songs/Generated/GeneratedData_{name}.asset"
            );
            UnityEditor.AssetDatabase.SaveAssets();
            Debug.Log("ScriptableObject создан!");
#endif

        return songInfo;
    }

    public void Pause()
    {
        audioSource.Pause();
        isRecording = false;
    }

    public void Resume()
    {
        recordStartTime = Time.time - audioSource.time;
        isRecording = true;
        audioSource.UnPause();
        RecordLoop().Forget();
    }

    private readonly Dictionary<KeyCode, Note.Directions> keyToDirection = new()
    {
        { KeyCode.A, Note.Directions.Left },
        { KeyCode.D, Note.Directions.Right },
        { KeyCode.W, Note.Directions.Up },
        { KeyCode.S, Note.Directions.Down }
    };
}
