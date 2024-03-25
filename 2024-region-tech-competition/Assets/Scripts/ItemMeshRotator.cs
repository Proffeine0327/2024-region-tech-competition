using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMeshRotator : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(new Vector3(210, 293, 123) * Time.deltaTime);
    }
}
