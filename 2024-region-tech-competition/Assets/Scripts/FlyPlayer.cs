using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPlayer : MonoBehaviour
{
    private static FlyPlayer instance;
    public static FlyPlayer Instance => instance ??= FindObjectOfType<FlyPlayer>();

    [SerializeField] private Transform orientation;
    [SerializeField] private Transform model;
    [SerializeField] private LayerMask modelAlignLayer;
    [SerializeField] private float acc;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float steerSpeed;

    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(rigidbody.velocity.magnitude > 0.5f)
            orientation.Rotate(0, Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime, 0);

        Physics.Raycast(model.position, Vector3.down, out var hit, 10f, modelAlignLayer);
        model.rotation = Quaternion.Lerp(model.rotation, Quaternion.FromToRotation(Vector3.up, hit.normal) * orientation.rotation, Time.deltaTime * 7);

        var velY = rigidbody.velocity.y;
        rigidbody.velocity = Vector3.ClampMagnitude(new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z), maxSpeed);
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, velY, rigidbody.velocity.z);
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(Input.GetAxisRaw("Vertical") * model.forward * acc, ForceMode.Acceleration);
    }
}
