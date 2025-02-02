using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy_P;

public class Enemy_W : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Patrolling,
        Chasing
    }

    public EnemyState currentState = EnemyState.Idle;
    private Transform player;

    [SerializeField] private float chaseRange = 4f;
    [SerializeField] private float intenseChaseSpeed = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case EnemyState.Idle:
                if (distanceToPlayer < chaseRange)
                    currentState = EnemyState.Chasing;
                break;

            case EnemyState.Chasing:
                ChasePlayer();
                break;
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * intenseChaseSpeed * Time.deltaTime;
    }
}

