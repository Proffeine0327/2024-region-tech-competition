using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtractMoneyDisplayer : MonoBehaviour
{
    public static SubtractMoneyDisplayer Instance { get; private set; }

    public float dist;

    private Vector2 startpos;
    private TextMeshProUGUI text;
    private List<Coroutine> routines = new();

    private RectTransform rectTransform => transform as RectTransform;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        startpos = rectTransform.anchoredPosition - Vector2.up * dist;
        text = GetComponent<TextMeshProUGUI>();
    }

    public void Display(int money)
    {
        StartCoroutine(DisplayRoutine(money));
    }

    private IEnumerator DisplayRoutine(int money)
    {
        routines.ForEach(r => StopCoroutine(r));
        routines.Clear();
        text.enabled = true;
        text.text = $"-{money:#,##0}$";
        rectTransform.anchoredPosition = startpos;
        routines.Add(rectTransform.DOAnchorMove(startpos + Vector2.up * dist, 0.5f, Ease.OutQuad));
        yield return new WaitForSeconds(2f);
        routines.Add(rectTransform.DOAnchorMove(startpos - Vector2.up * dist, 0.5f, Ease.OutQuad));
        yield return new WaitForSeconds(0.25f);
        text.enabled = false;
    }
}
