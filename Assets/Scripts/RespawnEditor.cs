using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

[CustomEditor(typeof(RespawnManager))]
public class RespawnEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();


        Show(serializedObject.FindProperty("checkpoints"));

        RespawnManager myScript = (RespawnManager)target;
        if (GUILayout.Button("Add Checkpoint"))
        {
            myScript.AddCheckpoint();
        }
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
                EditorGUILayout.PropertyField(serializedObject.FindProperty("resettableObjects"));
            }
        }
    }
}