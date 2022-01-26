#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RespawnManager))]
public class RespawnEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Show(serializedObject.FindProperty("checkpoints"));
    }

    public static void Show(SerializedProperty list)
    {
        EditorGUILayout.PropertyField(list);
        for (int i = 0; i < list.arraySize; i++)
        {
            var checkpoint = list.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(checkpoint);
            if (checkpoint.objectReferenceValue != null)
            {
                SerializedObject serializedObject = new UnityEditor.SerializedObject(checkpoint.objectReferenceValue);
                GUILayout.BeginHorizontal();
                GUILayout.Space(EditorGUI.indentLevel + 50);
                GUILayout.BeginVertical();

                EditorGUILayout.PropertyField(serializedObject.FindProperty("resettableObjects"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("neurons"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("endWall"));

                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                GUILayout.Space(EditorGUI.indentLevel + 50);

            }
        }
    }
}
#endif