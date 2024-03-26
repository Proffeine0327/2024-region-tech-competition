using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent followTarget;

    private bool isMoving;
    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        GameManager.Instance.ObserveEveryValueChanged(x => x.IsGameRunning)
            .Subscribe(x =>
            {
                if (!x) return;
                followTarget.SetDestination(GameManager.Instance.EndPoint.position);
            });
    }

    private void Update()
    {
        
    }
}
