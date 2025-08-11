using UnityEngine;
using UnityEditor;

[System.Serializable]
public class TrackedGO
{
    public int InstanceID;
    public TransformData prePlayTransformData;
    public TransformData postPlayTransformData;

    public TrackedGO() { }

    public TrackedGO(GameObject go)
    {
        InstanceID = go.GetInstanceID();
        prePlayTransformData = new TransformData(go.transform);
    }

    public void RecordPostPlayData(Transform transform)
    {
        postPlayTransformData = new TransformData(transform);
    }

    public bool HasChanged()
    {
        if (prePlayTransformData == null || postPlayTransformData == null)
            return false;

        return prePlayTransformData.HasChangedComparedTo(postPlayTransformData);
    }

    public void ApplyPostPlayData()
    {
        GameObject go = EditorUtility.InstanceIDToObject(InstanceID) as GameObject;
        if (go != null && postPlayTransformData != null)
        {
            postPlayTransformData.ApplyTo(go.transform);
        }
    }
}