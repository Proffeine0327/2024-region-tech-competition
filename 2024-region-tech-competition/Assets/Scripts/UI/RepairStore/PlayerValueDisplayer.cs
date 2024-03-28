using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerValueDisplayer : MonoBehaviour
{
    private DataManager dataManager => DataManager.Instance;

    public TextMeshProUGUI text;

    private PlayerData playerData => dataManager.playerDatas[dataManager.playerSelect];

    private void Update()
    {
        text.text = $"{playerData.maxSpeed * 5}\n{playerData.acceleration}\n{playerData.steerSpeed}";
    }
}
