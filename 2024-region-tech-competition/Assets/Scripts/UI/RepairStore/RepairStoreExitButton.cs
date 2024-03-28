using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairStoreExitButton : MonoBehaviour
{
    private Fade fade => Fade.Instance;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => fade.LoadSceneFade("Title"));
    }
}
