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

[Serializable]
public class PlayerData
{
    public int unlockMoney;
    public float acceleration;
    public float maxSpeed;
    public float steerSpeed;
    public float driftSteerSpeed;
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public PlayerData[] playerDatas;

    [NonSerialized] public int money = 10000;
    [NonSerialized] public List<int> unlockPlayer = new() { 0 };
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