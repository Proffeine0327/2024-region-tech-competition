using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private WheelCollider[] wheels;

    private void Awake()
    {
        wheels = GetComponentsInChildren<WheelCollider>();
    }

    private void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        var dir = new Vector3(h, 0, v).normalized;

        foreach(var w in wheels)
            w.attachedRigidbody.AddForce(transform.InverseTransformDirection(dir) * moveSpeed);
    }

    private void LateUpdate()
    {
        Camera.main.transform.position =
            Vector3.Lerp(Camera.main.transform.position, transform.position + new Vector3(0, 2.5f, -6f), Time.deltaTime * 5f);
    }
}
