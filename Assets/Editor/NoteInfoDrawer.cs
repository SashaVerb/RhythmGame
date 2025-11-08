using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SongInfo.NoteInfo))]
public class NoteInfoDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var bitProp = property.FindPropertyRelative("bit");
        var directionProp = property.FindPropertyRelative("direction");
        var typeProp = property.FindPropertyRelative("type");
        var durationProp = property.FindPropertyRelative("duration");

        EditorGUI.BeginProperty(position, label, property);

        var foldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label, true);

        if (property.isExpanded)
        {
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel++;

            var y = position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            EditorGUI.PropertyField(new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight), bitProp);
            y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            EditorGUI.PropertyField(new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight), directionProp);
            y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            EditorGUI.PropertyField(new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight), typeProp);
            y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            if (typeProp.enumValueIndex == (int)NoteFactory.Type.Long)
            {
                EditorGUI.PropertyField(new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight), durationProp);
                y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }

            EditorGUI.indentLevel = indent;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var typeProp = property.FindPropertyRelative("type");
        var expanded = property.isExpanded;

        if (!expanded)
            return EditorGUIUtility.singleLineHeight;

        int fieldCount = 3;
        if (typeProp.enumValueIndex == (int)NoteFactory.Type.Long)
            fieldCount++;

        return EditorGUIUtility.singleLineHeight * fieldCount
               + EditorGUIUtility.standardVerticalSpacing * fieldCount
               + EditorGUIUtility.singleLineHeight;
    }
}