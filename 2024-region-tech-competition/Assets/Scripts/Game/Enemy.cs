using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : BaseFlighter
{
    private DataManager dataManager => DataManager.Instance;
    private GameManager gameManager => GameManager.Instance;

    [SerializeField] private NavMeshAgent followTarget;

    private bool isMoving;
    private new Rigidbody rigidbody;

    private EnemyData enemyData => dataManager.enemyDatas[gameManager.stage];

    protected override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!GameManager.Instance.isGameRunning) return;
        if (!isMoving)
        {
            isMoving = true;
            followTarget.speed = enemyData.maxSpeed;
        }

        followTarget.enabled = Vector3.Distance(transform.position, followTarget.transform.position) < 40;
        if(followTarget.isActiveAndEnabled) followTarget.SetDestination(GameManager.Instance.endPoint.position);
        
        var vely = rigidbody.velocity.y;
        rigidbody.velocity = Vector3.ClampMagnitude(new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z), enemyData.maxSpeed * slow);
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, vely, rigidbody.velocity.z);
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isGameRunning) return;
        if (!canMove) return;

        if (Vector3.Distance(followTarget.transform.position, transform.position) > 2f)
        {
            var dir = followTarget.transform.position - transform.position;
            dir.y = 0;
            dir = dir.normalized;
            transform.rotation = Quaternion.LookRotation(dir);

            rigidbody.AddForce(transform.forward * enemyData.acceleration, ForceMode.Acceleration);
        }
    }
}
