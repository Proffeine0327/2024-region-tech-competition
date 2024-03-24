using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private bool isMoving;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(!isMoving && GameManager.Instance.IsGameStart)
        {
            isMoving = true;
            agent.SetDestination(GameManager.Instance.EndPoint.position);
        }
    }
}
