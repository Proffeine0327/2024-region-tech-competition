using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;

    private FlyPlayer player => FlyPlayer.Instance;

    private void Start()
    {
        startPosition = player.transform.TransformPoint(transform.position);
        startRotation = player.transform.rotation * transform.rotation;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + startPosition, Time.deltaTime * 10f);
        transform.rotation = Quaternion.Lerp(transform.rotation, player.transform.rotation * startRotation, Time.deltaTime * 10f);
    }
}
