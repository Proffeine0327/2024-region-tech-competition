using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RepairStorePartView : MonoBehaviour
{
    private DataManager dataManager => DataManager.Instance;
    private SubtractMoneyDisplayer subtractMoneyDisplayer => SubtractMoneyDisplayer.Instance;

    [Header("Wing")]
    public Button desert;
    public TextMeshProUGUI desertText;
    public Button mountain;
    public TextMeshProUGUI mountainText;
    public Button city;
    public TextMeshProUGUI cityText;
    [Header("Engine")]
    public Button engine;
    public TextMeshProUGUI engineText;


    private void Start()
    {
        desert.onClick.AddListener(() =>
        {
            if (dataManager.money >= 150)
            {
                dataManager.money -= 150;
                dataManager.desertWing = true;
                subtractMoneyDisplayer.Display(150);
            }
        });
        mountain.onClick.AddListener(() =>
        {
            if (dataManager.money >= 150)
            {
                dataManager.money -= 150;
                dataManager.mountainWing = true;
                subtractMoneyDisplayer.Display(150);
            }
        });
        city.onClick.AddListener(() =>
        {
            if (dataManager.money >= 150)
            {
                dataManager.money -= 150;
                dataManager.cityWing = true;
                subtractMoneyDisplayer.Display(150);
            }
        });

        engine.onClick.AddListener(() =>
        {
            switch (dataManager.engine)
            {
                case EngineType.None:
                    if (dataManager.money >= 100)
                    {
                        dataManager.money -= 100;
                        dataManager.engine = EngineType.Engine6;
                        subtractMoneyDisplayer.Display(100);
                    }
                    break;
                case EngineType.Engine6:
                    if (dataManager.money >= 200)
                    {
                        dataManager.money -= 200;
                        dataManager.engine = EngineType.Engine8;
                        subtractMoneyDisplayer.Display(200);
                    }
                    break;
            }
        });
    }

    private void Update()
    {
        desert.interactable = dataManager.money >= 150 && !dataManager.desertWing;
        mountain.interactable = dataManager.money >= 150 && !dataManager.mountainWing;
        city.interactable = dataManager.money >= 150 && !dataManager.cityWing;

        desertText.text = dataManager.desertWing ? "Desert Wing" : "Desert Wing\n150$";
        mountainText.text = dataManager.mountainWing ? "Mountain Wing" : "Mountain Wing\n150$";
        cityText.text = dataManager.cityWing ? "City Wing" : "City Wing\n150$";

        engine.interactable = dataManager.engine switch
        {
            EngineType.None => dataManager.money >= 100,
            EngineType.Engine6 => dataManager.money >= 200,
            _ => false
        };
        engineText.text = dataManager.engine switch
        {
            EngineType.None => "Engine6\n100$",
            EngineType.Engine6 => "Engine8\n200$",
            _ => "Engine8"
        };
    }
}
