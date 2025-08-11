using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[InitializeOnLoad]
public static class PlayModeTransformPreserver
{
    private const string ResourcePath = "TrackedTransformDatabase";
    private const string AssetPath = "Assets/Resources/TrackedTransformDatabase.asset";

    private static TrackedGameObjectsDatabase _instance;
    public static TrackedGameObjectsDatabase DBInstance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<TrackedGameObjectsDatabase>(ResourcePath);

#if UNITY_EDITOR
                // Auto-create if missing
                if (_instance == null)
                {
                    Debug.LogWarning("[Preserver] TrackedTransformDatabase not found. Creating a new one at Assets/Resources.");
                    _instance = ScriptableObject.CreateInstance<TrackedGameObjectsDatabase>();

                    // Ensure Resources folder exists
                    if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                        AssetDatabase.CreateFolder("Assets", "Resources");

                    AssetDatabase.CreateAsset(_instance, AssetPath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
#endif
            }

            return _instance;
        }
    }

    static PlayModeTransformPreserver()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        switch (state)
        {
            case PlayModeStateChange.ExitingEditMode:
                SaveSelectedGameObjectTransforms();
                break;

            case PlayModeStateChange.ExitingPlayMode:
                SaveTransformChanges();
                CheckTransformChanges();
                break;
        }
    }

    private static void SaveSelectedGameObjectTransforms()
    {
        DBInstance.trackedObjects.Clear();

        GameObject[] selectedGOs = Selection.gameObjects;

        int numGOs = selectedGOs.Length;
        for (int i = 0; i < numGOs; i++)
        {
            TrackedGO trackedGO = new TrackedGO(selectedGOs[i]);
            DBInstance.trackedObjects.Add(trackedGO);
        }
    }

    private static void SaveTransformChanges()
    {
        int numGOs = DBInstance.trackedObjects.Count;

        for (int i = 0; i < numGOs; i++)
        {
            TrackedGO trackedGO = DBInstance.trackedObjects[i];
            GameObject go = EditorUtility.InstanceIDToObject(trackedGO.InstanceID) as GameObject;
            trackedGO.postPlayTransformData = new TransformData(go.transform);
        }
    }

    private static void CheckTransformChanges()
    {
        TransformChangeReviewWindow.ShowWindow(DBInstance.trackedObjects);
    }
}