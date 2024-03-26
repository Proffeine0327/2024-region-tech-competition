using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [SerializeField] private Image panel;
    [SerializeField] private List<GameObject> blinders;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI explain;
    [SerializeField] private RectTransform selectOutline;

    private bool isDisplay;
    private int select = 0;
    private bool[] selectable = { false, false, };

    public int Money { get; set; } = 1000;
    public bool ImprovedWheel { get; set; }
    public int EngineStep { get; set; }

    private void Update()
    {
        if (isDisplay && Input.GetKeyDown(KeyCode.Escape)) Hide();
        if (Input.GetKeyDown(KeyCode.RightArrow)) select++;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) select--;
        select = Mathf.Clamp(select, 0, 1);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            switch (select)
            {
                case 0:
                    if (selectable[0]) ImprovedWheel = true;
                    break;
                case 1:
                    if (selectable[1]) EngineStep++;
                    break;
            }
        }

        switch (select)
        {
            case 0:
                title.text = "Dedicated Wing Upgrade";
                explain.text = "Upgrade wings to dedicated\nAcceleration gain 25%\n\n" + (ImprovedWheel ? "Sold Out" : "150$");
                break;
            case 1:
                if (EngineStep == 0)
                {
                    title.text = "6 Cylinder Engin Upgrade";
                    explain.text = "Upgrade engine to 6 cylinder\nMax speed gain 10%\n\n100$";
                }
                if (EngineStep == 1)
                {
                    title.text = "8 Cylinder Engin Upgrade";
                    explain.text = "Upgrade engine to 8 cylinder\nMax speed gain 25%\n\n200$";
                }
                if (EngineStep == 2)
                {
                    title.text = "8 Cylinder Engin Upgrade";
                    explain.text = "Upgrade engine to 8 cylinder\nMax speed gain 25%\n\nSold Out";
                }
                break;
        }

        selectable[0] = Money >= 150 && !ImprovedWheel;
        if (EngineStep == 0) selectable[1] = Money >= 100;
        if (EngineStep == 1) selectable[1] = Money >= 200;
        if (EngineStep == 2) selectable[1] = false;

        blinders.For((g, index) => g.SetActive(!selectable[index]));
        selectOutline.position = blinders[select].transform.position;
    }

    public void Display()
    {
        isDisplay = true;
        Time.timeScale = 0;
        panel.gameObject.SetActive(true);
    }

    public void Hide()
    {
        isDisplay = false;
        Time.timeScale = 1;
        panel.gameObject.SetActive(false);
    }
}