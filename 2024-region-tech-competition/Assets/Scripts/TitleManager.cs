using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    private static TitleManager instance;
    public static TitleManager Instance => instance ??= FindObjectOfType<TitleManager>();

    private Fade fade => Fade.Instance;

    [Header("Ref")]
    public TitlePlayerModelMover mover;
    [Header("UI")]
    public GameObject title;
    public Button play;
    public Button store;

    private void Start()
    {
        store.onClick.AddListener(() => StartCoroutine(SceneChangeRoutine(() => fade.LoadSceneFade("RepairShop"))));
    }

    private IEnumerator SceneChangeRoutine(System.Action onComplete = null)
    {
        title.SetActive(false);
        play.gameObject.SetActive(false);
        store.gameObject.SetActive(false);

        mover.Disappeare(5f);
        for (float t = 0; t < 5; t += Time.deltaTime)
        {
            Camera.main.transform.LookAt(mover.transform);
            yield return null;
        }
        onComplete?.Invoke();
    }
}
