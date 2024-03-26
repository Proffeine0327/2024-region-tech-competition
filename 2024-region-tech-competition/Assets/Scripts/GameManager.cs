using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance ??= FindAnyObjectByType<GameManager>();

    [SerializeField] private Transform endPoint;

    private BaseFlighter endFlighter;

    public bool IsGameRunning { get; private set; }
    public bool IsGameOver { get; private set; }
    public bool IsGameClear {  get; private set; }
    public Observer<int> WaitCount { get; private set; } = new() { Value = -1 };
    public Transform EndPoint => endPoint;
    public float PlayTime { get; private set; }

    public void GameEnd(BaseFlighter end)
    {
        if (endFlighter != null) return;

        endFlighter = end;
        IsGameRunning = false;
    }

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
        yield return new WaitUntil(() => !IsGameRunning);
        yield return new WaitForSeconds(3f);
        if(endFlighter is Player) IsGameClear = true;
        if (endFlighter is Enemy) IsGameOver = true;
    }
}