using UnityEngine;

[System.Serializable]
public class TransformData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public TransformData(Transform t)
    {
        position = t.localPosition;
        rotation = t.localRotation;
        scale = t.localScale;
    }

    public void ApplyTo(Transform t)
    {
        t.localPosition = position;
        t.localRotation = rotation;
        t.localScale = scale;
    }

    public bool HasChangedComparedTo(TransformData other)
    {
        return position != other.position ||
            rotation != other.rotation ||
            scale != other.scale;
    }
}