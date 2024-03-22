using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAlign : MonoBehaviour
{
    [SerializeField] private Transform model;
    [SerializeField] private LayerMask layer;

    private void Update()
    {
        Physics.Raycast(model.position, Vector3.down, out var hit, 10f, layer);
            var align = Quaternion.FromToRotation(Vector3.up, hit.normal);
        var quat = align * transform.rotation;
        model.rotation = Quaternion.Lerp(model.rotation, quat, Time.deltaTime * 10f);
    }
}
