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
        store.onClick.AddListener(() => StartCoroutine(SceneChangeRoutine(() => fade.LoadSceneFade("RepairStore"))));
    }

    private IEnumerator SceneChangeRoutine(System.Action onComplete = null)
    {
        title.SetActive(false);
        play.gameObject.SetActive(false);
        store.gameObject.SetActive(false);

        mover.Disappeare(3f);

        var camera = Camera.main;
        for (float t = 0; t < 3; t += Time.deltaTime)
        {
            var diff = mover.transform.position - camera.transform.position;
            var rot = Quaternion.LookRotation(diff);
            camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, rot, Time.deltaTime * 10);
            yield return null;
        }
        onComplete?.Invoke();
    }
}
