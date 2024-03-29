using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlate : MonoBehaviour
{
    private DataManager dataManager => DataManager.Instance;
    private ResourceContainer resourceContainer => ResourceContainer.Instance;

    private int prevPlayer = -1;
    private int prevColor = -1;
    private MeshRenderer model;

    private void Update()
    {
        transform.Rotate(0, 60 * Time.deltaTime, 0);

        if(prevPlayer != dataManager.playerSelect)
        {
            prevPlayer = dataManager.playerSelect;
            if (model) Destroy(model.gameObject);
            model = Instantiate(resourceContainer.playerModels[dataManager.playerSelect], transform, true).GetComponent<MeshRenderer>();
            model.sharedMaterial = resourceContainer.playerColors[dataManager.playerColorSelect];
            model.transform.localPosition = new Vector3(0, 3.333f, 0.1f);
            model.transform.localEulerAngles = Vector3.zero;
        }

        if(prevColor != dataManager.playerColorSelect)
        {
            prevColor = dataManager.playerColorSelect;
            model.sharedMaterial = resourceContainer.playerColors[dataManager.playerColorSelect];
        }
    }
}
