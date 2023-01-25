using Unity.VisualScripting;
using UnityEngine;
public static class Transforms
{
    public static void DestroyChildren(this Transform t, bool destroyImmediately = true)
    {
        foreach (Transform child in t)
        {
            if (destroyImmediately)
                MonoBehaviour.DestroyImmediate(child.gameObject);
            else
                MonoBehaviour.Destroy(child.gameObject);
        }
    }
}