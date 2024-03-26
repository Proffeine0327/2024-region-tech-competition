using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverDisplayer : MonoBehaviour
{
    [SerializeField] private Image bg;
    [SerializeField] private RectTransform title;

    private void Start()
    {
        GameManager.Instance.ObserveEveryValueChanged(g => g.IsGameOver)
            .Subscribe(x =>
            {
                if (!x) return;
                bg.gameObject.SetActive(true);
                bg.color = default;
                bg.DOColor(new Color(0, 0, 0, 0.5f), 0.5f);
                title.DOAnchorMove(Vector2.zero, 2f, Ease.OutBack);
            });
    }
}
