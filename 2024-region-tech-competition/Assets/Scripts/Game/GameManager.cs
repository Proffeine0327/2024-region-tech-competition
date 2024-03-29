using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance ??= FindAnyObjectByType<GameManager>();

    public int stage;
    public GameStartWaitCounter startWaitCounter;
    public GameClearDisplayer clearDisplayer;
    public GameOverDisplayer overDisplayer;
    public Transform endPoint;

    private BaseFlighter endFlighter;

    [NonSerialized] public bool isGameRunning;
    [NonSerialized] public float playTime;

    public void GameEnd(BaseFlighter end)
    {
        if (endFlighter != null) return;

        endFlighter = end;
        isGameRunning = false;
    }

    private void Start()
    {
        StartCoroutine(GameRoutine());
    }

    private void Update()
    {
        if (isGameRunning)
            playTime += Time.deltaTime;
    }

    private IEnumerator GameRoutine()
    {
        yield return new WaitForSeconds(3f);
        startWaitCounter.Display(3);
        yield return new WaitForSeconds(1f);
        startWaitCounter.Display(2);
        yield return new WaitForSeconds(1f);
        startWaitCounter.Display(1);        
        yield return new WaitForSeconds(1f);
        startWaitCounter.Display(0);                
        isGameRunning = true;

        yield return new WaitUntil(() => !isGameRunning);
        yield return new WaitForSeconds(3f);
        if (endFlighter is Player) clearDisplayer.Display();
        if (endFlighter is Enemy) overDisplayer.Display();
    }
}