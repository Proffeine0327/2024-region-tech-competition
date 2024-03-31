using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int stage;
    public GameStartWaitCounter startWaitCounter;
    public GameClearDisplayer clearDisplayer;
    public GameOverDisplayer overDisplayer;
    public Transform endPoint;

    private BaseFlighter endFlighter;

    [NonSerialized] public int gainMoney;
    [NonSerialized] public bool isGameRunning;
    [NonSerialized] public float playTime;
    [NonSerialized] public AudioSource audioSource;

    public void GameEnd(BaseFlighter end)
    {
        if (endFlighter != null) return;

        endFlighter = end;
        isGameRunning = false;
    }

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();
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
        audioSource.Play();
        yield return new WaitForSeconds(1f);
        startWaitCounter.Display(2);
        audioSource.Play();
        yield return new WaitForSeconds(1f);
        startWaitCounter.Display(1);
        audioSource.Play();
        yield return new WaitForSeconds(1f);
        startWaitCounter.Display(0);
        audioSource.pitch = 2f;
        audioSource.Play();
        isGameRunning = true;

        yield return new WaitUntil(() => !isGameRunning);
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0;
        if (endFlighter is Player) clearDisplayer.Display();
        if (endFlighter is Enemy) overDisplayer.Display();
    }
}