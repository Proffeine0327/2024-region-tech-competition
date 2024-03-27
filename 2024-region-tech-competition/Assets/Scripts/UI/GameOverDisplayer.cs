using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverDisplayer : MonoBehaviour
{
    [SerializeField] private Image bg;
    [SerializeField] private RectTransform title;

    public void Display()
    {
        bg.gameObject.SetActive(true);
        bg.color = default;
        bg.DOColor(new Color(0, 0, 0, 0.5f), 0.5f);
        title.DOAnchorMove(Vector2.zero, 2f, Ease.OutBack);
    }
}