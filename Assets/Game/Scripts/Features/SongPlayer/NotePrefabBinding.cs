using UnityEngine;

[CreateAssetMenu(fileName = nameof(NotePrefabBinding), menuName = SOMenuPath.StartFolder + nameof(NotePrefabBinding))]
public class NotePrefabBinding : ScriptableObject
{
    public Binding[] info;
    
    [System.Serializable]
    public class Binding
    {
        public Note prefab;
        public NoteFactory.Type noteType;
    }
}
