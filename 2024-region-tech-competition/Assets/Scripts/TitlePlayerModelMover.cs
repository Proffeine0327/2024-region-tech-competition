using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayerModelMover : MonoBehaviour
{
    private DataManager dataManager => DataManager.Instance;
    private ResourceLoader resourceLoader => ResourceLoader.Instance;

    public Vector3 disappearePosition;

    private float prev;

    public void Disappeare(float time)
    {
        transform.DOMove(disappearePosition, time, Ease.InBack);
    }

    private void Start()
    {
        var model = Instantiate(resourceLoader.playerModels[dataManager.playerSelect], transform, true);
        model.transform.localPosition = Vector3.zero;
        model.transform.localEulerAngles = Vector3.zero;
    }

    private void Update()
    {
        transform.position += new Vector3(0, (prev - Mathf.Sin(Time.time)) * 0.5f, 0);
        prev = Mathf.Sin(Time.time);
    }
}
