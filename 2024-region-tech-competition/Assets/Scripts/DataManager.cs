using System;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum EngineType
{
    None,
    Engine6,
    Engine8,
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    [NonSerialized] public int playerSelect;
    [NonSerialized] public int playerColorSelect;
    [NonSerialized] public bool desertWing;
    [NonSerialized] public bool mountainWing;
    [NonSerialized] public bool cityWing;
    [NonSerialized] public EngineType engine;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}