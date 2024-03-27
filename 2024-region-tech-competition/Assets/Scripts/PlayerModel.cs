using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    private DataManager dataManager => DataManager.Instance;
    private GameManager gameManager => GameManager.Instance;

    public GameObject speedParticle;
    public List<GameObject> improveWings;
    public List<GameObject> engineUpgrades;
    public List<GameObject> driftTrails;

    [NonSerialized] public bool isSpeed;
    [NonSerialized] public bool isDrift;

    private void Update()
    {
        var displayWheel = gameManager.stage switch
        {
            1 => dataManager.desertWing,
            2 => dataManager.mountainWing,
            3 => dataManager.cityWing,
            _ => false
        };

        improveWings.ForEach(x => x.gameObject.SetActive(displayWheel));
        engineUpgrades.For((x, index) => x.gameObject.SetActive(dataManager.engine == (EngineType)(index - 1)));
        driftTrails.ForEach(x => x.SetActive(isDrift));
        speedParticle.SetActive(isSpeed);
    }
}
