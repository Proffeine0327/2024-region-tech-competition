using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartWaitCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void Display(int count)
    {
        text.enabled = true;

        text.text = count == 0 ? "Start!" : count.ToString();
        text.color = Color.white;

        TweenUtility.DOFloat(400, 300, 1, x => text.fontSize = x, ease: Ease.OutQuad);
        text.DOColor(new Color(1, 1, 1, 0), 1);
    }
}
