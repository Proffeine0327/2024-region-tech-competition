using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    private static TitleManager instance;
    public static TitleManager Instance { get; private set; }

    private Fade fade => Fade.Instance;

    [Header("Ref")]
    public TitlePlayerModelMover mover;
    [Header("UI")]
    public GameObject title;
    public List<Button> buttons;
    public How2PlayUI how2playui;

    private List<Vector2> startPositions = new();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        buttons[0].onClick.AddListener(() => StartCoroutine(SceneChangeRoutine(() => fade.LoadSceneFade("Stage1")))); //play
        buttons[1].onClick.AddListener(() => fade.LoadSceneFade("RepairStore")); //repair
        buttons[2].onClick.AddListener(() => how2playui.Display()); //how2play
        buttons[3].onClick.AddListener(() => fade.LoadSceneFade("Ranking")); //ranking
        buttons[4].onClick.AddListener(() => Application.Quit()); //exit

        buttons.ForEach(button => startPositions.Add((button.transform as RectTransform).anchoredPosition));
    }

    private void Update()
    {
        buttons.For((b, index) =>
        {
            var rectTransform = b.transform as RectTransform;
            var contains = RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition);
            rectTransform.anchoredPosition =
                Vector2.Lerp(rectTransform.anchoredPosition, contains ? startPositions[index] - new Vector2(50, 0) : startPositions[index], Time.deltaTime * 10f);
        });
    }

    private IEnumerator SceneChangeRoutine(System.Action onComplete = null)
    {
        title.SetActive(false);
        buttons.ForEach(b => b.gameObject.SetActive(false));

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
