using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayTimeDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;

    private void Update()
    {
        tmp.text = GameManager.Instance.playTime.ToString("0.00");
    }
}
