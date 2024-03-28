using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyDisplayer : MonoBehaviour
{
    private DataManager dataManager => DataManager.Instance;

    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = $"{dataManager.money:#,##0}$";
    }
}
