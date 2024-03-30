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

[Serializable]
public class EnemyData
{
    public float acceleration;
    public float maxSpeed;
}

[Serializable]
public class RankingData
{
    public float time;
    public PlayerModel model;
    public Material color;
    public DateTime datetime;
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public PlayerData[] playerDatas;
    public EnemyData[] enemyDatas;

    [NonSerialized] public int money = 10000;
    [NonSerialized] public List<int> unlockPlayer = new() { 0 };
    [NonSerialized] public int playerSelect;
    [NonSerialized] public int playerColorSelect;
    [NonSerialized] public bool desertWing;
    [NonSerialized] public bool mountainWing;
    [NonSerialized] public bool cityWing;
    [NonSerialized] public EngineType engine;
    [NonSerialized] public List<RankingData> ranking = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        ranking = new()
        {
            new RankingData
            {
                color = ResourceContainer.Instance.playerColors[1],
                datetime = DateTime.Now,
                model = ResourceContainer.Instance.playerModels[1],
                time = 20.1f
            },
            new RankingData
            {
                color = ResourceContainer.Instance.playerColors[0],
                datetime = DateTime.Now,
                model = ResourceContainer.Instance.playerModels[0],
                time = 10.1f
            },
            new RankingData
            {
                color = ResourceContainer.Instance.playerColors[2],
                datetime = DateTime.Now,
                model = ResourceContainer.Instance.playerModels[2],
                time = 30.1f
            }
        };
    }
}