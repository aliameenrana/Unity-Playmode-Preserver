using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackedGameObjectsDatabase : ScriptableObject
{
    public List<TrackedGO> trackedObjects = new List<TrackedGO>();
}
