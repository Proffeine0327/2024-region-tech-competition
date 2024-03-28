using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RepairStoreSettingWindow : MonoBehaviour
{
    public enum Type { Type, Color, Part }

    public List<Button> buttons;
    public List<GameObject> windows;

    [NonSerialized] public Type type;

    private void Start()
    {
        buttons.For((b, i) => b.onClick.AddListener(() => type = (Type)i));
    }

    private void Update()
    {
        buttons.For((b, i) => b.image.color = i == (int)type ? new Color(0, 0, 0, 0.5f) : default);
        windows.For((w, i) => w.SetActive(i == (int)type));
    }
}