using UnityEngine;

public static class TransformExtension
{
    public static void ClearChildren(this Transform transform)
    {
        for(int i = transform.childCount - 1; i >= 0; --i)
        {
            Object.DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
