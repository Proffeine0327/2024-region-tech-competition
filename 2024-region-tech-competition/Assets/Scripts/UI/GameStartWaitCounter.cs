using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartWaitCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        GameManager.Instance.WaitCount
            .Subcribe(x =>
            {
                if(x < 0)
                {
                    text.enabled = false;
                    return;
                }

                text.enabled = true;

                text.text = x == 0 ? "Start!" : x.ToString();
                text.color = Color.white;

                TweenUtility.DOFloat(400, 300, 1, x => text.fontSize = x, ease:Ease.OutQuart);
                text.DOColor(new Color(1, 1, 1, 0), 1);
            });
    }
}
