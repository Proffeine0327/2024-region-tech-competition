using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Transform endPoint;

    public bool IsGameRunning { get; private set; }
    public Observer<int> WaitCount { get; private set; } = new() { Value = -1 };
    public Transform EndPoint => endPoint;
    public float PlayTime { get; private set; }

    private void Start()
    {
        StartCoroutine(GameRoutine());
    }

    private void Update()
    {
        if (IsGameRunning)
            PlayTime += Time.deltaTime;
    }

    private IEnumerator GameRoutine()
    {
        yield return new WaitForSeconds(3f);
        WaitCount.Value = 3;
        yield return new WaitForSeconds(1f);
        WaitCount.Value = 2;
        yield return new WaitForSeconds(1f);
        WaitCount.Value = 1;
        yield return new WaitForSeconds(1f);
        WaitCount.Value = 0;

        IsGameRunning = true;
    }
}
