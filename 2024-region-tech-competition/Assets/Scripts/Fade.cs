using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Fade : MonoBehaviour
{
    public class Settings
    {
        public Action onSceneLoaded { get; private set; }
        
        public void OnSceneLoaded(Action action)
        {
            onSceneLoaded = action;
        }
    }

    public static Fade Instance { get; private set; }

    public RectTransform mask;

    [NonSerialized] public bool isFading;

    public Settings LoadSceneFade(string sceneName)
    {
        var settings = new Settings();
        StartCoroutine(FadeRoutine(sceneName, settings));
        return settings;
    }

    private IEnumerator FadeRoutine(string sceneName, Settings settings)
    {
        mask.gameObject.SetActive(true);
        mask.DOSizeDelta(Vector2.zero, 1f, Ease.OutQuad);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
        settings.onSceneLoaded?.Invoke();
        mask.DOSizeDelta(Vector2.one * 2500, 1f, Ease.InQuad);
        yield return new WaitForSeconds(1f);
        mask.gameObject.SetActive(false);
    }

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
