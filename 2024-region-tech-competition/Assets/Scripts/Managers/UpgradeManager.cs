using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [SerializeField] private Image panel;
    [SerializeField] private Button exitBtn;
    [SerializeField] private Button improveWheelBtn;
    [SerializeField] private Button engineUpgradeBtn;

    public int Money { get; set; } = 1000;
    public bool ImprovedWheel { get; set; }
    public int EngineStep { get; set; }

    private void Start()
    {
        exitBtn.onClick.AddListener(() => Hide());
        improveWheelBtn.onClick.AddListener(() => ImprovedWheel = true);
        engineUpgradeBtn.onClick.AddListener(() => EngineStep++);
    }

    private void Update()
    {
        improveWheelBtn.interactable = Money >= 150 && !ImprovedWheel;
        if (EngineStep == 0) engineUpgradeBtn.interactable = Money >= 100;
        if (EngineStep == 1) engineUpgradeBtn.interactable = Money >= 200;
        if (EngineStep == 2) engineUpgradeBtn.interactable = false;
    }

    public void Display()
    {
        Time.timeScale = 0;
        panel.gameObject.SetActive(true);
    }

    public void Hide()
    {
        Time.timeScale = 1;
        panel.gameObject.SetActive(false);
    }
}