#pragma warning disable RCS1110
using UnityEngine;

public static class UnityEngine__GameObjectExtensions
{
    public static bool HasInterface<T>(this GameObject gameObject) where T : class
    {
        return gameObject.GetComponent(typeof(T)) != null;
    }
}
