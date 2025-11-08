using UnityEngine;

[CreateAssetMenu(fileName = nameof(SongInfo), menuName = SOMenuPath.StartFolder + nameof(SongInfo))]
public class SongInfo : ScriptableObject
{
    public AudioClip audioClip;
    public float speed;
    public float bpm;
    public float start;
    public NoteInfo[] notes;

    [System.Serializable]
    public class NoteInfo
    {
        public float bit;
        public Note.Directions direction;
        public NoteFactory.Type type;
        public float duration;
    }
}
