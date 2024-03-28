using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTypeSelector : MonoBehaviour
{
    private DataManager dataManager => DataManager.Instance;
    private SubtractMoneyDisplayer subtractMoneyDisplayer => SubtractMoneyDisplayer.Instance;

    public int index;
    public GameObject unlock;
    public TextMeshProUGUI money;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (!dataManager.unlockPlayer.Contains(index))
            {
                if(dataManager.money >= dataManager.playerDatas[index].unlockMoney)
                {
                    dataManager.unlockPlayer.Add(index);
                    dataManager.money -= dataManager.playerDatas[index].unlockMoney;
                    subtractMoneyDisplayer.Display(dataManager.playerDatas[index].unlockMoney);
                }
            }
            else
            {
                dataManager.playerSelect = index;
            }
        });
    }

    private void Update()
    {
        unlock.SetActive(!dataManager.unlockPlayer.Contains(index));
        money.text = $"{dataManager.playerDatas[index].unlockMoney}$";
    }
}