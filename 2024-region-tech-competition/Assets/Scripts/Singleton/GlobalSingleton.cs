using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSingleton<T> : Singleton<T> where T : MonoBehaviour
{
    protected virtual void Awake()
    {
        if (Instance == null || ReferenceEquals(Instance, this)) DontDestroyOnLoad(gameObject);
        else Destroy(gameObject);
    }
}
