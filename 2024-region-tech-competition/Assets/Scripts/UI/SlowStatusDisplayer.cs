using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowStatusDisplayer : MonoBehaviour
{
    [SerializeField] private Vector2 display;
    [SerializeField] private Vector2 hide;

    private RectTransform rectTransform => transform as RectTransform;

    private void Update()
    {
        rectTransform.anchoredPosition =
            Vector2.Lerp(rectTransform.anchoredPosition, Player.Instance.IsSlowing ? display : hide, Time.deltaTime * 10f);
    }
}
