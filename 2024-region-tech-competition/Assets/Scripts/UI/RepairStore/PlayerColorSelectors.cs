using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColorSelectors : MonoBehaviour
{
    private ResourceLoader resourceLoader => ResourceLoader.Instance;
    private DataManager dataManager => DataManager.Instance;

    public List<Button> buttons;

    private void Start()
    {
        buttons.For((b, i) =>
        {
            b.image.color = GetColor(resourceLoader.playerColors[i].name);
            b.onClick.AddListener(() => dataManager.playerColorSelect = i);
        });
    }

    private Color GetColor(string name)
    {
        switch (name)
        {
            case "Black": return Color.black;
            case "Blue": return Color.blue;
            case "Cyan": return Color.cyan;
            case "Green": return Color.green;
            case "Purple": return Color.magenta;
            case "Red": return Color.red;
            default: return default;
        }
    }
}
