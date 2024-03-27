using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Vector3 posOffset;
    [SerializeField] private Vector3 lookOffset;
    [SerializeField] private float speed;

    private Player player => Player.Instance;

    void FixedUpdate()
    {
        var targetPos = player.orientation.TransformPoint(posOffset);
        transform.position = Vector3.Lerp(transform.position, targetPos, speed);

        var dir = player.orientation.position - transform.position;
        var targetRot = Quaternion.LookRotation(dir + lookOffset, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, speed);
    }
}
