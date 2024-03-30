using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverDisplayer : MonoBehaviour
{
    private Fade fade => Fade.Instance;
    private GameManager gameManager => GameManager.Instance;

    [SerializeField] private Image bg;
    [SerializeField] private RectTransform title;
    [SerializeField] private Button giveup;
    [SerializeField] private Button restart;

    private void Start()
    {
        giveup.onClick.AddListener(() => fade.LoadSceneFade("Title"));
        restart.onClick.AddListener(() => fade.LoadSceneFade($"Stage{gameManager.stage + 1}"));
    }

    public void Display()
    {
        bg.gameObject.SetActive(true);
        bg.color = default;
        bg.DOColor(new Color(0, 0, 0, 0.5f), 0.5f, updateType: UpdateType.Update);
        title.DOAnchorMove(Vector2.zero, 2f, Ease.OutBack, UpdateType.Update);
        (giveup.transform as RectTransform).DOAnchorMoveY(0, 2f, Ease.OutQuad, UpdateType.Update);
        (restart.transform as RectTransform).DOAnchorMoveY(0, 2f, Ease.OutQuad, UpdateType.Update);
    }
}