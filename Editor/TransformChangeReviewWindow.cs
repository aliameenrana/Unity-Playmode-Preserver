using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class TransformChangeReviewWindow : EditorWindow
{
    private List<TrackedGO> changedObjects;

    public static void ShowWindow(List<TrackedGO> allTracked)
    {
        var window = GetWindow<TransformChangeReviewWindow>("Transform Changes Detected");
        window.changedObjects = new List<TrackedGO>();

        foreach (var tracked in allTracked)
        {
            if (tracked.HasChanged())
                window.changedObjects.Add(tracked);
        }

        window.Show();
    }

    private void OnGUI()
    {
        if (changedObjects == null || changedObjects.Count == 0)
        {
            EditorGUILayout.LabelField("No transform changes detected.");
            return;
        }

        EditorGUILayout.LabelField($"Changes in {changedObjects.Count} object(s) detected:", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        foreach (var tracked in changedObjects)
        {
            GameObject go = EditorUtility.InstanceIDToObject(tracked.InstanceID) as GameObject;
            if (go == null) continue;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.ObjectField(go, typeof(GameObject), true);

            if (GUILayout.Button("Apply", GUILayout.Width(60)))
            {
                Undo.RecordObject(go.transform, "Apply Post-Play Transform");
                tracked.ApplyPostPlayData();
                EditorUtility.SetDirty(go.transform);
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}