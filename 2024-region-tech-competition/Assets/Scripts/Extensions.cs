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
}