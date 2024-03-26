using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Update()
    {
        text.text = $"{UpgradeManager.Instance.Money:#,##0}$";
    }
}
