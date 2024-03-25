using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static List<T> GetComponentsInChildren<T>(this MonoBehaviour mb, string name = null) where T : Component
    {
        var list = new List<T>();
        foreach (var t in mb.GetComponentsInChildren<Transform>(true))
        {
            Debug.Log(t.name);
            if (!string.IsNullOrEmpty(name) && t.name != name)
                continue;
                
            if(t.TryGetComponent<T>(out var comp))
                list.Add(comp);
        }
        return list;
    }

    public static void For<T>(this List<T> list, Action<T, int> action)
    {
        for(int i = 0; i < list.Count; i++)
            action?.Invoke(list[i], i);
    }

    #region Vector3
    public static Vector3 SetX(this Vector3 vector, float x)
    {
        vector.x = x;
        return vector;
    }
    public static Vector3 SetY(this Vector3 vector, float y)
    {
        vector.y = y;
        return vector;
    }
    public static Vector3 SetZ(this Vector3 vector, float z)
    {
        vector.z = z;
        return vector;
    }
    #endregion

    #region Vector2 
    public static Vector2 SetX(this Vector2 vector, float x)
    {
        vector.x = x;
        return vector;
    }
    public static Vector2 SetY(this Vector2 vector, float y)
    {
        vector.y = y;
        return vector;
    }
    #endregion

    #region Quaternion
    public static Quaternion SetX(this Quaternion quaternion, float x)
    {
        quaternion.x = x;
        return quaternion;
    }
    public static Quaternion SetY(this Quaternion quaternion, float y)
    {
        quaternion.y = y;
        return quaternion;
    }
    public static Quaternion SetZ(this Quaternion quaternion, float z)
    {
        quaternion.z = z;
        return quaternion;
    }
    public static Quaternion SetW(this Quaternion quaternion, float w)
    {
        quaternion.w = w;
        return quaternion;
    }
    #endregion

    #region Color
    public static Color SetR(this Color color, float r)
    {
        color.r = r;
        return color;
    }
    public static Color SetG(this Color color, float g)
    {
        color.g = g;
        return color;
    }
    public static Color SetB(this Color color, float b)
    {
        color.b = b;
        return color;
    }
    public static Color SetA(this Color color, float a)
    {
        color.a = a;
        return color;
    }
    #endregion
}