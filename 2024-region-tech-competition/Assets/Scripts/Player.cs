using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody phys;
    [SerializeField] private float steering;
    [SerializeField] private float acceleration;
    [SerializeField] private LayerMask layerMask;

    private float speed;
    private float curSpeed;
    private float rotate;
    private float curRotate;

    private void Update()
    {
        transform.position = phys.position;

        speed = acceleration * Input.GetAxisRaw("Vertical");

        if (Input.GetAxis("Horizontal") != 0)
        {
            var dir = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
            var amount = Mathf.Abs(Input.GetAxis("Horizontal"));
            Steer(dir, amount);
        }

        curSpeed = Mathf.SmoothStep(curSpeed, speed, Time.deltaTime * 12f); speed = 0;
        curRotate = Mathf.Lerp(curRotate, rotate, Time.deltaTime * 4f); rotate = 0;
    }

    private void FixedUpdate()
    {
        phys.AddForce(transform.forward * curSpeed, ForceMode.Acceleration);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + curRotate, 0), Time.deltaTime * 5f);

        //Physics.Raycast(transform.position, Vector3.down, out var hit, 2f, layerMask);
        //transform.up = Vector3.Lerp(transform.up, hit.normal, 8f * Time.deltaTime);
        //Debug.DrawRay(transform.position, transform.up, Color.red);
        //Debug.DrawRay(transform.position, hit.normal, Color.blue);
    }

    private void Steer(int dir, float amount)
    {
        rotate = (steering * dir) * amount;
    }
}
