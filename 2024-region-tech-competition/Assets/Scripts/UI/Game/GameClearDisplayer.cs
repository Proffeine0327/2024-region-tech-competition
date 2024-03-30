using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameClearDisplayer : MonoBehaviour
{
    private GameManager gameManager => GameManager.Instance;
    private DataManager dataManager => DataManager.Instance;
    private Fade fade => Fade.Instance;

    [SerializeField] private Image bg;
    [SerializeField] private RectTransform title;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Button next;

    private void Start()
    {
        next.onClick.AddListener(() => fade.LoadSceneFade($"Stage{gameManager.stage + 2}"));
    }

    public void Display()
    {
        StartCoroutine(Routine());

        IEnumerator Routine()
        {
            bg.gameObject.SetActive(true);
            bg.color = default;

            bg.DOColor(new Color(0, 0, 0, 0.5f), 0.5f, updateType: UpdateType.Update);
            title.DOScale(Vector3.one, 1f, Ease.OutBack, UpdateType.Update);

            var wait = new WaitForSecondsRealtime(0);
            for (float t = 0; t < 1f; t += Time.unscaledDeltaTime)
            {
                timeText.text = $"Time: {Mathf.Lerp(0, gameManager.playTime, t):##.00}";
                moneyText.text = $"Money: {Mathf.Lerp(0, gameManager.gainMoney, t):#,##0.#}";
                yield return wait;
            }
            timeText.text = $"Time: {gameManager.playTime:##.00}";
            moneyText.text = $"Money: {gameManager.gainMoney:#,##0.#}";
        }
    }
}