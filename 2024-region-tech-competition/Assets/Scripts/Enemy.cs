using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : BaseFlighter
{
    [SerializeField] private NavMeshAgent followTarget;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;

    private bool isMoving;
    private new Rigidbody rigidbody;

    protected override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameRunning) return;
        if (!isMoving)
        {
            isMoving = true;
            followTarget.speed = maxSpeed;
        }

        followTarget.enabled = Vector3.Distance(transform.position, followTarget.transform.position) < 40;
        if(followTarget.isActiveAndEnabled) followTarget.SetDestination(GameManager.Instance.EndPoint.position);
        
        var vely = rigidbody.velocity.y;
        rigidbody.velocity = Vector3.ClampMagnitude(new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z), maxSpeed * slow);
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, vely, rigidbody.velocity.z);
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsGameRunning) return;
        if (!canMove) return;

        if (Vector3.Distance(followTarget.transform.position, transform.position) > 2f)
        {
            var dir = followTarget.transform.position - transform.position;
            dir.y = 0;
            dir = dir.normalized;
            transform.rotation = Quaternion.LookRotation(dir);

            rigidbody.AddForce(transform.forward * acceleration, ForceMode.Acceleration);
        }
    }
}
